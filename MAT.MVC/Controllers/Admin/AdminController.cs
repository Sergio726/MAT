using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.MVC.Models;
using MAT.Utilities;
using MAT.Services;
using MAT.Enums;
using MAT.MVC.Common;
using System.Web.Security;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Newtonsoft.Json;
using MAT.MVC.Infrastructure;
namespace MAT.MVC.Controllers.Admin
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            if (!Roles.IsUserInRole(User.Identity.Name, "Administrador")) return RedirectToAction("Index", "Home");
            return View();
        }

        public ActionResult HistorialPrecios()
        {
            return View();
        }
                              

        public ActionResult ResumenPagos()
        {
            try
            {
                List<DDViaje> _DDViaje = new List<DDViaje>();
                _DDViaje = MAT.MVC.Models.ViajeMethod.DDViaje();

                var json = "";
                var jsonSerialiser = new JavaScriptSerializer();
                json = jsonSerialiser.Serialize(_DDViaje);
                ViewBag.jDDViaje = json;
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "AdminController.ResumenPagos");
                ViewBag.jDDViaje = "[]";
            }
            
            return View();
        }

        public ActionResult ResumenPagosPorFecha()
        {
            return View();
        }

        public ActionResult GridResumenPagos(Guid ViajeID)
        {
            List<PagoModel> _model = new List<PagoModel>();
            try
            {
                _model = PagoMethod.GetPagosByViaje(ViajeID);
                ViewBag.TotalPagos = _model.Sum(l => l.Monto);

                var jsonPatientList = JsonConvert.SerializeObject(_model);
                ViewBag.sbDataSetJson = jsonPatientList.ToString();
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "AdminController.GridResumenPagos");
            }
            return PartialView();
        }

        public ActionResult GridResumenPagosFecha(string fecha)
        {
            try
            {
                List<PagoModel> model = new List<PagoModel>();
                DateTime dFecha = Convert.ToDateTime(fecha);
                model = PagoMethod.GetPagosByFecha(dFecha);
                return PartialView(model);
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error: " + e.Message;
                return PartialView();

            }
        }

        public ActionResult ServiciosAdminList()
        {
            Services.ServicioService servicioService = new Services.ServicioService();
            List<MAT.Entities.Servicio> servicios = servicioService.GetAll().Where(se => se.TipoServicio == 1).OrderBy(ser => ser.Descripcion).ToList();
            return View(servicios);
        }

        public ActionResult ServiciosAdminCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ServiciosAdminCreate(FormCollection form)
        {
            Services.ServicioService servicioService = new Services.ServicioService();
            MAT.Entities.Servicio servicio = new Entities.Servicio();
            Helper.FillEntity<Entities.Servicio>(ref servicio, form);
            if (!string.IsNullOrEmpty(form["Precio"])) servicio.Precio = Convert.ToDouble(form["Precio"]);
            servicio.TipoServicio = 1;
            servicioService.Insert(servicio);
            return RedirectToAction("ServiciosAdminList");
        }

        public bool ServiciosAdminDelete(Guid id)
        {
            bool result = false;
            try
            {
                Services.ServicioService servicioService = new Services.ServicioService();
                servicioService.Delete(id);
                result = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                result = false;
            }
            return result;
        }

        public ActionResult ServiciosAdminPrecioCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ServiciosAdminPrecioCreate(FormCollection form)
        {
            Services.PrecioServicioService precioservicioService = new Services.PrecioServicioService();
            Entities.PrecioServicio precio = new Entities.PrecioServicio();
            precio.PrecioServicioId = Guid.NewGuid();
            precio.Activo = true;
            precio.FechaRegistro = DateTime.Now;
            if (!string.IsNullOrEmpty(form.Get("Precio"))) precio.Precio = Convert.ToInt32(form.Get("Precio"));
            if (!string.IsNullOrEmpty(form.Get("ServicioId"))) precio.ServicioId = Guid.Parse(form.Get("ServicioId"));

            List<Entities.PrecioServicio> servicios = precioservicioService.GetAll().Where(pr => pr.ServicioId == precio.ServicioId).ToList();
            foreach (var item in servicios)
            {
                item.Activo = false;
                precioservicioService.Update(item);
            }
            precioservicioService.Insert(precio);
            return View("ServiciosAdminPrecioHistorial");
        }

        public bool ServiciosAdminPrecioDelete(Guid id)
        {
            bool result = false;
            try
            {
                Services.PrecioServicioService precioService = new Services.PrecioServicioService();
                precioService.Delete(id);
                result = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                result = false;
            }

            return result;
        }

        public ActionResult ServiciosAdminPrecioEdit(Guid id)
        {
            Services.PrecioServicioService precioService = new Services.PrecioServicioService();
            Entities.PrecioServicio precio = precioService.GetByPrecioServicioId(id);
            return View(precio);
        }

        [HttpPost]
        public ActionResult ServiciosAdminPrecioEdit(Guid id, FormCollection form)
        {
            Services.PrecioServicioService precioservicioService = new Services.PrecioServicioService();
            Entities.PrecioServicio precio = precioservicioService.GetByPrecioServicioId(id);
            if (!string.IsNullOrEmpty(form.Get("Precio"))) precio.Precio = Convert.ToInt32(form.Get("Precio"));
            if (!string.IsNullOrEmpty(form.Get("ServicioId"))) precio.ServicioId = Guid.Parse(form.Get("ServicioId"));

            if (!string.IsNullOrEmpty(form.Get("Activo")))
            {
                precio.Activo = form.Get("Activo") == "false" ? false : true;

                if (precio.ServicioId != null && precio.Activo)
                {
                    List<Entities.PrecioServicio> servicios = precioservicioService.GetAll().Where(pr => pr.ServicioId == precio.ServicioId).ToList();
                    foreach (var item in servicios)
                    {
                        item.Activo = false;
                        precioservicioService.Update(item);
                    }        
                }
            }
            precio.Activo = form.Get("Activo") == "false" ? false : true;
            precioservicioService.Update(precio);
            return View("ServiciosAdminPrecioHistorial");
        }

        public ActionResult ServiciosAdminPrecioHistorial()
        {
            return View();
        }

        public ActionResult ServiciosAdminPrecioHistorialGrid(Guid id)
        {
            Services.PrecioServicioService precioservicioService = new Services.PrecioServicioService();
            List<Entities.PrecioServicio> precios = precioservicioService.GetAll().Where(p => p.ServicioId == id).OrderBy(p => p.FechaRegistro).ToList();
            return PartialView(precios);
        }

       
        public ActionResult PartialDropDownHotel(Guid id)
        {
            IEnumerable<SelectListItem> hoteles = MAT.Utilities.Helper.ToSelectEntities("HotelPorViaje", id);
            return PartialView(hoteles);
        }

        public ActionResult GridPlanillaHotel(Guid viajeid)
        {
            List<List<PlanillaHotelModel>> conjuntoplanillas = new List<List<PlanillaHotelModel>>();
            Services.ViajeHotelService viajehotelService = new ViajeHotelService();
            List<Entities.ViajeHotel> hoteles = viajehotelService.GetByViajeId(viajeid).ToList();
            foreach (var h in hoteles)
            {
                Entities.Hotel hotel = new Services.HotelService().GetByHotelId(h.HotelId);
                List<Entities.Habitacion> _habitaciones = new HabitacionService().GetByHotelId(h.HotelId).ToList();
                List<Models.PlanillaHotelModel> _planillahotelmodel = new List<Models.PlanillaHotelModel>();
                foreach (var item in _habitaciones)
                {
                    PlanillaHotelModel planillahotellinea = new Models.PlanillaHotelModel(item.HabitacionId, hotel.Nombre, viajeid);
                    _planillahotelmodel.Add(planillahotellinea);
                }
                conjuntoplanillas.Add(_planillahotelmodel.OrderBy(pl => pl.Habitacion.Tipo).ToList());
            }            
            return PartialView(conjuntoplanillas);
        }

        public ActionResult GridPlanillaHotelPrint(Guid viajeid, Guid planillaid)
        {
            List<List<PlanillaHotelPrintModel>> conjuntoplanillas = new List<List<PlanillaHotelPrintModel>>();
            Services.ViajeHotelService viajehotelService = new ViajeHotelService();
            List<Entities.ViajeHotel> hoteles = viajehotelService.GetByViajeId(viajeid).ToList();
            foreach (var h in hoteles)
            {
                Entities.Hotel hotel = new Services.HotelService().GetByHotelId(h.HotelId);
                List<Entities.Habitacion> _habitaciones = new HabitacionService().GetByHotelId(h.HotelId).ToList();
                List<Models.PlanillaHotelPrintModel> _planillahotelmodel = new List<Models.PlanillaHotelPrintModel>();
                foreach (var item in _habitaciones)
                {
                    PlanillaHotelPrintModel planillahotellinea = new Models.PlanillaHotelPrintModel(item.HabitacionId, planillaid, hotel.Nombre, viajeid);
                    _planillahotelmodel.Add(planillahotellinea);
                }
                conjuntoplanillas.Add(_planillahotelmodel.OrderBy(pl => pl.Habitacion.Tipo).ToList());
            }
            return PartialView(conjuntoplanillas);
        }

        public ActionResult GridPlanillaHotelDetallePrint(Guid viajeid, Guid planillaid)
        {
            List<List<PlanillaHotelPrintModel>> conjuntoplanillas = new List<List<PlanillaHotelPrintModel>>();
            Services.ViajeHotelService viajehotelService = new ViajeHotelService();
            List<Entities.ViajeHotel> hoteles = viajehotelService.GetByViajeId(viajeid).ToList();
            foreach (var h in hoteles)
            {
                Entities.Hotel hotel = new Services.HotelService().GetByHotelId(h.HotelId);
                List<Entities.Habitacion> _habitaciones = new HabitacionService().GetByHotelId(h.HotelId).ToList();
                List<Models.PlanillaHotelPrintModel> _planillahotelmodel = new List<Models.PlanillaHotelPrintModel>();
                foreach (var item in _habitaciones)
                {
                    PlanillaHotelPrintModel planillahotellinea = new Models.PlanillaHotelPrintModel(item.HabitacionId, planillaid, hotel.Nombre, viajeid);
                    _planillahotelmodel.Add(planillahotellinea);
                }
                conjuntoplanillas.Add(_planillahotelmodel.OrderBy(pl => pl.Habitacion.Tipo).ToList());
            }
            return PartialView(conjuntoplanillas);
        }

        public ActionResult PartialResumenGridPlanillaHotel(List<PlanillaHotelModel> planilla)
        {
            List<ResumenPlanillaModel> resumen = new List<ResumenPlanillaModel>();
            foreach (HabitacionTipo item in HabitacionTipoMethod.GetAllHabitacionTipo())
            {
                resumen.Add(new ResumenPlanillaModel(planilla, item.Id));
            }
          
            double _total = 0;
            foreach (var item in resumen)
            {
                _total += item.Subtotal;
            }
            ViewData["Total"] = _total;
            return PartialView(resumen);
        }

        public ActionResult PartialGridResumenPlanillaHotelPrint(List<PlanillaHotelPrintModel> planilla)
        {
            List<ResumenPlanillaPrintModel> resumen = new List<ResumenPlanillaPrintModel>();
            foreach (HabitacionTipo item in HabitacionTipoMethod.GetAllHabitacionTipo())
            {
                resumen.Add(new ResumenPlanillaPrintModel(planilla, item.Id));
            }
            //resumen.Add(new ResumenPlanillaPrintModel(planilla, eTipoHabitacion.Single));
            //resumen.Add(new ResumenPlanillaPrintModel(planilla, eTipoHabitacion.Doble));
            //resumen.Add(new ResumenPlanillaPrintModel(planilla, eTipoHabitacion.Matrimonial));
            //resumen.Add(new ResumenPlanillaPrintModel(planilla, eTipoHabitacion.Triple));
            //resumen.Add(new ResumenPlanillaPrintModel(planilla, eTipoHabitacion.Cuadruple));
            double _total = 0;
            foreach (var item in resumen)
            {
                _total += item.Subtotal;
            }
            ViewData["TotalHotel"] = _total;
            ViewData["Hotel"] = planilla.FirstOrDefault().Hotel;
            return PartialView(resumen);
        }

        public ActionResult PlanillaServicios()
        {
            MATContext.ServiciosSeleccionados = new List<Entities.PlanillaServicioItem>();
            return View();
        }

        public ActionResult PartialGridServiciosAdmin()
        {
            Services.ServicioService servicioService = new Services.ServicioService();
            List<MAT.Entities.Servicio> servicios = servicioService.GetAll().Where(se => se.TipoServicio == 1).OrderBy(ser => ser.Descripcion).ToList();
            return PartialView(servicios);
        }

        public bool AgregarPlanillaServicioItem(Guid servicioid, Guid viajeid)
        {
            bool result = false;
            try
            {
                if (MATContext.ServiciosSeleccionados == null) MATContext.ServiciosSeleccionados = new List<Entities.PlanillaServicioItem>();
                Entities.PlanillaServicioItem item = new Entities.PlanillaServicioItem();
                item.PlanillaServicioItemId = Guid.NewGuid();
                item.ServicioId = servicioid;
                MATContext.ServiciosSeleccionados.Add(item);
                result = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                result = false;
            }
            return result;
        }

        public bool ActualizarPlanillaServicioItem(Guid planillaservicioitemid, string cantidad, string subtotal)
        {
            bool result = false;
            try
            {
                int _cantidad = 0;
                double _subtotal = 0;
                if (!string.IsNullOrEmpty(cantidad))  _cantidad = Convert.ToInt32(cantidad);
                if (!string.IsNullOrEmpty(subtotal)) _subtotal = Convert.ToDouble(subtotal);
                Entities.PlanillaServicioItem item = MATContext.ServiciosSeleccionados.Where(se => se.PlanillaServicioItemId == planillaservicioitemid).FirstOrDefault();
                item.Cantidad = _cantidad;
                item.Subtotal = _subtotal;
                result = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

                result = false;
            }
            return result;
        }

        public bool CrearColeccionPlanillas()
        {
            bool result = false;
            try
            {
                List<Entities.Planilla> coleccion = new List<Entities.Planilla>();
                MATContext.ColeccionPlanillas = coleccion;
                result = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                result = false;
            }
            return result;
        }

        public bool GenerarPlanilla(string total, string viajeid)
        {
            bool result = false;
            try
            {
                double _total = Convert.ToDouble(total);
                Guid _viajeid = Guid.Parse(viajeid);
                Entities.Planilla planilla = new Entities.Planilla();
                planilla.PlanillaId = Guid.NewGuid();
                planilla.ViajeId = _viajeid;
                planilla.Total = _total;
                planilla.FechaRegistro = DateTime.Now;
                MATContext.Planilla = planilla;
                MATContext.HabitacionesPlanilla = new List<Entities.PlanillaHabitacionItem>();
                result = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

                result = false; ;
            }
            return result;
        }

        public bool AgregarHabitacionesItem(Guid habitacionid, int dias, double subtotal)
        {
            bool result = false;
            try
            {
                Entities.PlanillaHabitacionItem habitacionitem = new Entities.PlanillaHabitacionItem();
                habitacionitem.PlanillaHabitacionItemId = Guid.NewGuid();
                habitacionitem.HabitacionId = habitacionid;
                habitacionitem.PlanillaId = MATContext.Planilla.PlanillaId;
                habitacionitem.Cantidad = dias;
                habitacionitem.Subtotal = subtotal;
                MATContext.HabitacionesPlanilla.Add(habitacionitem);
                result = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

                result = false;
            }
            return result;
        }

        public bool GenerarPlanillaServicio(string total, string viajeid)
        {
            bool result = false;
            try
            {
                Services.PlanillaHabitacionItemService habitacionitemService = new PlanillaHabitacionItemService();
                double _total = Convert.ToDouble(total);
                Guid _viajeid = Guid.Parse(viajeid);
                Services.PlanillaService planillaService = new PlanillaService();
                Services.PlanillaServicioItemService planillaitemService = new PlanillaServicioItemService();

                MATContext.Planilla.Total += _total;
                planillaService.Insert(MATContext.Planilla);
                foreach (var itemhabitacion in MATContext.HabitacionesPlanilla)
                {
                    habitacionitemService.Insert(itemhabitacion);
                }
                foreach (var item in MATContext.ServiciosSeleccionados)
                {
                    item.PlanillaId = MATContext.Planilla.PlanillaId;
                    planillaitemService.Insert(item);
                }
                result = true;
            }
            catch 
            {

                result = false;
            }
            return result;
        }

        public bool AgregarTotalHabitaciones(string total)
        {
            bool result = false;
            try
            {
                double _total = Convert.ToDouble(total);
                MATContext.Planilla.Total = _total;
                result = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                result = false;
            }
            return result;
        }

        public ActionResult GridPlanillaServicioItemContext()
        {
            return PartialView(MATContext.ServiciosSeleccionados);
        }

        public ActionResult PlanillasGeneradas()
        {
            return View();
        }

        public ActionResult GridPlanillasGeneradas(Guid viajeid)
        {
            Services.PlanillaService planillaService = new PlanillaService();
            List<Entities.Planilla> planillas = planillaService.GetAll().Where(pl => pl.ViajeId == viajeid).OrderBy(pl => pl.FechaRegistro).ToList();
            return PartialView(planillas);
        }

        public ActionResult ImprimirPlanilla(Guid planillaid)
        {
            
            return PartialView(new Services.PlanillaService().GetByPlanillaId(planillaid));
        }

        public ActionResult ImprimirPlanillaDetalle(Guid planillaid)
        {

            return PartialView(new Services.PlanillaService().GetByPlanillaId(planillaid));
        }

        public ActionResult GridPlanillaServiciosItemPrint(Guid planillaid)
        {
            Services.PlanillaServicioItemService servicioitemService = new PlanillaServicioItemService();
            List<Entities.PlanillaServicioItem> servicioitems = servicioitemService.GetByPlanillaId(planillaid).ToList();
            return PartialView(servicioitems);
        }

        public ActionResult DeletePlanilla(Guid id)
        {
            Services.PlanillaServicioItemService planillaservicioService = new PlanillaServicioItemService();
            Services.PlanillaHabitacionItemService planillahotelService = new PlanillaHabitacionItemService();
            Services.PlanillaService planillaService = new PlanillaService();
            List<Entities.PlanillaServicioItem> servicios = planillaservicioService.GetByPlanillaId(id).ToList();
            for (int i = servicios.Count-1; i > -1; i--)
            {
                var item = servicios[i];
                planillaservicioService.Delete(item.PlanillaServicioItemId);
            }

            List<Entities.PlanillaHabitacionItem> habitaciones = planillahotelService.GetByPlanillaId(id).ToList();
            for (int i = habitaciones.Count-1; i > -1; i--)
            {
                var item = habitaciones[i];
                planillahotelService.Delete(item.PlanillaHabitacionItemId);
            }
            planillaService.Delete(id);
            return RedirectToAction("PlanillasGeneradas");
        }

        public ActionResult EditarPlanilla(Guid id)
        {
            return View(new Services.PlanillaService().GetByPlanillaId(id));
        }

        public ActionResult GridPlanillaHotelDetalleEdit(Guid planillaid, Guid viajeid)
        {
            List<List<PlanillaHotelPrintModel>> conjuntoplanillas = new List<List<PlanillaHotelPrintModel>>();
            Services.ViajeHotelService viajehotelService = new ViajeHotelService();
            List<Entities.ViajeHotel> hoteles = viajehotelService.GetByViajeId(viajeid).ToList();
            foreach (var h in hoteles)
            {
                Entities.Hotel hotel = new Services.HotelService().GetByHotelId(h.HotelId);
                List<Entities.Habitacion> _habitaciones = new HabitacionService().GetByHotelId(h.HotelId).ToList();
                List<Models.PlanillaHotelPrintModel> _planillahotelmodel = new List<Models.PlanillaHotelPrintModel>();
                foreach (var item in _habitaciones)
                {
                    PlanillaHotelPrintModel planillahotellinea = new Models.PlanillaHotelPrintModel(item.HabitacionId, planillaid, hotel.Nombre, viajeid);
                    _planillahotelmodel.Add(planillahotellinea);
                }
                conjuntoplanillas.Add(_planillahotelmodel.OrderBy(pl => pl.Habitacion.Tipo).ToList());
            }
            return PartialView(conjuntoplanillas);
        }

        public ActionResult GridPlanillaServiciosItemEdit(Guid planillaid)
        {
            Services.PlanillaServicioItemService servicioitemService = new PlanillaServicioItemService();
            List<Entities.PlanillaServicioItem> servicioitems = servicioitemService.GetByPlanillaId(planillaid).ToList();
            return PartialView(servicioitems);
        }

        public bool GuardarDatosPlanilla(Guid planillaid, string fecha, string total)
        {
            bool result = false;
            try
            {
                DateTime _fecha = !string.IsNullOrEmpty(fecha) ? Convert.ToDateTime(fecha) : DateTime.Now;
                Double _total = !string.IsNullOrEmpty(total) ? Convert.ToDouble(total) : 0;
                Services.PlanillaService planillaService = new PlanillaService();
                Entities.Planilla planilla = planillaService.GetByPlanillaId(planillaid);
                planilla.FechaRegistro = _fecha;
                planilla.Total = _total;
                planillaService.Update(planilla);
                result = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                result = false;
            }
            return result;
        }

        public bool GuardarDatosItem(Guid itemid, string dias, string subtotal)
        {
            bool result = false;
            try
            {
                PlanillaHabitacionItemService habitacionitemService = new PlanillaHabitacionItemService();
                Entities.PlanillaHabitacionItem habitacionitem = habitacionitemService.GetByPlanillaHabitacionItemId(itemid);
                habitacionitem.Cantidad = !string.IsNullOrEmpty(dias) ? Convert.ToInt32(dias) : 0 ;
                habitacionitem.Subtotal = !string.IsNullOrEmpty(subtotal) ? Convert.ToDouble(subtotal) : 0;
                habitacionitemService.Update(habitacionitem);
                result = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                result = false;
            }
            return result;
        }

        public bool GuardarDatosServiciosItem(Guid id, string cantidad, string subtotal)
        {
            bool result = false;
            try
            {
                Services.PlanillaServicioItemService servicioitemService = new PlanillaServicioItemService();
                Entities.PlanillaServicioItem servicioitem = servicioitemService.GetByPlanillaServicioItemId(id);
                servicioitem.Cantidad = !string.IsNullOrEmpty(cantidad) ? Convert.ToInt32(cantidad) : 0;
                servicioitem.Subtotal = !string.IsNullOrEmpty(subtotal) ? Convert.ToDouble(subtotal) : 0;
                servicioitemService.Update(servicioitem);
                result = true;
            }
            catch (Exception )
            {
                result = false;
            }
            return result;
        }

        [Authorize]
        public ActionResult AuditoriaFacturas() {
            return View();
        }

        [Authorize]
        public ActionResult Logs(string correlationId = null)
        {
            if (!IsAdminUser()) return RedirectToAction("Index", "Home");

            var logs = MATLogger.GetRecentLogs(1000);

            if (!string.IsNullOrWhiteSpace(correlationId))
                logs = logs.Where(l => l.Contains(correlationId)).ToList();

            ViewBag.CorrelationId = correlationId;
            ViewBag.Logs = logs;
            return View();
        }

        [Authorize]
        public ContentResult LogsRaw(string correlationId = null)
        {
            if (!IsAdminUser()) return Content("Sin permisos");

            var logs = MATLogger.GetRecentLogs(1000);
            if (!string.IsNullOrWhiteSpace(correlationId))
                logs = logs.Where(l => l.Contains(correlationId)).ToList();

            return Content(string.Join("\n", logs), "text/plain", Encoding.UTF8);
        }

        [Authorize]
        public ActionResult ErrorLog()
        {
            if (!IsAdminUser()) return RedirectToAction("Index", "Home");
            return View();
        }

        [Authorize]
        public JsonResult ErrorLogJson(string correlationId = null, string fechaDesde = null)
        {
            if (!IsAdminUser())
                return Json(new { ok = false, mensaje = "Sin permisos" }, JsonRequestBehavior.AllowGet);

            DateTime? fecha = null;
            if (!string.IsNullOrWhiteSpace(fechaDesde))
            {
                DateTime parsed;
                if (DateTime.TryParse(fechaDesde, out parsed))
                    fecha = parsed;
            }

            var dt = DbErrorLogger.GetRecent(200, correlationId, fecha);
            var lista = new System.Collections.Generic.List<object>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var row = dt.Rows[i];
                lista.Add(new
                {
                    id     = row["Id"] == DBNull.Value     ? "" : row["Id"].ToString(),
                    fecha  = row["FechaHora"] == DBNull.Value ? "" : Convert.ToDateTime(row["FechaHora"]).ToString("dd/MM/yyyy HH:mm:ss"),
                    corrId = row["CorrelationId"] == DBNull.Value ? "" : row["CorrelationId"].ToString(),
                    tipo   = row["Tipo"] == DBNull.Value   ? "" : row["Tipo"].ToString(),
                    msg    = row["Mensaje"] == DBNull.Value ? "" : row["Mensaje"].ToString(),
                    stack  = row["StackTrace"] == DBNull.Value ? "" : row["StackTrace"].ToString(),
                    url    = row["Url"] == DBNull.Value    ? "" : row["Url"].ToString(),
                    user   = row["Usuario"] == DBNull.Value ? "" : row["Usuario"].ToString(),
                    imp    = row["Importancia"] == DBNull.Value ? "1" : row["Importancia"].ToString()
                });
            }

            return Json(new { ok = true, errores = lista }, JsonRequestBehavior.AllowGet);
        }

        private bool IsAdminUser()
        {
            try
            {
                return Roles.IsUserInRole(User.Identity.Name, "Administrador");
            }
            catch
            {
                // RoleManager puede no estar configurado; en ese caso permitir a cualquier usuario autenticado
                return User.Identity.IsAuthenticated;
            }
        }

        [Authorize]
        public JsonResult AuditFactura(string dateFrom, string dateTo)
        {
            if (!IsAdminUser())
                return Json(new List<AuditFactura>(), JsonRequestBehavior.AllowGet);

            var listFactura = new List<AuditFactura>();
            SqlParameter[] _dbParams = new SqlParameter[]
                        {
                            DBHelper.MakeParam("@dateFrom", SqlDbType.VarChar, 0, dateFrom),
                            DBHelper.MakeParam("@dateTo", SqlDbType.VarChar, 0, dateTo)
                        };
            using (SqlDataReader _Reader = DBHelper.ExecuteDataReader("usp_MAT_Admin_AuditoriaFacturas", _dbParams))
            {
                while (_Reader.Read())
                {
                    AuditFactura _item = new Models.AuditFactura();
                    _item.ID = _Reader["ID"].ToString();
                    _item.Accion = _Reader["Accion"].ToString();
                    _item.Descripcion = _Reader["Descripcion"].ToString();
                    _item.Fecha = _Reader["Fecha"].ToString();
                    _item.Cliente = _Reader["Cliente"].ToString();
                    _item.Vendedor = _Reader["Vendedor"].ToString();
                    listFactura.Add(_item);
                }
            }

            // Devolver el array directamente; Json() ya serializa (evita doble JSON string).
            return Json(listFactura, JsonRequestBehavior.AllowGet);
        }
    }
}
