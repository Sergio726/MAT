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
        public VPersona Persona { get; set; }
        public bool EsCliente { get; set; }
        public bool EsPasajero { get; set; }
        public bool EsVendedor { get; set; }
        public bool EsProveedor { get; set; }

        public PerfilModel(Guid personaid)
        {
            var perfil = Infrastructure.Data.PerfilDataAccess.GetByPersonaId(personaid);
            Persona = perfil.Persona;
            EsCliente = perfil.EsCliente;
            EsPasajero = perfil.EsPasajero;
            EsVendedor = perfil.EsVendedor;
            EsProveedor = perfil.EsProveedor;
        }
    }
}