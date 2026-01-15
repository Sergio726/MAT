using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Services;
using System.Text;
using MAT.MVC.Models;
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
        //private readonly PaqueteDetalleService _paqueteDetalleService;

        //public HomeController()
        //{
        //    _paqueteDetalleService = new PaqueteDetalleService(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        //}

        //
        // GET: /Home/

        [Authorize]
        public ActionResult Index()
        {
            if (!WebSecurity.Initialized)
            {                
                return RedirectToAction("Login", "Account");
            }
            if (Roles.IsUserInRole(User.Identity.Name, "Administrador")) return RedirectToAction("Index", "Admin");
            
            // Obtener estadísticas del mes actual
            var estadisticas = GetEstadisticasMesActual();
            ViewBag.Estadisticas = estadisticas;
            
            return View();
        }

        /// <summary>
        /// Obtiene las estadísticas del mes actual
        /// </summary>
        private Dictionary<string, object> GetEstadisticasMesActual()
        {
            var estadisticas = new Dictionary<string, object>();
            
            try
            {
                var fechaInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var fechaFin = fechaInicio.AddMonths(1).AddDays(-1);
                
                // Obtener facturas del mes actual
                var facturaService = new FacturaService();
                var facturasMes = facturaService.GetAll()
                    .Where(f => f.Fecha.HasValue && 
                           f.Fecha.Value >= fechaInicio && 
                           f.Fecha.Value <= fechaFin)
                    .ToList();
                
                // Total por cobrar (facturas con saldo pendiente)
                double totalPorCobrar = 0;
                foreach (var factura in facturasMes)
                {
                    if (factura.Monto.HasValue)
                    {
                        var movimientos = new MovimientoCuentaService().GetByFacturaId(factura.FacturaId)
                            .Where(m => m.PagoId.HasValue).ToList();
                        double pagos = 0;
                        foreach (var mov in movimientos)
                        {
                            if (mov.PagoId.HasValue)
                            {
                                var pago = new PagoService().GetByPagoId(mov.PagoId.Value);
                                if (pago.Monto.HasValue)
                                    pagos += pago.Monto.Value;
                            }
                        }
                        double saldo = factura.Monto.Value - pagos;
                        if (saldo > 0)
                            totalPorCobrar += saldo;
                    }
                }
                
                // Cantidad de presupuestos del mes
                var presupuestosMes = PresupuestoMethod.GetAll()
                    .Where(p => p.FechaCreacion >= fechaInicio && p.FechaCreacion <= fechaFin)
                    .ToList();
                
                // Clientes nuevos del mes (clientes con su primera factura en el mes actual)
                // Un cliente es nuevo si su primera factura está en el mes actual
                var clientesConFacturasMes = facturasMes
                    .Where(f => f.ClienteId != Guid.Empty)
                    .Select(f => f.ClienteId)
                    .Distinct()
                    .ToList();
                
                var clientesNuevos = 0;
                foreach (var clienteId in clientesConFacturasMes)
                {
                    // Verificar si este cliente tiene facturas anteriores al mes actual
                    var facturasAnteriores = facturaService.GetByClienteId(clienteId, 0, 1, out int totalFacturas)
                        .Where(f => f.Fecha.HasValue && f.Fecha.Value < fechaInicio)
                        .Any();
                    
                    // Si no tiene facturas anteriores, es un cliente nuevo
                    if (!facturasAnteriores)
                    {
                        clientesNuevos++;
                    }
                }
                
                // Ventas por vendedor
                var ventasPorVendedor = facturasMes
                    .Where(f => f.VendedorId != Guid.Empty)
                    .GroupBy(f => f.VendedorId)
                    .Select(g => new
                    {
                        VendedorId = g.Key,
                        Cantidad = g.Count(),
                        Total = g.Sum(f => f.Monto ?? 0)
                    })
                    .ToList();
                
                var vendedorService = new VendedorService();
                var ventasVendedorDetalle = ventasPorVendedor.Select(v => new
                {
                    VendedorNombre = vendedorService.GetByVendedorId(v.VendedorId)?.Descripcion ?? "Sin nombre",
                    Cantidad = v.Cantidad,
                    Total = v.Total
                }).ToList();
                
                estadisticas["TotalPorCobrar"] = totalPorCobrar;
                estadisticas["CantidadPresupuestos"] = presupuestosMes.Count;
                estadisticas["ClientesNuevos"] = clientesNuevos;
                estadisticas["VentasPorVendedor"] = ventasVendedorDetalle;
                estadisticas["TotalVentas"] = facturasMes.Sum(f => f.Monto ?? 0);
                estadisticas["CantidadVentas"] = facturasMes.Count;
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error al obtener estadísticas del mes: {ex.Message}", 1);
                // Valores por defecto en caso de error
                estadisticas["TotalPorCobrar"] = 0;
                estadisticas["CantidadPresupuestos"] = 0;
                estadisticas["ClientesNuevos"] = 0;
                estadisticas["VentasPorVendedor"] = new List<object>();
                estadisticas["TotalVentas"] = 0;
                estadisticas["CantidadVentas"] = 0;
            }
            
            return estadisticas;
        }

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
