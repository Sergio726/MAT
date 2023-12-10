using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class PasajeroMenorModel
    {
        public int PasajeroMenorID { get; set; }
        public string ApellidoMayor {get; set;}
        public string NombreMayor {get; set;}
        public string DocMayor {get; set;}
        public string ApellidoMenor {get; set;}
        public string NomreMenor {get; set;}
        public string DocMenor { get; set; }
    }

    public class PasajeroMayor
    {
        public string PasajeID { get; set; }
        public string ClienteId { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public Int32 TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }

}                    
                     
                     
                     