using MAT.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.MVC.Infrastructure;

namespace MAT.MVC.Controllers
{
    public class NotaCreditoController : Controller
    {
        //
        // GET: /NotaCredito/

        public ActionResult partialMovimientoNotaCredito(Guid ClienteID)
        {
            List<MovimientoNotaCredito> _model = new List<MovimientoNotaCredito>();

            try
            {
                string sCliente = "";
                _model = NotaCreditoMethod.GetMovimientoNotaCreditoByClienteID(ClienteID, out sCliente);
                ViewBag.Cliente = sCliente;
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return PartialView(_model);
        }

        public ActionResult partialNotaCredito(Guid NotaID)
        {
            NotaCreditoModel _model = new NotaCreditoModel();

            try
            {
                _model = NotaCreditoMethod.GetNotaByID(NotaID);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
            }
            return PartialView(_model);
        }

        public JsonResult getCreditoDisponible(Guid ClienteID)
        {
            string[] sResult = new string[2];
            try
            {
                string sCliente = "";
                List<MovimientoNotaCredito> _model = NotaCreditoMethod.GetMovimientoNotaCreditoByClienteID(ClienteID, out sCliente);
                double dCred = 0;
                if (_model.Count > 0)
                    dCred = _model.Select(l => l.Monto).Sum();

                sResult[0] = "Done.";
                sResult[1] = dCred.ToString();
            }
            catch (Exception e)
            {
                sResult[0] = "Error.";
                sResult[1] = ErrorUtil.LogAndGetPublicMessage(e, "NotaCreditoController.getCreditoDisponible");
            }
            return Json(sResult, JsonRequestBehavior.AllowGet);
        }

    }
}
