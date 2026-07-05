using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Provincia</summary>
    [Serializable]
    public class Provincia
    {
		public int Id { get; set; }
		public string Nombre { get; set; }
		public Guid IdPais { get; set; }
    }
}

