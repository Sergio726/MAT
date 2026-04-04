using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Services;
using System.Text;
using MAT.MVC.Models;
using MAT.MVC.Common;
using WebMatrix.WebData;
using PagedList;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using MAT.Utilities;
using System.Configuration;

namespace MAT.MVC.Controllers.Home
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        [Authorize]
        public ActionResult Index()
        {
            if (!WebSecurity.Initialized)
            {
                return RedirectToAction("Login", "Account");
            }

            bool isAdmin = Roles.IsUserInRole(User.Identity.Name, "Administrador");
            Guid? vendedorId = null;

            if (!isAdmin)
            {
                try
                {
                    var vendedor = MATContext.CurrentVendedor;
                    if (vendedor != null)
                        vendedorId = vendedor.VendedorId;
                }
                catch
                {
                    // Si no se puede obtener el vendedor, se muestra sin filtro
                }
            }

            ViewBag.IsAdmin = isAdmin;

            var estadisticas = GetEstadisticasMesActual(vendedorId);
            ViewBag.Estadisticas = estadisticas;

            return View();
        }

        /// <summary>
        /// Obtiene las estadísticas del mes actual filtradas por vendedor.
        /// Si vendedorId es null, retorna datos de todos los vendedores (admin).
        /// </summary>
        private Dictionary<string, object> GetEstadisticasMesActual(Guid? vendedorId)
        {
            var estadisticas = new Dictionary<string, object>();
            var fechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var fechaFin = fechaInicio.AddMonths(1).AddDays(-1);
            string sFrom = fechaInicio.ToString("dd-MM-yyyy");
            string sTo = fechaFin.ToString("dd-MM-yyyy");

            // --- PRESUPUESTOS ---
            try
            {
                var presupuestosMes = PresupuestoMethod.GetAll(
                    vendedorIdOrigen: vendedorId,
                    fechaDesde: fechaInicio,
                    fechaHasta: fechaFin
                );
                estadisticas["CantidadPresupuestos"] = presupuestosMes.Count;
                estadisticas["TotalPresupuestos"] = presupuestosMes.Sum(p => p.MontoPactado);
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error estadísticas presupuestos: {ex.Message}", 1);
                estadisticas["CantidadPresupuestos"] = 0;
                estadisticas["TotalPresupuestos"] = 0.0;
            }

            // --- CLIENTES NUEVOS (SQL directo) ---
            try
            {
                estadisticas["ClientesNuevos"] = GetClientesNuevosCount(vendedorId, fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error estadísticas clientes nuevos: {ex.Message}", 1);
                estadisticas["ClientesNuevos"] = 0;
            }

            // --- VENTAS (usp_MAT_Reportes_Ventas) ---
            try
            {
                double totalFacturado = 0;
                double totalCobrado = 0;
                double totalPendiente = 0;
                int cantidadVentas = 0;

                SqlParameter[] ventasParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@From", SqlDbType.NVarChar, 10, sFrom),
                    DBHelper.MakeParam("@To", SqlDbType.NVarChar, 10, sTo),
                    DBHelper.MakeParam("@ViajeId", SqlDbType.UniqueIdentifier, 0, DBNull.Value),
                    DBHelper.MakeParam("@VendedorId", SqlDbType.UniqueIdentifier, 0, vendedorId.HasValue ? (object)vendedorId.Value : DBNull.Value),
                    DBHelper.MakeParam("@ClienteId", SqlDbType.UniqueIdentifier, 0, DBNull.Value)
                };

                using (SqlDataReader reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Reportes_Ventas", ventasParams))
                {
                    while (reader.Read())
                    {
                        cantidadVentas++;
                        totalFacturado += reader["TotalFactura"] != DBNull.Value ? Convert.ToDouble(reader["TotalFactura"]) : 0;
                        totalCobrado += reader["MontoPagado"] != DBNull.Value ? Convert.ToDouble(reader["MontoPagado"]) : 0;
                        totalPendiente += reader["Saldo"] != DBNull.Value ? Convert.ToDouble(reader["Saldo"]) : 0;
                    }
                }

                estadisticas["TotalFacturado"] = totalFacturado;
                estadisticas["TotalCobrado"] = totalCobrado;
                estadisticas["TotalPendiente"] = totalPendiente;
                estadisticas["CantidadVentas"] = cantidadVentas;
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error estadísticas ventas: {ex.Message}", 1);
                estadisticas["TotalFacturado"] = 0.0;
                estadisticas["TotalCobrado"] = 0.0;
                estadisticas["TotalPendiente"] = 0.0;
                estadisticas["CantidadVentas"] = 0;
            }

            return estadisticas;
        }

        /// <summary>
        /// Cuenta clientes nuevos del mes usando SQL directo (sin FacturaService/PersonaService).
        /// Un cliente es nuevo si su primera factura en el sistema cae en el rango de fechas.
        /// </summary>
        private int GetClientesNuevosCount(Guid? vendedorId, DateTime fechaInicio, DateTime fechaFin)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MAT.Data.ConnectionString"].ToString();
            string sql = @"
                ;WITH PrimeraFactura AS (
                    SELECT f.ClienteID, MIN(f.Fecha) AS PrimeraFecha
                    FROM dbo.Factura f
                    WHERE f.Fecha IS NOT NULL
                    GROUP BY f.ClienteID
                )
                SELECT COUNT(*) AS Total
                FROM PrimeraFactura pf
                INNER JOIN dbo.Cliente c ON c.ClienteID = pf.ClienteID
                WHERE pf.PrimeraFecha >= @FechaInicio
                  AND pf.PrimeraFecha < @FechaFin
                  AND (@VendedorId IS NULL OR c.VendedorID = @VendedorId)";

            using (var cn = new SqlConnection(connStr))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                cmd.Parameters.AddWithValue("@FechaFin", fechaFin.AddDays(1));
                cmd.Parameters.AddWithValue("@VendedorId", vendedorId.HasValue ? (object)vendedorId.Value : DBNull.Value);
                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        #region AJAX Endpoints para detalle de indicadores

        /// <summary>
        /// Detalle de presupuestos del mes (para popup)
        /// </summary>
        [Authorize]
        public JsonResult GetPresupuestosDetalle()
        {
            try
            {
                Guid? vendedorId = GetCurrentVendedorId();
                var fechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var fechaFin = fechaInicio.AddMonths(1).AddDays(-1);

                var lista = PresupuestoMethod.GetAll(
                    vendedorIdOrigen: vendedorId,
                    fechaDesde: fechaInicio,
                    fechaHasta: fechaFin
                );

                var result = lista.Select(p => new
                {
                    p.CodigoSeguimiento,
                    p.NombreCliente,
                    p.DniCliente,
                    p.MontoPactado,
                    Estado = p.Estado.ToString(),
                    FechaCreacion = p.FechaCreacion.ToString("dd/MM/yyyy HH:mm"),
                    p.ViajeDescripcion,
                    p.VendedorOrigenNombre,
                    p.Observaciones
                });

                return Json(new { success = true, data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Delega al endpoint canónico en PersonaClienteController.
        /// </summary>
        [Authorize]
        public JsonResult GetClientesNuevosDetalle(string fechaDesde = null, string fechaHasta = null)
        {
            return new MAT.MVC.Controllers.PersonaCliente.PersonaClienteController()
                        .GetClientesNuevosDetalle(fechaDesde, fechaHasta);
        }

        /// <summary>
        /// Detalle de ventas del mes (para popup) usando usp_MAT_Reportes_Ventas
        /// </summary>
        [Authorize]
        public JsonResult GetVentasDetalle()
        {
            try
            {
                Guid? vendedorId = GetCurrentVendedorId();
                var fechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var fechaFin = fechaInicio.AddMonths(1).AddDays(-1);
                string sFrom = fechaInicio.ToString("dd-MM-yyyy");
                string sTo = fechaFin.ToString("dd-MM-yyyy");

                SqlParameter[] ventasParams = new SqlParameter[]
                {
                    DBHelper.MakeParam("@From", SqlDbType.NVarChar, 10, sFrom),
                    DBHelper.MakeParam("@To", SqlDbType.NVarChar, 10, sTo),
                    DBHelper.MakeParam("@ViajeId", SqlDbType.UniqueIdentifier, 0, DBNull.Value),
                    DBHelper.MakeParam("@VendedorId", SqlDbType.UniqueIdentifier, 0, vendedorId.HasValue ? (object)vendedorId.Value : DBNull.Value),
                    DBHelper.MakeParam("@ClienteId", SqlDbType.UniqueIdentifier, 0, DBNull.Value)
                };

                var ventas = new List<object>();
                using (SqlDataReader reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Reportes_Ventas", ventasParams))
                {
                    while (reader.Read())
                    {
                        ventas.Add(new
                        {
                            FacturaId = reader["FacturaId"]?.ToString() ?? "",
                            FacturaFecha = reader["FacturaFecha"] != DBNull.Value ? Convert.ToDateTime(reader["FacturaFecha"]).ToString("dd/MM/yyyy") : "",
                            FacturaEstado = reader["FacturaEstado"]?.ToString() ?? "",
                            ClienteFullName = reader["ClienteFullName"]?.ToString() ?? "",
                            VendedorFullName = reader["VendedorFullName"]?.ToString() ?? "",
                            ViajeDescripcion = reader["ViajeDescripcion"]?.ToString() ?? "",
                            FechaSalida = reader["FechaSalida"]?.ToString() ?? "",
                            CantidadButacas = reader["CantidadButacas"] != DBNull.Value ? Convert.ToInt32(reader["CantidadButacas"]) : 0,
                            MonedaTipo = reader["MonedaTipo"] != DBNull.Value ? Convert.ToInt32(reader["MonedaTipo"]) : 1,
                            TotalFactura = reader["TotalFactura"] != DBNull.Value ? Convert.ToDouble(reader["TotalFactura"]) : 0,
                            MontoPagado = reader["MontoPagado"] != DBNull.Value ? Convert.ToDouble(reader["MontoPagado"]) : 0,
                            Saldo = reader["Saldo"] != DBNull.Value ? Convert.ToDouble(reader["Saldo"]) : 0
                        });
                    }
                }

                return Json(new { success = true, data = ventas }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Obtiene el VendedorId del usuario logueado. Null si es admin.
        /// </summary>
        private Guid? GetCurrentVendedorId()
        {
            bool isAdmin = Roles.IsUserInRole(User.Identity.Name, "Administrador");
            if (isAdmin) return null;

            try
            {
                var vendedor = MATContext.CurrentVendedor;
                return vendedor?.VendedorId;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        [Authorize]
        public ActionResult ViajesPorFecha()
        {
            return View();
        }

        public string GetFechasDeViajes()
        {
            try
            {
                ViajeService vServ = new ViajeService();
                List<Entities.Viaje> viajesdefecha = vServ.GetAll()
                    .OrderByDescending(v => v.FechaSalida)
                    .ToList();

                StringBuilder arrayfechas = new StringBuilder();

                foreach (var item in viajesdefecha)
                {
                    if (item.FechaSalida.HasValue)
                    {
                        arrayfechas.Append(item.FechaSalida.Value.ToString("yyyy-MM-dd"));
                        arrayfechas.Append(",");
                    }
                }

                if (arrayfechas.Length > 0)
                    arrayfechas.Remove(arrayfechas.Length - 1, 1); // Remover la última coma

                return arrayfechas.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error en GetFechasDeViajes: " + ex.Message);
                return string.Empty; // o devolver un mensaje como "Error" si preferís
            }
        }


        [Authorize]
        public ActionResult TodosLosViajesIndex()
        {
            ViajeService vServ = new ViajeService();
            int yearinicio = vServ.GetAll().OrderBy(v => v.FechaSalida).FirstOrDefault().FechaSalida.Value.Year;
            int yearfinal = vServ.GetAll().OrderByDescending(v => v.FechaSalida).FirstOrDefault().FechaSalida.Value.Year;
            int currentYear = DateTime.Now.Year;
            // Si el año actual está fuera del rango, usar el año más reciente
            int selectedYear = (currentYear >= yearinicio && currentYear <= yearfinal) ? currentYear : yearfinal;

            List<SelectListItem> yearlistitem = new List<SelectListItem>();
            for (int i = yearinicio; i < yearfinal +1 ; i++)
            {
                SelectListItem item = new SelectListItem();
                item.Text = i.ToString();
                item.Value = i.ToString();
                item.Selected = (i == selectedYear);
                yearlistitem.Add(item);
            }
            return View(yearlistitem);
        }

        [Authorize]
        public ActionResult TodosLosViajes(string yearfilter)
        {
       
            List<ViajeModel> Viajes = new List<ViajeModel>();

            Viajes = MVC.Models.ViajeMethod.ViajeByDate(null, Convert.ToInt32(yearfilter));

            return PartialView(Viajes);
        }

        public ActionResult GenerarPasajes()
        {

            return null;
        }

        /// <summary>
        /// Metodo que devuelve una lista html de los viajes-paquetes
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public string GetViajes(string fecha)
        {
            try
            {
                
                DateTime datefecha = Convert.ToDateTime(fecha);

                // Creamos una lista simulada de viajes
                List<ViajeModel> Viajes = new List<ViajeModel>();

                Viajes = MVC.Models.ViajeMethod.ViajeByDate(fecha);
                                 
                // Generamos el HTML
                StringBuilder htmlstring = new StringBuilder();
                htmlstring.Append("<ul class='viajes-lista'>");
                foreach (var item in Viajes)
                {
                    htmlstring.Append("<li data-image='" + "fotoviaje" + "' style='background-image:url(" + "foto" + ");'>");
                    htmlstring.Append("<a href='#' class='btn_viaje' data-id=" + item.ViajeID + ">");
                    htmlstring.Append("<div class='viaje-container'>");
                    htmlstring.Append("<h3>"); htmlstring.Append(item.Descripcion); htmlstring.Append("</h3>");
                    if (!String.IsNullOrEmpty(item.Descripcion)) htmlstring.Append("<h5>" + item.Descripcion + "</h5>");

                    htmlstring.Append("<h5>"); htmlstring.Append(String.Format("Destino: {0}", item.PaqueteNombre)); htmlstring.Append("</h5>");
                    htmlstring.Append("<h6>"); htmlstring.Append(String.Format("Salida: {0}", item.FechaSalida.ToString())); htmlstring.Append("</h6>");
                    htmlstring.Append("</div></a></li>");
                }

                htmlstring.Append("</ul>");
                return htmlstring.ToString();
            }
            
            catch (Exception ex)
            {
                StringBuilder excepcion = new StringBuilder();
                excepcion.AppendLine(ex.Message);
                excepcion.AppendLine(ex.Source);
                excepcion.AppendLine(ex.StackTrace);
                return excepcion.ToString();
            }
           
        }

        public JsonResult QuickLocalidadSearch(string query, string sIdProvincia = "")
        {
            VLocalidadService localidadService = new VLocalidadService();
            List<Entities.VLocalidad> _localidades = localidadService.GetAll().Where(loc => loc.Nombre.ToUpper().Contains(query.ToUpper())).ToList();
            
            return Json(_localidades, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HistorialPagos()
        {
            return View();
        }

        public ActionResult RenderGridHistorialPagos(int? page, string filter)
        {
            List<Entities.Historial> historial = new Services.HistorialService().GetAll().ToList();
            IList<HistorialModel> historialModel = new List<HistorialModel>();
            foreach (var item in historial)
            {
                historialModel.Add(new HistorialModel(item.HistorialId));
            }
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (!string.IsNullOrEmpty(filter))
            {
                historialModel = historialModel.Where(hi => hi.Cliente.Nombre.ToUpper().Contains(filter.ToUpper()) || hi.Cliente.Apellido.ToUpper().Contains(filter.ToUpper()) || hi.Vendedor.Nombre.Contains(filter) || hi.Vendedor.Apellido.Contains(filter)).ToList();
            }
            return PartialView(historialModel.ToPagedList(pageNumber,pageSize));
        }

       
    }
}
