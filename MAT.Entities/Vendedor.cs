using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Vendedor</summary>
    [Serializable]
    public class Vendedor
    {
		public Guid VendedorId { get; set; }
		public string Descripcion { get; set; }
    }
}

