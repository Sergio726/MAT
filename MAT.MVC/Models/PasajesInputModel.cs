using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    [Serializable]
    public class PasajeInputModel
    {
        public string pasajeid { get; set; }
        public string pasajeroid { get; set; }
        public string precioid { get; set; }
        public string adicionalesid { get; set; }
    }
}