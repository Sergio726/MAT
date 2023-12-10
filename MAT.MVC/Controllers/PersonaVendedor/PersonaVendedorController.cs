using MAT.Entities;
using MAT.MVC.Models;
using MAT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAT.MVC.Views.PersonaVendedor
{
    public class PersonaVendedorController : Controller
    {
        //
        // GET: /PersonaVendedor/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PartialHistorialPagos(Guid vendedorid)
        {
            PagoService pagoService = new PagoService();
            List<Pago> pagos = pagoService.GetByVendedorId(vendedorid).OrderByDescending(pa => pa.FechaPago).ToList();
            return PartialView(pagos);
        }
               

    }
}
