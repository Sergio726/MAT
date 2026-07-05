using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Reserva</summary>
    [Serializable]
    public class Reserva
    {
		public Guid PasajeId { get; set; }
		public DateTime? FechaReserva { get; set; }
		public string Apellido { get; set; }
		public string Nombre { get; set; }
		public string NroDocumento { get; set; }
		public Guid? ViajeId { get; set; }
		public string Paquete { get; set; }
		public Guid? FacturaId { get; set; }
		public Guid? ClienteId { get; set; }
		public int? TipoCliente { get; set; }
    }
}

