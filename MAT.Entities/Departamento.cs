using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Departamento</summary>
    [Serializable]
    public class Departamento
    {
		public int Id { get; set; }
		public int IdProvincia { get; set; }
		public string Nombre { get; set; }
    }
}

