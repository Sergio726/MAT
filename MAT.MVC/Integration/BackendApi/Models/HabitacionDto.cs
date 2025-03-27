using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Integration.BackendApi.Models
{
    public class HabitacionDto
    {
        public Guid HotelId { get; set; }
        public Guid HabitacionId { get; set; }
        public string NroHabitacion { get; set; }
        public string Tipo { get; set; }
        public string HabitacionNombre { get; set; }
        public string HabitacionTipo { get; set; }
        public string HotelNombre { get; set; }
        public int Disponibilidad { get; set; }
        public int Capacidad { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Precio { get; set; }
        public string HabitacionDescripcion { get; set; }

    }
}