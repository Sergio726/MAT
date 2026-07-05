using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: PaqueteExcursion</summary>
    [Serializable]
    public class PaqueteExcursion
    {
		public Guid PaqueteExcursionId { get; set; }
		public Guid ExcursionId { get; set; }
		public Guid PaqueteId { get; set; }
    }
}

