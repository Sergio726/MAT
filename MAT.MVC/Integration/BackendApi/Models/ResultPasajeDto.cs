using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Integration.BackendApi.Models
{
    public class ResultPasajeDto
    {
        public Guid? PasajeId { get; set; }
        public Guid? PasajeroId { get; set; }  // Ahora permite valores nulos
        public Guid? ButacaId { get; set; }
        public DateTime? FechaReserva { get; set; }
        public DateTime? FechaCompra { get; set; }
        public Guid? ViajeId { get; set; }
        public Guid? FacturaId { get; set; }
        public int? EstadoPasaje { get; set; }
        public Guid? VoucherId { get; set; }
        public Guid? PrecioId { get; set; }
        public string ButacaNro { get; set; }
        public int? ButacaPiso { get; set; }
        public string ButacaFila { get; set; }
        public string ButacaPosicion { get; set; }
        public string ButacaCodigoButaca { get; set; }
        public Guid? TransporteID { get; set; }
        public string TransporteNroCoche { get; set; }
        public Guid? PaqueteID { get; set; }
        public string PasajeroNombre { get; set; }
        public string PasajeroApellido { get; set; }
        public int? MonedaTipo { get; set; }
        public string TransporteTipo { get; set; }
    }

}