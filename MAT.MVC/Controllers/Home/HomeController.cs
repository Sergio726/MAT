using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
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
        /// Panel "Próximas salidas" del inicio: viajes de hoy a 40 días con libres, vendidos y pre-reservas.
        /// Reemplaza al recuadro flotante que se abría solo en cada entrada (y mostraba viajes sin orden, sin los de hoy
        /// ni los completos). Se guarda 2 minutos en memoria: lo piden todos los usuarios al entrar.
        /// </summary>
        [Authorize]
        [HttpGet]
        public ActionResult ProximasSalidas()
        {
            try
            {
                const string clave = "MAT.Home.ProximasSalidas.v1";
                var lista = HttpRuntime.Cache[clave] as List<MAT.MVC.Models.Home.ProximaSalidaDto>;
                if (lista == null)
                {
                    lista = Infrastructure.Data.DashboardDataAccess.GetProximasSalidas(40);
                    HttpRuntime.Cache.Insert(clave, lista, null, DateTime.UtcNow.AddMinutes(2), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                return JsonCamelCaseHelper.Serialize(new { success = true, data = lista });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "HomeController.ProximasSalidas");
                return JsonCamelCaseHelper.Serialize(new { success = false, message = msg });
            }
        }

        /// <summary>
        /// Buscador global de la barra superior, etapa 2 (bloque G del plan del panel principal): clientes,
        /// facturas (por titular) y viajes. Los módulos se buscan en el navegador; esto suma los datos.
        /// Mismos permisos que las pantallas de destino (solo sesión iniciada).
        /// </summary>
        [Authorize]
        [HttpGet]
        public ActionResult Buscar(string q)
        {
            try
            {
                var grupos = Infrastructure.Data.BuscadorDataAccess.Buscar(q);
                return JsonCamelCaseHelper.Serialize(new { success = true, grupos = grupos });
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "HomeController.Buscar");
                return JsonCamelCaseHelper.Serialize(new { success = false, message = msg });
            }
        }

        /// <summary>
        /// Obtiene las estadísticas del mes actual filtradas por vendedor, con la tendencia de cada indicador.
        /// Si vendedorId es null, retorna datos de todos los vendedores (admin).
        /// Tendencia (bloque E del plan del panel principal): mes en curso contra el mismo tramo del mes anterior
        /// (del 1 al día de hoy), no contra el mes completo, que al comienzo del mes siempre daría una caída falsa.
        /// El tramo anterior está cerrado: se guarda 10 minutos en memoria por vendedor y día.
        /// </summary>
        private Dictionary<string, object> GetEstadisticasMesActual(Guid? vendedorId)
        {
            var hoy = DateTime.Today;
            var fechaInicio = new DateTime(hoy.Year, hoy.Month, 1);
            var estadisticas = GetEstadisticas(vendedorId, fechaInicio, fechaInicio.AddMonths(1).AddDays(-1));

            try
            {
                DateTime desdeAnterior, hastaAnterior;
                Models.Home.TendenciaKpi.PeriodoAnterior(hoy, out desdeAnterior, out hastaAnterior);
                var clave = "MAT.Home.EstadisticasAnteriores." + (vendedorId.HasValue ? vendedorId.Value.ToString() : "todos") + "." + hoy.ToString("yyyyMMdd");
                var anteriores = HttpRuntime.Cache[clave] as Dictionary<string, object>;
                if (anteriores == null)
                {
                    anteriores = GetEstadisticas(vendedorId, desdeAnterior, hastaAnterior);
                    HttpRuntime.Cache.Insert(clave, anteriores, null, DateTime.UtcNow.AddMinutes(10), System.Web.Caching.Cache.NoSlidingExpiration);
                }

                var periodo = Models.Home.TendenciaKpi.EtiquetaPeriodo(desdeAnterior, hastaAnterior);
                foreach (var clave2 in new[] { "CantidadPresupuestos", "ClientesNuevos", "CantidadVentas" })
                {
                    estadisticas["Tendencia" + clave2] = Models.Home.TendenciaKpi.Calcular(
                        Convert.ToDouble(estadisticas[clave2]), Convert.ToDouble(anteriores[clave2]), periodo);
                }
            }
            catch (Exception ex)
            {
                // Sin tendencia el inicio sigue funcionando: la vista no la muestra
                MATLogger.Log($"Error tendencia de estadísticas: {ex.Message}", 1);
            }

            return estadisticas;
        }

        /// <summary>
        /// Presupuestos, clientes nuevos y ventas de un rango (las dos fechas inclusive, como día completo).
        /// </summary>
        private Dictionary<string, object> GetEstadisticas(Guid? vendedorId, DateTime fechaInicio, DateTime fechaFin)
        {
            var estadisticas = new Dictionary<string, object>();
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
        public ActionResult GetClientesNuevosDetalle(string fechaDesde = null, string fechaHasta = null, int pagina = 1, int tamanio = 25)
        {
            return new MAT.MVC.Controllers.PersonaCliente.PersonaClienteController()
                        .GetClientesNuevosDetalle(fechaDesde, fechaHasta, pagina, tamanio);
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
                // 2026-11-07: sin la imagen de fondo de prueba (pedía la URL "foto", que no existe, una vez por tarjeta),
                // sin repetir la descripción, textos codificados y enlace real a Reservas (antes href='#' y la
                // navegación quedaba solo en el JS de .btn_viaje, que se conserva).
                foreach (var item in Viajes)
                {
                    var titulo = !String.IsNullOrWhiteSpace(item.Descripcion) ? item.Descripcion : item.PaqueteNombre;
                    htmlstring.Append("<li>");
                    htmlstring.Append("<a href='/Reserva/Index?viajeid=" + item.ViajeID + "' class='btn_viaje' data-id='" + item.ViajeID + "'>");
                    htmlstring.Append("<div class='viaje-container'>");
                    htmlstring.Append("<h3>" + HttpUtility.HtmlEncode(titulo) + "</h3>");
                    if (!String.IsNullOrWhiteSpace(item.PaqueteNombre) && item.PaqueteNombre != titulo)
                    {
                        htmlstring.Append("<h5>Paquete: " + HttpUtility.HtmlEncode(item.PaqueteNombre) + "</h5>");
                    }
                    htmlstring.Append("<h6>Salida: " + HttpUtility.HtmlEncode(item.FechaSalida.ToString()) + "</h6>");
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
