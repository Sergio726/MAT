using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class AuditModel
    {
    }

    public class AuditFactura
    {
        public string ID { get; set; }
        public string Accion { get; set; }
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
        public string Cliente { get; set; }
        public string Vendedor { get; set; }
    }

}