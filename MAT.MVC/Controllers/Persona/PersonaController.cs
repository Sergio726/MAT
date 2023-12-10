using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Services;

namespace MAT.MVC.Controllers.Persona
{
    public class PersonaController : Controller
    {
        //
        // GET: /Persona/

        public ActionResult Index()
        {
            PersonaService srv = new PersonaService();
            IList<MAT.Entities.Persona> personas = srv.GetAll();
            return View(personas);
        }

        public ActionResult Details(Guid id)
        {
            PersonaService srv = new PersonaService();
            MAT.Entities.Persona persona = srv.Get(new PersonaKey(id));
            return View(persona);
        }

        //GET Method
        public ActionResult Edit(Guid id)
        {
            PersonaService srv = new PersonaService();
            MAT.Entities.Persona persona = srv.Get(new PersonaKey(id));
            return View(persona);
        }

        [HttpPost]
        public ActionResult Edit(MAT.Entities.Persona persona)
        {
            PersonaService srv = new PersonaService();
            srv.Update(persona);
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
            PersonaService srv = new PersonaService();
            srv.Insert(persona);
            return RedirectToAction("Index", "Persona");
        }

        public ActionResult Delete(Guid id)
        {
            PersonaService srv = new PersonaService();
            srv.Delete(id);
            return RedirectToAction("Index", "Persona");
        }
    }
}
