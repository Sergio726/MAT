using MAT.MVC.Models;
using MAT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAT.MVC.Controllers.Busqueda
{
    public class BusquedaController : Controller
    {
        //
        // GET: /Busqueda/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Perfil()
        {
            return View();
        }

        public JsonResult PersonaAutocomplete(string query)
        {
            VPersonaService personaService = new VPersonaService();
            IList<MAT.Entities.VPersona> personas = personaService.GetAll().Where(p => p.NroDocumento.Replace(".","").Contains(query.Replace(".","")) || p.NroDocumento.Contains(query) || p.Nombre.ToUpper().Contains(query.ToUpper()) || p.Apellido.ToUpper().Contains(query.ToUpper())).ToList(); //
            return Json(personas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Card(Guid personaid)
        {
            PerfilModel perfil = new PerfilModel(personaid);
            
            PersonaClienteService PCService = new PersonaClienteService();
            ViewBag.PersonaClienteCUIL = PCService.GetAll().Where(l => l.ClienteId == personaid).First().Cuit;
            return PartialView(perfil);
        }
    }
}
