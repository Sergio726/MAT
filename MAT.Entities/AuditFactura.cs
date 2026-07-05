using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: AuditFactura</summary>
    [Serializable]
    public class AuditFactura
    {
		public int Id { get; set; }
		public Guid? FacturaId { get; set; }
		public Guid? PersonaId { get; set; }
		public Guid VendedorId { get; set; }
		public string Accion { get; set; }
		public string Descripcion { get; set; }
		public DateTime Fecha { get; set; }
    }
}

