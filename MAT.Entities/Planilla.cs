using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Planilla</summary>
    [Serializable]
    public class Planilla
    {
		public Guid PlanillaId { get; set; }
		public Guid ViajeId { get; set; }
		public DateTime FechaRegistro { get; set; }
		public double Total { get; set; }
    }
}

