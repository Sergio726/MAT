using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: PaqueteAdicional</summary>
    [Serializable]
    public class PaqueteAdicional
    {
		public Guid PaqueteAdicionalId { get; set; }
		public Guid? PaqueteId { get; set; }
		public Guid? AdicionalId { get; set; }
    }
}

