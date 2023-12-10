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
            Services.PersonaClienteService clienteService = new Services.PersonaClienteService();
            Services.PersonaVendedorService vendedorService = new Services.PersonaVendedorService();
            Registro = registroServices.GetByHistorialId(id);
            Cliente = clienteService.GetAll().Where(per => per.PersonaId == Registro.Cliente).FirstOrDefault();
            Vendedor = vendedorService.GetAll().Where(ven => ven.PersonaId == Registro.Vendedor).FirstOrDefault();
        }
    }
}