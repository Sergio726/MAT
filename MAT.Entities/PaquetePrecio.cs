using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: PaquetePrecio</summary>
    [Serializable]
    public class PaquetePrecio
    {
		public Guid PaquetePrecioId { get; set; }
		public Guid? PaqueteId { get; set; }
		public Guid? PrecioId { get; set; }
    }
}

