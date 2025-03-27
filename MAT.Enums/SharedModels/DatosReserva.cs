using System;
using System.Collections.Generic;

namespace MAT.Enums.SharedModels
{
    public class DatosReserva
    {
        public NuevaPersona Cliente { get; set; }
        public List<Pasajero> Pasajeros { get; set; }
        public DatosPago Pago { get; set; }
        public Guid ViajeId { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
