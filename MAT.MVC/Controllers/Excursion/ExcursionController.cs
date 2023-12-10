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
            Services.ExcursionService excService = new Services.ExcursionService();
            IList<Entities.Excursion> excursiones = excService.GetAll().OrderBy(ex => ex.Descripcion).ToList();
            return View(excursiones);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            Services.ExcursionService excService = new Services.ExcursionService();
            Entities.Excursion excursion = new Entities.Excursion();
            excursion.ExcursionId = Guid.NewGuid();
            Helper.FillEntity<Entities.Excursion>(ref excursion, form);
            excService.Insert(excursion);
            return RedirectToAction("Index");
        }

        public bool Delete(Guid id)
        {
            bool result = false;
            try
            {
                Services.PaqueteExcursionService paqexcService = new Services.PaqueteExcursionService();
                List<Entities.PaqueteExcursion> excursionesVinculadas = paqexcService.GetByExcursionId(id).ToList();
                for (int i = excursionesVinculadas.Count; i > 0; i--)
                {
                    var item = excursionesVinculadas[i - 1];
                    paqexcService.Delete(item.PaqueteExcursionId);
                }
                Services.ExcursionService excService = new Services.ExcursionService();
                excService.Delete(id);
                result = true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                result = false;
            }
            return result;
        }

        public ActionResult Edit(Guid id)
        {
            return View(new Services.ExcursionService().GetByExcursionId(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection form)
        {
            Services.ExcursionService excursionService = new Services.ExcursionService();
            Entities.Excursion excursion = excursionService.GetByExcursionId(id);
            Helper.FillEntity<Entities.Excursion>(ref excursion, form);
            excursionService.Update(excursion);
            return RedirectToAction("Index");
        }
    }
}
