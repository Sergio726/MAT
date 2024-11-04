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
using MAT.MVC.Integration.BackendApi;


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

        
        [HttpPost]
        public async Task<string> PagarReserva(DatosReserva reserva)
        {
            try
            {
                var datosPasajes = new List<PasajeDto>();
                foreach (var pasajero in reserva.Pasajeros)
                {
                    var pasaje = new PasajeDto();
                    pasaje.PasajeId = new Guid(pasajero.Butaca.PasajeId);
                    pasaje.PasajeroId = new Guid(pasajero.Id);
                    pasaje.ButacaId = new Guid(pasajero.Butaca.ButacaId);
                    pasaje.ButacaCodigo = pasajero.Butaca.Codigo;
                    pasaje.ButacaPrecio = Convert.ToDecimal(pasajero.Butaca.Precio);
                    pasaje.HabitacionId = new Guid(pasajero.Habitacion.Id);
                    pasaje.AdicionalesIds = pasajero.Adicionales.Select(x => new Guid(x.AdicionalId)).ToList();

                    datosPasajes.Add(pasaje);
                }

                var datosPago = new PagoDto();
                datosPago.ViajeId = reserva.ViajeId;
                datosPago.VendedorId = MATContext.CurrentVendedor.VendedorId;
                datosPago.ClienteId = new Guid(reserva.Cliente.Id);
                datosPago.Observaciones = reserva.Pago.Observaciones;
                datosPago.Condicion = reserva.Pago.Condition;
                datosPago.MonedaTipo = reserva.Pago.ViajeMonedaTipo;
                datosPago.DescuentoDetalle = reserva.Pago.Detalledescuento;
                datosPago.DescuentoMonto = reserva.Pago.Descuento;
                datosPago.Monto = reserva.Pago.Monto;
                datosPago.NroRecibo = reserva.Pago.Recibo;
                datosPago.TransaccionId = reserva.Pago.TransaccionId;
                datosPago.TipoPago = reserva.Pago.TipoPago;
                datosPago.NroFactura = reserva.Pago.NroFactura;
                datosPago.MontoRecibidoMonedaTipo = reserva.Pago.MontoRecibidoMonedaTipo.ToString();
                datosPago.MontoEquivalente = reserva.Pago.MontoEquivalente;
                datosPago.MontoEquivalenteCotizacion = Convert.ToDecimal(reserva.Pago.MontoEquivalenteCotizacion);
                datosPago.Pasajes = datosPasajes;

                string sEstadoFactura = "";

                var result = await _backendAPI.PagarPasajes(datosPago);
                if (result.Result == "Done.")
                    sEstadoFactura = result.EstadoFactura;
                else
                    sEstadoFactura = result.Result;
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
