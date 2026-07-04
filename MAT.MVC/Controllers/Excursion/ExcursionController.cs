using MAT.MVC.Infrastructure.Data;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAT.MVC.Controllers.Excursion
{
    public class ExcursionController : Controller
    {
        //
        // GET: /Excursion/

        public ActionResult Index()
        {
            IList<Entities.Excursion> excursiones = MaestrosDataAccess.GetAllExcursiones().OrderBy(ex => ex.Descripcion).ToList();
            return View(excursiones);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            Entities.Excursion excursion = new Entities.Excursion();
            excursion.ExcursionId = Guid.NewGuid();
            Helper.FillEntity<Entities.Excursion>(ref excursion, form);
            MaestrosDataAccess.InsertExcursion(excursion);
            return RedirectToAction("Index");
        }

        public bool Delete(Guid id)
        {
            bool result = false;
            try
            {
                // NetTiers F4: la cascada (vinculos PaqueteExcursion + Excursion)
                // corre en la transaccion del SP.
                MaestrosDataAccess.DeleteExcursion(id);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public ActionResult Edit(Guid id)
        {
            return View(MaestrosDataAccess.GetExcursionById(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection form)
        {
            Entities.Excursion excursion = MaestrosDataAccess.GetExcursionById(id);
            Helper.FillEntity<Entities.Excursion>(ref excursion, form);
            MaestrosDataAccess.UpdateExcursion(excursion);
            return RedirectToAction("Index");
        }
    }
}
