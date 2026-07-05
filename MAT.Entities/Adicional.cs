using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Adicional</summary>
    [Serializable]
    public class Adicional
    {
		public Guid AdicionalId { get; set; }
		public double Monto { get; set; }
		public string Descripcion { get; set; }
    }
}

