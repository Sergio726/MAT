using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAT.MVC.Controllers.Adicional
{
    public class AdicionalController : Controller
    {
        //
        // GET: /Adicional/

        public ActionResult Index()
        {
            Services.AdicionalService adicService = new Services.AdicionalService();
            IList<Entities.Adicional> adicionales = adicService.GetAll().OrderBy(ad =>ad.Descripcion).ToList();
            return View(adicionales);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            Services.AdicionalService adicService = new Services.AdicionalService();
            Entities.Adicional adicional = new Entities.Adicional();
            adicional.AdicionalId = Guid.NewGuid();
            MAT.Utilities.Helper.FillEntity<Entities.Adicional>(ref adicional, form);
            adicional.Monto = Convert.ToDouble(form["Monto"]);
            adicService.Insert(adicional);
            return RedirectToAction("Index");
        }

        public bool Delete(Guid id)
        {
            bool result = false;
            try
            {
                Services.PaqueteAdicionalService paqservicioService = new Services.PaqueteAdicionalService();
                List<Entities.PaqueteAdicional> vinculados = paqservicioService.GetByAdicionalId(id).ToList();
                for (int i = vinculados.Count; i > 0; i--)
                {
                    var item = vinculados[i - 1];
                    paqservicioService.Delete(item.PaqueteAdicionalId);
                }
                Services.AdicionalService adService = new Services.AdicionalService();
                adService.Delete(id);
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
            return View(new Services.AdicionalService().GetByAdicionalId(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection form)
        {
            Services.AdicionalService adicionalService = new Services.AdicionalService();
            Entities.Adicional adicional = adicionalService.GetByAdicionalId(id);
            Helper.FillEntity<Entities.Adicional>(ref adicional, form);
            adicionalService.Update(adicional);
            return RedirectToAction("Index");
        }
    }
}
