using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Debito</summary>
    [Serializable]
    public class Debito
    {
		public Guid DebitoId { get; set; }
		public DateTime? Fecha { get; set; }
		public Guid? ClienteId { get; set; }
		public Guid? VendedorId { get; set; }
		public double? MontoDebito { get; set; }
    }
}

