using MAT.Entities;
using MAT.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAT.MVC.Controllers.CuentaCorriente
{
    public class CuentaCorrienteController : Controller
    {
        //
        // GET: /CuentaCorriente/

        public ActionResult Index()
        {
            // Sin vista: daba 500. La cuenta corriente se ve en la ficha del cliente; se redirige al listado (2026-11-07).
            return RedirectToAction("Index", "PersonaCliente");
        }

        public ActionResult DetalleComprobante(Guid id)
        {
            //Comprobante comprobante = new Comprobante(id);
            return PartialView();
        }

    }
}
