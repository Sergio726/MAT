using AutoMapper;
using MAT.Entities;
using MAT.MVC.Common;
using MAT.MVC.Integration;
using MAT.MVC.Integration.BackendApi.Models;
using MAT.MVC.Models;
using MAT.Services;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MAT.Enums.SharedModels;
using MAT.Enums;


namespace MAT.MVC.Controllers.NuevaReserva
{
    public class NuevaReservaController : Controller
    {
        //
        // GET: /NuevaReserva/

        private BackendAPI _backendAPI;

        public NuevaReservaController()
        {
            _backendAPI = new BackendAPI();
        }
        [Authorize]
        public async Task<ActionResult> Index(Guid viajeid)
        {
            try
            {
                var Model = await NuevaReservaModel.CreateAsync(viajeid);
                return View(Model);
            }
            catch (Exception e)
            {
                var Model = new NuevaReservaModel(viajeid);                
                Model.Reservas = new List<ReservaStandard>();
                ViewBag.MsgError = e.Message;
                return View(Model);
            }
        }

        public async Task<JsonResult> QuickClienteSearchAsync(string query)
        {
            var result = new List<PersonaDto>();
            try
            {
                result = await _backendAPI.SearchClientAsync(query);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> QuickPersonaSearchAsync(string query)
        {
            var result = new List<PersonaDto>();
            try
            {
                result = await _backendAPI.SearchPersonAsync(query);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetHabitacionesDisponiblesByViaje(string viajeId)
        {
            var result = new List<HabitacionDto>();
            try
            {
                result = await _backendAPI.GetHabitacionesDisponiblesByViaje(viajeId);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAdicionalesByViaje(string viajeId)
        {
            var result = new List<AdicionalDto>();
            try
            {
                result = await _backendAPI.GetAdicionalesByViaje(viajeId);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SeleccionarPasajero(string entityId, string source)
        {
            if (!string.IsNullOrEmpty(source)) ViewData["source"] = source;
            if (!string.IsNullOrEmpty(entityId)) ViewData["entityId"] = entityId;
            return PartialView();
        }

        [HttpPost]
        public ActionResult ReservarPasajes(DatosReserva reserva)
        {
            try
            {
                var Model = new NuevaReservaModel(reserva.ViajeId);
                Model.Reserva = reserva;                
                ViewBag.MontoFactura = reserva.PrecioTotal;
                return PartialView("FormReserva", Model);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return PartialView("FormReserva");
            }
        }

        //29-10-2024: nuevo proceso para la reserva de pasajes, API
        [HttpPost]
        //public string FormReserva(string cliente, string tipopago, string condicion, string recibo, string TransaccionId, string nroFactura,
        //                          string observaciones, string jsonobject, string descuento = "", string monto = "0", string montoFactura = "0", string listmenores = "",
        //                          string tutormenor = "", string viajeid = "", string detalledescuento = "",
        //                          string MontoRecibido = "", string MontoRecibidoMonedaTipo = "1", string MontoEquivalente = "", string MontoEquivalenteMonedaTipo = "", string MontoEquivalenteCotizacion = "", string ViajeMonedaTipo = "1")
        public string PagarReserva(DatosReserva reserva)
        {
            try
            {
                var datosPasajes = new List<TablePasaje>();
                foreach(var pasajero in reserva.Pasajeros)
                {
                    var pasaje = new TablePasaje();
                    pasaje.PasajeId = new Guid(pasajero.Butaca.PasajeId);
                    pasaje.PasajeroId = new Guid(pasajero.Id);
                    pasaje.ButacaId = new Guid(pasajero.Butaca.ButacaId);
                    pasaje.ButacaPrecio = Convert.ToDecimal(pasajero.Butaca.Precio);
                    pasaje.ButacaCodigo = pasajero.Butaca.Codigo;
                    pasaje.AdicionalesIds = pasajero.Adicionales.Select(x => new Guid(x.AdicionalId)).ToList();
                    pasaje.HabiactionId = new Guid(pasajero.Habitacion.Id);

                    datosPasajes.Add(pasaje);
                }
                string sEstadoFactura = "";
                DataSet ds = ReservaMethod.NuevoSPPago(reserva, datosPasajes);

                if(ds.Tables.Count > 0 && ds.Tables[0]?.Rows[0]["Result"]?.ToString() == "Done.")
                {
                    sEstadoFactura = ds.Tables[0]?.Rows[0]["EstadoFactura"].ToString();
                }

                return sEstadoFactura;
            }
            catch (Exception ex)
            {
                MATLogger.Log(String.Format("{0} {1}", ex.Message, ex.StackTrace), 1);
                return "Error";
            }
        }
    }
}
