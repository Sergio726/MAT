using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: MovimientoCuenta</summary>
    [Serializable]
    public class MovimientoCuenta
    {
		public Guid MovimientoId { get; set; }
		public Guid? PagoId { get; set; }
		public Guid FacturaId { get; set; }
		public DateTime? FechaRegistro { get; set; }
		public Guid CuentaId { get; set; }
		public Guid? NotaId { get; set; }
		public Guid? CuentaCorrienteId { get; set; }
		public Guid? DebitoId { get; set; }
    }
}

