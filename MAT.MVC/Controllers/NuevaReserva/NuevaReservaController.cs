using AutoMapper;
using MAT.MVC.Integration;
using MAT.MVC.Integration.BackendApi.Models;
using MAT.MVC.Models;
using MAT.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
            //var tt = await _backendAPI.GetAllImageAsync();

            //List<ReservaStandard> Model = new List<ReservaStandard>();
            var Model = new NuevaReservaModel();

            try
            {
                Model.ViajeId = viajeid;                
                List<ResultPasajeDto> resultPasajeDto = await _backendAPI.GetListOfPasajesByViajeID(viajeid.ToString());
                Model.Reservas = Mapper.Map<List<ReservaStandard>>(resultPasajeDto);
                var resultDetalleViaje = await _backendAPI.GetDetalleViajeAsync(viajeid.ToString());
                Model.DetalleViaje = Mapper.Map<DetalleViaje>(resultDetalleViaje);
                ViewBag.PreReservas = ReservaMethod.GetPreReservaVencidas(viajeid.ToString());
                ViewBag.ListaEspera = ListaEsperaModel.Method.GetCountListaEsperaByViajeId(viajeid.ToString()).Tables[0].Rows[0]["CountListaEspera"];

            }
            catch (Exception e)
            {
                ViewBag.MsgError = e.Message;
            }

            return View(Model);
        }

        public async Task<JsonResult> QuickClienteSearchAsync(string query)
        {
            var result =  new List<PersonaDto>();
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
    }
}
