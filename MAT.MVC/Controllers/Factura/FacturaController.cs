using MAT.MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MAT.MVC.Infrastructure;

namespace MAT.MVC.Controllers.Factura
{
    public class FacturaController : Controller
    {
        //
        // GET: /Factura/

        [Authorize]
        public ActionResult Index()
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
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "FacturaController.Index");
            }

            return View();
        }

        public JsonResult PersonaAutocomplete(string sParam) 
        {
            List<PersonaClienteModel> lSearch = new List<PersonaClienteModel>();
            lSearch = MAT.MVC.Models.PersonaClienteMethod.PersonaClienteSearchByNombreDNI(sParam);

            return Json(lSearch, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult FacturaResultSearch(string sPersonaID = "", string sViajeID = "")
        {
            List<FacturaStandard> LFactura = new List<FacturaStandard>();
            try
            {
                sPersonaID = (sPersonaID == "" ? null : sPersonaID);
                sViajeID = (sViajeID == "" ? null : sViajeID);
                LFactura = FacturaMetod.FacturaSearch(sPersonaID, sViajeID);

                var jsonPatientList = JsonConvert.SerializeObject(LFactura);
                ViewBag.sbDataSetJson = jsonPatientList.ToString();

                return PartialView();
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "FacturaController.FacturaResultSearch");
                ViewBag.sbDataSetJson = "[]";
                return PartialView();
            }
        }

        [Authorize]
        public ActionResult FacturaMoreDetails(string sFacturaID)
        {
            try
            {
                //load pasajeros

                ViewBag.lPasajeros = MAT.MVC.Models.FacturaMetod.GetMoreDetailsByFacturaID(sFacturaID);
                return PartialView();
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "FacturaController.FacturaMoreDetails");
                return PartialView();
            }
        }

        [Authorize]
        public ActionResult FacturaListByViajeID(string sViajeID, string sPaquete)
        {
            ViewBag.ViajeID = sViajeID;
            ViewBag.PaqueteDescripcion = sPaquete;
            
            // Obtener información del viaje
            if (!string.IsNullOrEmpty(sViajeID))
            {
                try
                {
                    var viaje = ViajeMethod.ViajeByViajeID(sViajeID);
                    if (viaje != null)
                    {
                        ViewBag.ViajeDescripcion = viaje.Descripcion;
                        ViewBag.ViajeFechaSalida = viaje.FechaSalida;
                    }
                }
                catch
                {
                    // Si hay error, dejar los valores vacíos
                    ViewBag.ViajeDescripcion = "";
                    ViewBag.ViajeFechaSalida = "";
                }
            }
            
            return View();
        }


    }
}
