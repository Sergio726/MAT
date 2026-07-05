using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Excursion</summary>
    [Serializable]
    public class Excursion
    {
		public Guid ExcursionId { get; set; }
		public string Descripcion { get; set; }
		public double? Costo { get; set; }
		public string Observaciones { get; set; }
		public Guid? ProveedorId { get; set; }
    }
}

