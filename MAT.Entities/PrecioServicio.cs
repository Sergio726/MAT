using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: PrecioServicio</summary>
    [Serializable]
    public class PrecioServicio
    {
		public Guid PrecioServicioId { get; set; }
		public Guid ServicioId { get; set; }
		public DateTime FechaRegistro { get; set; }
		public bool Activo { get; set; }
		public double Precio { get; set; }
    }
}

