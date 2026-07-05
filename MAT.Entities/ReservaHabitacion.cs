using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: ReservaHabitacion</summary>
    [Serializable]
    public class ReservaHabitacion
    {
		public Guid ReservaHabitacionId { get; set; }
		public Guid? HabitacionId { get; set; }
		public Guid? PasajeId { get; set; }
		public DateTime? FechaReserva { get; set; }
		public DateTime? Desde { get; set; }
		public DateTime? Hasta { get; set; }
		public bool? Expiro { get; set; }
		public string HoraIngreso { get; set; }
		public string HoraSalida { get; set; }
		public Guid? PasajeroId { get; set; }
		public Guid? ViajeId { get; set; }
    }
}

