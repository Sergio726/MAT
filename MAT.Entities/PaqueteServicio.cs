using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: PaqueteServicio</summary>
    [Serializable]
    public class PaqueteServicio
    {
		public Guid PaqueteServicioId { get; set; }
		public Guid? ServicioId { get; set; }
		public Guid? PaqueteId { get; set; }
    }
}

