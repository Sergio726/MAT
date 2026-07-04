using MAT.Entities;
using MAT.MVC.Infrastructure.Data;
using MAT.MVC.Models;
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
            var pagos = PagoDataAccess.GetPagosByVendedorId(vendedorid).OrderByDescending(pa => pa.FechaPago).ToList();
            var filas = new List<VendedorHistorialPagoFila>(pagos.Count);
            foreach (var p in pagos)
            {
                Guid? facturaId = null;
                var movs = PagoDataAccess.GetMovimientosByPagoId(p.PagoId);
                if (movs != null && movs.Count > 0)
                    facturaId = movs[0].FacturaId;
                filas.Add(new VendedorHistorialPagoFila { Pago = p, FacturaId = facturaId });
            }
            return PartialView(filas);
        }
               

    }
}
