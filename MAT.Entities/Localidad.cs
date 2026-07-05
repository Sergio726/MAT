using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Localidad</summary>
    [Serializable]
    public class Localidad
    {
		public int Id { get; set; }
		public int IdDepartamento { get; set; }
		public string Nombre { get; set; }
    }
}

