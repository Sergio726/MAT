using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Habitacion</summary>
    [Serializable]
    public class Habitacion
    {
		public Guid HabitacionId { get; set; }
		public int? NroHabitacion { get; set; }
		public int Tipo { get; set; }
		public Guid? HotelId { get; set; }
		public int Estado { get; set; }
		public int Capacidad { get; set; }
		public int Ocupacion { get; set; }
		public string Nombre { get; set; }
    }
}

