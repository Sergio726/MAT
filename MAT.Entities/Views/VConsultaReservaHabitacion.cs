using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: VConsultaReservaHabitacion</summary>
    [Serializable]
    public class VConsultaReservaHabitacion
    {
		public Guid ReservaHabitacionId { get; set; }
		public Guid? HotelId { get; set; }
		public bool? Expiro { get; set; }
		public Guid? HabitacionId { get; set; }
		public int? Capacidad { get; set; }
		public int? Ocupacion { get; set; }
		public int? Estado { get; set; }
		public DateTime? Desde { get; set; }
		public DateTime? Hasta { get; set; }
    }
}

