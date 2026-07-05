using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: PlanillaHabitacionItem</summary>
    [Serializable]
    public class PlanillaHabitacionItem
    {
		public Guid PlanillaHabitacionItemId { get; set; }
		public Guid PlanillaId { get; set; }
		public Guid HabitacionId { get; set; }
		public int Cantidad { get; set; }
		public double Subtotal { get; set; }
    }
}

