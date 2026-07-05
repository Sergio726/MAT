using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: PlanillaServicio</summary>
    [Serializable]
    public class PlanillaServicio
    {
		public Guid PlanillaServicioId { get; set; }
		public Guid ViajeId { get; set; }
		public DateTime FechaRegistro { get; set; }
		public double Total { get; set; }
    }
}

