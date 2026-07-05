using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: PlanillaServicioItem</summary>
    [Serializable]
    public class PlanillaServicioItem
    {
		public Guid PlanillaServicioItemId { get; set; }
		public Guid PlanillaId { get; set; }
		public Guid ServicioId { get; set; }
		public int Cantidad { get; set; }
		public double Subtotal { get; set; }
    }
}

