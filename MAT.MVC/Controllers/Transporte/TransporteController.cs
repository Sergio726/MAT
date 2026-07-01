using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MAT.MVC.Infrastructure;
using MAT.MVC.Models;

namespace MAT.MVC.Controllers.Transporte
{
    public class TransporteController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                List<TransporteModel> lTransporte = TransporteMethod.GetListTransporte();
                return View(lTransporte);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(ex, "TransporteController.Index");
                return View(new List<TransporteModel>());
            }
        }

        public ActionResult Create()
        {
            return View(new TransporteFormViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransporteFormViewModel model)
        {
            model.Normalize();
            if (!model.IsTipoValid())
            {
                ModelState.AddModelError("Tipo", "Seleccione un tipo de transporte válido.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                TransporteMethod.InsertTransporte(model);
                return RedirectToAction("Index", "Transporte");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(ex, "TransporteController.Create");
                return View(model);
            }
        }

        public ActionResult Edit(Guid Id)
        {
            try
            {
                var model = TransporteMethod.GetFormById(Id);
                if (model == null)
                {
                    TempData["Error"] = "El transporte solicitado no existe.";
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ErrorUtil.LogAndGetPublicMessage(ex, "TransporteController.Edit");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TransporteFormViewModel model)
        {
            model.Normalize();
            if (!model.TransporteId.HasValue || model.TransporteId.Value == Guid.Empty)
            {
                TempData["Error"] = "El transporte solicitado no existe.";
                return RedirectToAction("Index");
            }

            if (!model.IsTipoValid())
            {
                ModelState.AddModelError("Tipo", "Seleccione un tipo de transporte válido.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                TransporteMethod.UpdateTransporte(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(ex, "TransporteController.Edit");
                return View(model);
            }
        }

        public ActionResult Delete(Guid Id)
        {
            try
            {
                TransporteMethod.DeleteTransporte(Id);
            }
            catch (Exception ex)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(ex, "TransporteController.Delete");
                return PartialView("Error", msg);
            }

            return RedirectToAction("Index", "Transporte");
        }
    }
}
