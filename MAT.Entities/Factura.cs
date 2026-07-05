using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Factura</summary>
    [Serializable]
    public class Factura
    {
		public Guid FacturaId { get; set; }
		public string NroFactura { get; set; }
		public double? Monto { get; set; }
		public DateTime? Fecha { get; set; }
		public int? Tipo { get; set; }
		public int? Estado { get; set; }
		public Guid ClienteId { get; set; }
		public Guid VendedorId { get; set; }
		public double DescuentoAplicado { get; set; }
		public string Observaciones { get; set; }
		public int? DiasPreReserva { get; set; }
    }
}

