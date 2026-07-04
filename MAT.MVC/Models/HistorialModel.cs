using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class HistorialModel
    {
        public Entities.Historial Registro { get; set; }
        public Entities.PersonaCliente Cliente { get; set; }
        public Entities.PersonaVendedor Vendedor { get; set; }

        public HistorialModel(Guid id)
        {
            Services.HistorialService registroServices = new Services.HistorialService();
            Registro = registroServices.GetByHistorialId(id);
            Cliente = Infrastructure.Data.PersonaClienteDataAccess.GetByPersonaId(Registro.Cliente);
            Vendedor = Infrastructure.Data.PersonaVendedorDataAccess.GetByPersonaId(Registro.Vendedor);
        }
    }
}