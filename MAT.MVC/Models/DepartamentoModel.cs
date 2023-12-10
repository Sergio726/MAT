using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class Departamento
    {
        public int IdDepartamento { get; set; }
        public string Nombre { get; set; }
    }
    
    public class Provincia
    {
        public int ID { get; set; }
        public string IdPais { get; set; }
        public string Nombre { get; set; }
    }

}