using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Viaje</summary>
    [Serializable]
    public class Viaje
    {
		public Guid ViajeId { get; set; }
		public Guid? PaqueteId { get; set; }
		public string Origen { get; set; }
		public DateTime? FechaSalida { get; set; }
		public string HoraSalida { get; set; }
		public string PaisOrigen { get; set; }
		public string PaisDestino { get; set; }
		public string Paso { get; set; }
		public string Medio { get; set; }
		public Guid? BusId { get; set; }
		public DateTime? FechaRegreso { get; set; }
		public string HoraRegreso { get; set; }
		public string Descripcion { get; set; }
		public double? PrecioSemicama { get; set; }
		public double? PrecioCama { get; set; }
		public double? PrecioPromocional { get; set; }
		public DateTime? FechaPromocion { get; set; }
		public int? NDias { get; set; }
		public int? NNoches { get; set; }
    }
}

