using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Ciudad</summary>
    [Serializable]
    public class Ciudad
    {
		public int CiudadId { get; set; }
		public string CiudadNombre { get; set; }
		public string PaisCodigo { get; set; }
		public string CiudadDistrito { get; set; }
		public int CiudadPoblacion { get; set; }
    }
}

