using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Pago</summary>
    [Serializable]
    public class Pago
    {
		public Guid PagoId { get; set; }
		public DateTime? FechaPago { get; set; }
		public double? Monto { get; set; }
		public int? TipoPago { get; set; }
		public string TransaccionId { get; set; }
		public Guid? ClienteId { get; set; }
		public Guid? VendedorId { get; set; }
		public string NroRecibo { get; set; }
		public int? EstadoRendicion { get; set; }
		public Guid? CuentaCorrienteId { get; set; }
    }
}

