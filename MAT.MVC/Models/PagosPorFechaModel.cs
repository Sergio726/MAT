using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class PagosPorFechaModel
    {
        public Entities.Pago Pago { get; set; }
        public Entities.PersonaCliente Cliente { get; set; }
        public Entities.PersonaVendedor Vendedor { get; set; }

        public PagosPorFechaModel(Guid pagoid)
        {
            Services.PersonaClienteService personaService = new Services.PersonaClienteService();
            Services.PersonaVendedorService vendedorService = new Services.PersonaVendedorService();
            Pago = Infrastructure.Data.PagoDataAccess.GetPagoById(pagoid);
            Cliente = personaService.GetAll().Where(p => p.PersonaId == Pago.ClienteId.Value).FirstOrDefault();
            Vendedor = vendedorService.GetAll().Where(v => v.PersonaId == Pago.VendedorId.Value).FirstOrDefault();
        }
    }
}