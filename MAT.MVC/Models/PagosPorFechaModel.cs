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
            Pago = Infrastructure.Data.PagoDataAccess.GetPagoById(pagoid);
            Cliente = Infrastructure.Data.PersonaClienteDataAccess.GetByPersonaId(Pago.ClienteId.Value);
            Vendedor = Infrastructure.Data.PersonaVendedorDataAccess.GetByPersonaId(Pago.VendedorId.Value);
        }
    }
}