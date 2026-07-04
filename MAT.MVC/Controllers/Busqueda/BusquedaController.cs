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
            IList<MAT.Entities.VPersona> personas = Infrastructure.Data.VPersonaDataAccess.Search(query);
            return Json(personas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Card(Guid personaid)
        {
            PerfilModel perfil = new PerfilModel(personaid);

            ViewBag.PersonaClienteCUIL = Infrastructure.Data.PersonaClienteDataAccess.GetByPersonaId(personaid).Cuit;
            return PartialView(perfil);
        }
    }
}
