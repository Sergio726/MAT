using MAT.Entities;
using MAT.MVC.Models;
using MAT.Services;
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
            return View();
        }

        public ActionResult DetalleComprobante(Guid id)
        {
            //Comprobante comprobante = new Comprobante(id);
            return PartialView();
        }

    }
}
