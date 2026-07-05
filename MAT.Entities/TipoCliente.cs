using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: TipoCliente</summary>
    [Serializable]
    public class TipoCliente
    {
		public int TipoId { get; set; }
		public string Descripcion { get; set; }
    }
}

