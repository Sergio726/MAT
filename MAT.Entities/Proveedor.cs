using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Proveedor</summary>
    [Serializable]
    public class Proveedor
    {
		public Guid ProveedorId { get; set; }
		public string RazonSocial { get; set; }
		public string Telefono { get; set; }
		public string Fax { get; set; }
		public string Web { get; set; }
		public string Email { get; set; }
		public string Idioma { get; set; }
		public int? CondicionIva { get; set; }
		public string Cuit { get; set; }
		public int? FormaPago { get; set; }
		public int? LocalidadId { get; set; }
    }
}

