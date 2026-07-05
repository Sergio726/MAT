using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: EstadoPasaje</summary>
    [Serializable]
    public class EstadoPasaje
    {
		public int Id { get; set; }
		public string Descripcion { get; set; }
    }
}

