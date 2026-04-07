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
            var pagoService = new PagoService();
            var movimientoService = new MovimientoCuentaService();
            var pagos = pagoService.GetByVendedorId(vendedorid).OrderByDescending(pa => pa.FechaPago).ToList();
            var filas = new List<VendedorHistorialPagoFila>(pagos.Count);
            foreach (var p in pagos)
            {
                Guid? facturaId = null;
                var movs = movimientoService.GetByPagoId(p.PagoId);
                if (movs != null && movs.Count > 0)
                    facturaId = movs[0].FacturaId;
                filas.Add(new VendedorHistorialPagoFila { Pago = p, FacturaId = facturaId });
            }
            return PartialView(filas);
        }
               

    }
}
