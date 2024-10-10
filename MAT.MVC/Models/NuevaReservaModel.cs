using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class NuevaReservaModel
    {
        public Guid ViajeId { get; set; }
        public List<ReservaStandard> Reservas { get; set; }
        public DetalleViaje DetalleViaje { get; set; }
    }
}