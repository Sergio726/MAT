using MAT.MVC.Infrastructure.Data;
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
            IList<Entities.Adicional> adicionales = MaestrosDataAccess.GetAllAdicionales().OrderBy(ad =>ad.Descripcion).ToList();
            return View(adicionales);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            Entities.Adicional adicional = new Entities.Adicional();
            adicional.AdicionalId = Guid.NewGuid();
            MAT.Utilities.Helper.FillEntity<Entities.Adicional>(ref adicional, form);
            adicional.Monto = Convert.ToDouble(form["Monto"]);
            MaestrosDataAccess.InsertAdicional(adicional);
            return RedirectToAction("Index");
        }

        public bool Delete(Guid id)
        {
            bool result = false;
            try
            {
                // NetTiers F4: la cascada (vinculos PaqueteAdicional + Adicional)
                // corre en la transaccion del SP.
                MaestrosDataAccess.DeleteAdicional(id);
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
            return View(MaestrosDataAccess.GetAdicionalById(id));
        }

        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection form)
        {
            Entities.Adicional adicional = MaestrosDataAccess.GetAdicionalById(id);
            Helper.FillEntity<Entities.Adicional>(ref adicional, form);
            MaestrosDataAccess.UpdateAdicional(adicional);
            return RedirectToAction("Index");
        }
    }
}
