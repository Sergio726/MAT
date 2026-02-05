using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MAT.MVC.Models;
using MAT.MVC.Infrastructure;

namespace MAT.MVC.Controllers.Itinerario
{
    public class ItinerarioController : Controller
    {
        [Authorize]
        [HttpGet]
        public JsonResult GetByViajeID(string viajeId)
        {
            string sMensaje = "";
            List<ItinerarioViajeModel> items = new List<ItinerarioViajeModel>();

            try
            {
                items = ItinerarioMethod.GetItinerarioByViajeID(viajeId);
            }
            catch (Exception e)
            {
                sMensaje = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "ItinerarioController.GetByViajeID");
            }

            return Json(new
            {
                items,
                sMensaje
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetAllParadas()
        {
            string sMensaje = "";
            List<ItinerarioModel> items = new List<ItinerarioModel>();

            try
            {
                items = ItinerarioMethod.GetAllItinerarios();
            }
            catch (Exception e)
            {
                sMensaje = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "ItinerarioController.GetAllParadas");
            }

            return Json(new
            {
                items,
                sMensaje
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult AddParada(string nombre, string descripcion)
        {
            string[] sResult = new string[2];
            try
            {
                sResult = ItinerarioMethod.InsertItinerario(nombre, descripcion);
            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "ItinerarioController.AddParada");
            }

            return Json(new
            {
                ID = sResult[0],
                Mensaje = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult AddStep(string viajeId, string itinerarioId, int orden, string horaAprox, int? duracionMin, string observacion)
        {
            string[] sResult = new string[2];
            try
            {
                sResult = ItinerarioMethod.InsertItinerarioViaje(viajeId, itinerarioId, orden, horaAprox, duracionMin, observacion);
            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "ItinerarioController.AddStep");
            }

            return Json(new
            {
                ID = sResult[0],
                Mensaje = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult UpdateStep(string itinerarioViajeId, string itinerarioId, int? orden, string horaAprox, int? duracionMin, string observacion)
        {
            string[] sResult = new string[2];
            try
            {
                sResult = ItinerarioMethod.UpdateItinerarioViaje(itinerarioViajeId, itinerarioId, orden, horaAprox, duracionMin, observacion);
            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "ItinerarioController.UpdateStep");
            }

            return Json(new
            {
                ID = sResult[0],
                Mensaje = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult DeleteStep(string itinerarioViajeId)
        {
            string[] sResult = new string[2];
            try
            {
                sResult = ItinerarioMethod.DeleteItinerarioViaje(itinerarioViajeId);
            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "ItinerarioController.DeleteStep");
            }

            return Json(new
            {
                ID = sResult[0],
                Mensaje = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult ReorderSteps(string itinerarioViajeId, int newOrden)
        {
            string[] sResult = new string[2];
            try
            {
                sResult = ItinerarioMethod.UpdateOrdenItinerarioViaje(itinerarioViajeId, newOrden);
            }
            catch (Exception e)
            {
                sResult[0] = "-1";
                sResult[1] = "Error: " + ErrorUtil.LogAndGetPublicMessage(e, "ItinerarioController.ReorderSteps");
            }

            return Json(new
            {
                ID = sResult[0],
                Mensaje = sResult[1]
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
