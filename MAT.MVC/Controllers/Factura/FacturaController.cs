using MAT.MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
                ViewBag.Error = e.Message + " " + e.StackTrace.ToString();
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
                ViewBag.Error = e.Message;
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
                ViewBag.Error = e.Message;
                return PartialView();
            }
        }

        [Authorize]
        public ActionResult FacturaListByViajeID(string sViajeID, string sPaquete)
        {

            ViewBag.ViajeID = sViajeID;
            ViewBag.PaqueteDescripcion = sPaquete;
            return View();
        }


    }
}
