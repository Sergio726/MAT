using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class ListaFacturasModel
    {

        public Entities.Persona Persona { get; set; }
        public List<Entities.Factura> ListaFacturas { get; set; }

        public ListaFacturasModel(Guid id)
        {
            Persona = new Services.PersonaService().GetByPersonaId(id);
            ListaFacturas = Infrastructure.Data.FacturaDataAccess.GetByClienteId(id).OrderByDescending(fac => fac.Fecha.Value).ToList();
        }
    }
}