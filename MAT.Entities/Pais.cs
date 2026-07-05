using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Pais</summary>
    [Serializable]
    public class Pais
    {
		public Guid PaisId { get; set; }
		public string Descripcion { get; set; }
    }
}

