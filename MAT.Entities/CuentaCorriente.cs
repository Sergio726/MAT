using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: CuentaCorriente</summary>
    [Serializable]
    public class CuentaCorriente
    {
		public Guid CuentaCorrienteId { get; set; }
		public DateTime? Fecha { get; set; }
		public double? Monto { get; set; }
		public Guid ClienteId { get; set; }
    }
}

