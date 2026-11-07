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
            // Pantalla vieja: armaba ~19 MB de HTML con todas las personas y no la enlaza ninguna pantalla.
            // Se redirige al listado de clientes vigente (2026-11-07).
            return RedirectToAction("Index", "PersonaCliente");
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

        [HttpPost]
        [ValidateAntiForgeryToken]   // borra: solo POST con token (2026-11-07, antes un enlace GET borraba)
        public ActionResult Delete(Guid id)
        {
            Infrastructure.Data.PersonaDataAccess.Delete(id);
            return RedirectToAction("Index", "Persona");
        }
    }
}
