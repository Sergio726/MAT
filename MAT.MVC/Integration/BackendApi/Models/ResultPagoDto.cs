using MAT.MVC.Integration.BackendApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Integration.BackendApi
{
    public class ResultPagoDto: ErrorDto
    {
        public string Result { get; set; }
        public string EstadoFactura { get; set; }
    }
}