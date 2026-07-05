using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Pasaje</summary>
    [Serializable]
    public class Pasaje
    {
		public Guid PasajeId { get; set; }
		public Guid? PasajeroId { get; set; }
		public Guid? ButacaId { get; set; }
		public DateTime? FechaReserva { get; set; }
		public DateTime? FechaCompra { get; set; }
		public Guid? ViajeId { get; set; }
		public Guid? FacturaId { get; set; }
		public int EstadoPasaje { get; set; }
		public Guid? VoucherId { get; set; }
		public Guid? PrecioId { get; set; }
    }
}

