using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Services;
using MAT.Utilities;
using MAT.MVC.Models;

namespace MAT.MVC.Controllers
{
    public class PagoController : Controller
    {
        //
        // GET: /Pago/

      
        public ActionResult Edit(Guid id)
        {
            Pago p = new PagoService().GetByPagoId(id);
            return View(p);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection form)
        {
            PagoService pSrv = new PagoService();
            Pago p = pSrv.GetByPagoId(id);
            Helper.FillEntity<Pago>(ref p, form);
            pSrv.Update(p);
            return RedirectToAction("Index");
        }

        [Authorize]
        public JsonResult GetPagoDetalleByPagoID(Guid PagoID)
        {
            PagoDetalle oPagoDetalle = PagoMethod.GetPagoDetalleByPagoID(PagoID);
            return Json(oPagoDetalle, JsonRequestBehavior.AllowGet);
        }
    }
}
