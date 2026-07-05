using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: ViajeHotel</summary>
    [Serializable]
    public class ViajeHotel
    {
		public Guid ViajeHotelId { get; set; }
		public Guid ViajeId { get; set; }
		public Guid HotelId { get; set; }
		public string Desde { get; set; }
		public string Hasta { get; set; }
		public string HoraIngreso { get; set; }
		public string HoraSalida { get; set; }
    }
}

