using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Services;
using MAT.Enums;
using MAT.Utilities;

namespace MAT.MVC.Controllers.Cliente
{
    public class ClienteController : Controller
    {
        //
        // GET: /Cliente/

        public ActionResult Index()
        {
            PersonaClienteService pcSrv = new PersonaClienteService();
            IList<Entities.PersonaCliente> clientes = pcSrv.GetAll();
            return View(clientes);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Entities.PersonaCliente cliente)
        {

            Entities.Persona p = new Entities.Persona()
            {
                Apellido = cliente.Apellido,
                Nombre=cliente.Nombre

            };
            
            return RedirectToAction("Index", "Cliente");
        }

    }
}
