using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Nota</summary>
    [Serializable]
    public class Nota
    {
		public Guid NotaId { get; set; }
		public double? PorcentajeRetencion { get; set; }
		public double? MontoRetencion { get; set; }
		public DateTime? Fecha { get; set; }
		public int? Dias { get; set; }
		public Guid? ClienteId { get; set; }
		public Guid? VendedorId { get; set; }
		public string NroNota { get; set; }
		public double? MontoNota { get; set; }
    }
}

