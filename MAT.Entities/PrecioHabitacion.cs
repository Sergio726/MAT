using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: PrecioHabitacion</summary>
    [Serializable]
    public class PrecioHabitacion
    {
		public Guid PrecioHabitacionId { get; set; }
		public int TipoHabitacion { get; set; }
		public Guid HotelId { get; set; }
		public DateTime FechaRegistro { get; set; }
		public bool Activo { get; set; }
		public double Precio { get; set; }
    }
}

