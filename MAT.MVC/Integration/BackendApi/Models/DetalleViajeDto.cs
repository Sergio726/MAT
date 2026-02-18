using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Integration.BackendApi.Models
{
    public class DetalleViajeDto
    {
        public string Descripcion { get; set; }
        public string Destino { get; set; }
        public string FechaSalida { get; set; }
        public string FechaRegreso { get; set; }
        public string HoraSalida { get; set; }
        public string HoraRegreso { get; set; }
        public int TiempoConsentracion { get; set; }
        public string NroCoche { get; set; }
        /// <summary>Patente o dominio del transporte (Matricula).</summary>
        public string TransportePatente { get; set; }
        public string PaqueteServicios { get; set; }
        public string PaqueteExcusionesIncluidas { get; set; }
        public string PaqueteExcusionesOpcionales { get; set; }
        public string Observaciones { get; set; }
    }
}