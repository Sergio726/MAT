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
using MAT.MVC.Infrastructure;
using MAT.MVC.Models.Reportes;
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
                List<Entities.Viaje> viajesdefecha = Infrastructure.Data.ViajeDataAccess.GetAll()
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
                var correlationId = MAT.MVC.Infrastructure.RequestContext.GetOrCreateCorrelationId();
                MATLogger.Log($"[{correlationId}] HomeController.GetFechasDeViajes - {ex.GetType().Name}: {ex.Message}", 1);
                MATLogger.Log($"[{correlationId}] {ex.StackTrace}", 1);
                MAT.MVC.Infrastructure.DbErrorLogger.Log(correlationId, ex, Request?.Url?.ToString(), User?.Identity?.Name);
                return string.Empty;
            }
        }


        [Authorize]
        public ActionResult TodosLosViajesIndex()
        {
            try
            {
                var todos = Infrastructure.Data.ViajeDataAccess.GetAll()
                    .Where(v => v.FechaSalida.HasValue)
                    .OrderBy(v => v.FechaSalida)
                    .ToList();

                int currentYear = DateTime.Now.Year;

                if (!todos.Any())
                {
                    var soloActual = new List<SelectListItem>
                    {
                        new SelectListItem { Text = currentYear.ToString(), Value = currentYear.ToString(), Selected = true }
                    };
                    return View(soloActual);
                }

                int yearinicio = todos.First().FechaSalida.Value.Year;
                int yearfinal  = todos.Last().FechaSalida.Value.Year;
                int selectedYear = (currentYear >= yearinicio && currentYear <= yearfinal) ? currentYear : yearfinal;

                var yearlistitem = new List<SelectListItem>();
                for (int i = yearinicio; i <= yearfinal; i++)
                {
                    yearlistitem.Add(new SelectListItem
                    {
                        Text     = i.ToString(),
                        Value    = i.ToString(),
                        Selected = (i == selectedYear)
                    });
                }
                return View(yearlistitem);
            }
            catch (Exception ex)
            {
                var msg = MAT.MVC.Infrastructure.ErrorUtil.LogAndGetPublicMessage(ex, "HomeController.TodosLosViajesIndex");
                TempData["Error"] = msg;
                return View(new List<SelectListItem>());
            }
        }

        [Authorize]
        public ActionResult TodosLosViajes(string yearfilter)
        {
            try
            {
                if (!int.TryParse(yearfilter, out int year))
                    year = DateTime.Now.Year;

                var viajes = MVC.Models.ViajeMethod.ViajeByDate(null, year);
                return PartialView(viajes);
            }
            catch (Exception ex)
            {
                MAT.MVC.Infrastructure.ErrorUtil.LogAndGetPublicMessage(ex, "HomeController.TodosLosViajes");
                return PartialView(new List<ViajeModel>());
            }
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
                var correlationId = MAT.MVC.Infrastructure.RequestContext.GetOrCreateCorrelationId();
                MATLogger.Log($"[{correlationId}] HomeController.GetViajes - {ex.GetType().Name}: {ex.Message}", 1);
                MATLogger.Log($"[{correlationId}] {ex.StackTrace}", 1);
                MAT.MVC.Infrastructure.DbErrorLogger.Log(correlationId, ex, Request?.Url?.ToString(), User?.Identity?.Name);
                return string.Empty;
            }
           
        }

        [Authorize]
        public ActionResult HistorialPagos()
        {
            ViewBag.IsAdmin = AdminAuthorizationHelper.CanViewAllHistorialPagos(HttpContext);
            return View();
        }

        /// <summary>
        /// JSON para la grilla de historial de pagos (rango de fechas + tipo venta).
        /// Vendedores no admin solo ven sus propios cobros (administrador o admindev ven todos).
        /// </summary>
        [Authorize]
        [HttpGet]
        public ActionResult HistorialPagosConsultar(string from, string to, string tipoVentaId = null)
        {
            try
            {
                var canViewAll = AdminAuthorizationHelper.CanViewAllHistorialPagos(HttpContext);

                var q = ReportesQueryHelper.Parse(from, to, viajeId: null, vendedorId: null, clienteId: null, tipoVentaId);
                if (!q.IsValid)
                    return JsonCamelCaseHelper.Serialize(new { success = false, message = q.ValidationMessage });

                if (!q.ShouldExecute)
                    return JsonCamelCaseHelper.Serialize(new { success = true, data = new List<ReportePagoRowDto>(), total = 0, scopedToVendedor = !canViewAll });

                if (!canViewAll)
                {
                    var vendedorId = GetCurrentVendedorId();
                    if (!vendedorId.HasValue)
                        return JsonCamelCaseHelper.Serialize(new { success = false, message = "No se pudo identificar el vendedor asociado a su usuario." });

                    q = ReportesQueryParseResult.ForDateRange(
                        q.FromDate.Value,
                        q.ToDate.Value,
                        vendedorId,
                        q.ClienteId,
                        q.TipoVentaId);
                }

                var list = HistorialPagosLoader.Load(q);
                return JsonCamelCaseHelper.Serialize(new
                {
                    success = true,
                    data = list,
                    total = list.Count,
                    scopedToVendedor = !canViewAll
                });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "HomeController.HistorialPagosConsultar");
                return JsonCamelCaseHelper.Serialize(new { success = false, message = msg });
            }
        }

       
    }
}
