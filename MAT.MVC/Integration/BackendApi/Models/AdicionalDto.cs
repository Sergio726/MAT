using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Integration.BackendApi.Models
{
    public class AdicionalDto
    {
        public Guid AdicionalId { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; } 
        public bool IsMenor { get; set; }
    }
}