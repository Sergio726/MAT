using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Destino</summary>
    [Serializable]
    public class Destino
    {
		public Guid DestinoId { get; set; }
		public int? LocalidadId { get; set; }
    }
}

