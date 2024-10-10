using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Integration.BackendApi.Models
{
    public class PersonaDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get;set; }
        public string Email { get; set; }
        public string NroDocumento { get; set; }
    }
    
}