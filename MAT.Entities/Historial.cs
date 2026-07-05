using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Historial</summary>
    [Serializable]
    public class Historial
    {
		public Guid HistorialId { get; set; }
		public int Tabla { get; set; }
		public int Operacion { get; set; }
		public DateTime FechaHoraRegistro { get; set; }
		public Guid Cliente { get; set; }
		public Guid Vendedor { get; set; }
		public string Observaciones { get; set; }
		public double Monto { get; set; }
    }
}

