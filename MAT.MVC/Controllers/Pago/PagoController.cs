using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Utilities;
using MAT.MVC.Infrastructure.Data;
using MAT.MVC.Models;

namespace MAT.MVC.Controllers
{
    public class PagoController : Controller
    {
        //
        // GET: /Pago/

      
        public ActionResult Edit(Guid id)
        {
            Pago p = PagoDataAccess.GetPagoById(id);
            return View(p);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection form)
        {
            Pago p = PagoDataAccess.GetPagoById(id);
            Helper.FillEntity<Pago>(ref p, form);
            PagoDataAccess.UpdatePago(p);
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
