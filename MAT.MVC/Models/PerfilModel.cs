using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.Entities;
using MAT.Services;
using MAT.Utilities;
using MAT.Enums;

namespace MAT.MVC.Models
{
    public class PerfilModel
    {
        private VPersonaService personaService;
        private ClienteService clienteService;
        private PasajeroService pasajeroService;
        private VendedorService vendedorService;
        private ProveedorService proveedorService;

        public VPersona Persona { get; set; }
        public bool EsCliente { get; set; }
        public bool EsPasajero { get; set; }
        public bool EsVendedor { get; set; }
        public bool EsProveedor { get; set; }

        public PerfilModel(Guid personaid)
        {
            personaService = new VPersonaService();
            clienteService = new ClienteService();
            pasajeroService = new PasajeroService();
            vendedorService = new VendedorService();
            proveedorService = new ProveedorService();

            Persona = personaService.GetAll().Where(p => p.PersonaId == personaid).FirstOrDefault();
            EsCliente = clienteService.GetByClienteId(personaid) != null ? true : false;
            EsPasajero = pasajeroService.GetByPasajeroId(personaid) != null ? true : false;
            EsVendedor = vendedorService.GetByVendedorId(personaid) != null ? true : false;
            EsProveedor = proveedorService.GetByProveedorId(personaid) != null ? true : false;
        }
    }
}