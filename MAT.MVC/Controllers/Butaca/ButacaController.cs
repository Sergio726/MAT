using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MAT.Entities;
using MAT.MVC.Infrastructure;
using MAT.MVC.Models;
using PagedList;

namespace MAT.MVC.Controllers.Butaca
{
    public class ButacaController : Controller
    {
        public ActionResult Index(int? page, string searchString)
        {
            try
            {
                IList<MAT.Entities.Butaca> butacas;

                if (!string.IsNullOrEmpty(searchString))
                {
                    butacas = ButacaMethod.GetByTransporteId(new Guid(searchString))
                        .OrderByDescending(b => b.NroButaca)
                        .ToList();
                }
                else
                {
                    butacas = ButacaMethod.GetAll()
                        .OrderBy(b => b.TransporteId)
                        .ThenByDescending(b => b.NroButaca)
                        .ToList();
                }

                if (Request.HttpMethod != "GET")
                {
                    page = 1;
                }

                int pageSize = 8;
                int pageNumber = page ?? 1;

                return View(butacas.ToPagedList(pageNumber, pageSize));
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "ButacaController.Index");
                return View(new List<MAT.Entities.Butaca>().ToPagedList(1, 8));
            }
        }

        public ActionResult Create(string TransporteId)
        {
            try
            {
                var eButaca = new MAT.Entities.Butaca();
                if (!string.IsNullOrEmpty(TransporteId))
                {
                    var trId = new Guid(TransporteId);
                    var transporte = TransporteMethod.GetEntityById(trId);
                    if (transporte != null)
                    {
                        eButaca.TransporteId = trId;
                    }
                }

                return View(eButaca);
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "ButacaController.Create");
                return View(new MAT.Entities.Butaca());
            }
        }

        [HttpPost]
        public ActionResult Create(MAT.Entities.Butaca eButaca)
        {
            try
            {
                ButacaMethod.Insert(eButaca);
                return RedirectToAction("Index", "Butaca", new { SearchString = eButaca.TransporteId.ToString() });
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "ButacaController.Create");
                return View(eButaca);
            }
        }

        public ActionResult Edit(Guid Id)
        {
            try
            {
                var eButaca = ButacaMethod.GetById(Id);
                if (eButaca == null)
                {
                    TempData["Error"] = "La butaca solicitada no existe.";
                    return RedirectToAction("Index");
                }

                return View(eButaca);
            }
            catch (Exception e)
            {
                TempData["Error"] = ErrorUtil.LogAndGetPublicMessage(e, "ButacaController.Edit");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(Guid Id, FormCollection collection)
        {
            MAT.Entities.Butaca eButaca = null;
            try
            {
                eButaca = ButacaMethod.GetById(Id);
                if (eButaca == null)
                {
                    TempData["Error"] = "La butaca solicitada no existe.";
                    return RedirectToAction("Index");
                }

                eButaca.NroButaca = Convert.ToInt32(collection.Get("NroButaca"));
                eButaca.Piso = Convert.ToInt32(collection.Get("Piso"));
                eButaca.Ubicacion = Convert.ToInt32(collection.Get("Ubicacion"));
                eButaca.Tipo = Convert.ToInt32(collection.Get("Tipo"));
                eButaca.TransporteId = new Guid(collection.Get("TransporteId"));
                eButaca.Fila = collection.Get("Fila");
                eButaca.Posicion = collection.Get("Posicion");
                eButaca.CodigoButaca = collection.Get("CodigoButaca");

                ButacaMethod.Update(eButaca);
                return RedirectToAction("Details/" + eButaca.ButacaId, "Butaca");
            }
            catch (Exception e)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(e, "ButacaController.Edit");
                return View(eButaca ?? new MAT.Entities.Butaca());
            }
        }

        public ActionResult Details(Guid Id)
        {
            try
            {
                var eButaca = ButacaMethod.GetById(Id);
                if (eButaca == null)
                {
                    TempData["Error"] = "La butaca solicitada no existe.";
                    return RedirectToAction("Index");
                }

                return View(eButaca);
            }
            catch (Exception e)
            {
                TempData["Error"] = ErrorUtil.LogAndGetPublicMessage(e, "ButacaController.Details");
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(Guid Id)
        {
            try
            {
                ButacaMethod.Delete(Id);
            }
            catch (Exception e)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(e, "ButacaController.Delete");
                return PartialView("Error", msg);
            }

            return RedirectToAction("Index", "Butaca");
        }
    }
}
