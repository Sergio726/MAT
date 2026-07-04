using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;

namespace MAT.MVC.Controllers.Persona
{
    public class PersonaController : Controller
    {
        //
        // GET: /Persona/

        public ActionResult Index()
        {
            IList<MAT.Entities.Persona> personas = Infrastructure.Data.PersonaDataAccess.GetAll();
            return View(personas);
        }

        public ActionResult Details(Guid id)
        {
            MAT.Entities.Persona persona = Infrastructure.Data.PersonaDataAccess.GetById(id);
            return View(persona);
        }

        //GET Method
        public ActionResult Edit(Guid id)
        {
            MAT.Entities.Persona persona = Infrastructure.Data.PersonaDataAccess.GetById(id);
            return View(persona);
        }

        [HttpPost]
        public ActionResult Edit(MAT.Entities.Persona persona)
        {
            Infrastructure.Data.PersonaDataAccess.Update(persona);
            return View(persona);
        }

        //GET Method
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MAT.Entities.Persona persona)
        {
            Infrastructure.Data.PersonaDataAccess.Insert(persona);
            return RedirectToAction("Index", "Persona");
        }

        public ActionResult Delete(Guid id)
        {
            Infrastructure.Data.PersonaDataAccess.Delete(id);
            return RedirectToAction("Index", "Persona");
        }
    }
}
