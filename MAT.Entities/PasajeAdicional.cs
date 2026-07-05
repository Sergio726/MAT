using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: PasajeAdicional</summary>
    [Serializable]
    public class PasajeAdicional
    {
		public Guid PasajeAdicionalId { get; set; }
		public Guid? PasajeId { get; set; }
		public Guid? AdicionalId { get; set; }
    }
}

