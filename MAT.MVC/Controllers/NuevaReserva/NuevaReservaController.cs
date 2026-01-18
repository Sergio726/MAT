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
                ViewBag.MsgError = MATLogger.FormatExceptionToHtml(e);
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

        /// <summary>
        /// Busca presupuestos activos por DNI del cliente
        /// </summary>
        [HttpPost]
        public JsonResult BuscarPresupuestoPorDni(string dniCliente)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dniCliente))
                {
                    return Json(new { success = false, message = "El DNI es requerido." }, JsonRequestBehavior.AllowGet);
                }

                var presupuestos = PresupuestoMethod.GetByDni(dniCliente.Trim());

                if (presupuestos == null || presupuestos.Count == 0)
                {
                    return Json(new { success = true, hasPresupuesto = false, presupuestos = new List<object>() }, JsonRequestBehavior.AllowGet);
                }

                // Tomar el más reciente
                var presupuesto = presupuestos.OrderByDescending(p => p.FechaCreacion).FirstOrDefault();

                var resultado = new
                {
                    presupuestoId = presupuesto.PresupuestoID.ToString(),
                    codigoSeguimiento = presupuesto.CodigoSeguimiento,
                    montoPactado = presupuesto.MontoPactado,
                    vendedorOrigenNombre = presupuesto.VendedorOrigenNombre,
                    vendedorIdOrigen = presupuesto.VendedorIdOrigen.ToString(),
                    viajeId = presupuesto.ViajeId?.ToString(),
                    viajeDescripcion = presupuesto.ViajeDescripcion,
                    paqueteDescripcion = presupuesto.PaqueteDescripcion,
                    fechaCreacion = presupuesto.FechaCreacion.ToString("dd/MM/yyyy HH:mm"),
                    fechaExpiracion = presupuesto.FechaExpiracion.ToString("dd/MM/yyyy HH:mm"),
                    isExpirado = presupuesto.IsExpirado
                };

                return Json(new { success = true, hasPresupuesto = true, presupuesto = resultado }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en NuevaReservaController.BuscarPresupuestoPorDni: {ex.Message} - {ex.StackTrace}", 1);
                return Json(new { success = false, message = "Error al buscar presupuestos." }, JsonRequestBehavior.AllowGet);
            }
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
                    pasaje.PasajeroId = new Guid(pasajero.Id);                    
                    
                    pasaje.AdicionalesIds = pasajero.Adicionales.Select(x => new Guid(x.AdicionalId)).ToList();
                    if (pasajero.PasajeroAdulto != null && pasajero.PasajeroAdulto.Id != null)
                    {
                        pasaje.PasajeroAdultoId = new Guid(pasajero.PasajeroAdulto.Id);
                    }
                    else                    
                    {
                        pasaje.HabitacionId = new Guid(pasajero.Habitacion.Id);
                        pasaje.PasajeId = new Guid(pasajero.Butaca.PasajeId);
                        pasaje.ButacaId = new Guid(pasajero.Butaca.ButacaId);
                        pasaje.ButacaCodigo = pasajero.Butaca.Codigo;
                        pasaje.ButacaPrecio = Convert.ToDecimal(pasajero.Butaca.Precio);
                    }
                    datosPasajes.Add(pasaje);
                }

                var datosPago = new ReservaDto();
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

                try
                {
                    var result = await _backendAPI.ReservarPasajes(datosPago);
                    sEstadoFactura = result.Factura.EstadoDescripcion;
                    
                    // Si hay un presupuesto asociado, actualizar su estado
                    if (!string.IsNullOrEmpty(reserva.PresupuestoId))
                    {
                        try
                        {
                            Guid presupuestoId = new Guid(reserva.PresupuestoId);
                            Guid? facturaId = !string.IsNullOrEmpty(result.Factura.Id) ? new Guid(result.Factura.Id) : (Guid?)null;
                            Guid vendedorCierre = MATContext.CurrentVendedor.VendedorId;
                            
                            PresupuestoMethod.UpdateEstado(
                                presupuestoId,
                                eEstadoPresupuesto.Cerrado,
                                facturaId,
                                vendedorCierre
                            );
                            
                            MATLogger.Log($"Presupuesto {presupuestoId} cerrado exitosamente. Factura: {facturaId}", 0);
                        }
                        catch (Exception exPresupuesto)
                        {
                            // Log el error pero no interrumpir el flujo de la venta
                            MATLogger.Log($"Error al actualizar presupuesto: {exPresupuesto.Message}", 1);
                        }
                    }
                }
                catch (Exception ex) { 
                    string mess = ex.Message;
                    sEstadoFactura += mess;
                }


                return sEstadoFactura;
            }
            catch (Exception ex)
            {
                // No exponer stacktrace al cliente
                var safe = MAT.MVC.Infrastructure.ErrorUtil.LogAndGetPublicMessage(ex, "NuevaReservaController.PagarReserva");
                return "Error: " + safe;
            }
        }
    }
}
