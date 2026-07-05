using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: PersonaProveedor</summary>
    [Serializable]
    public class PersonaProveedor
    {
		public Guid PersonaId { get; set; }
		public string Apellido { get; set; }
		public string Nombre { get; set; }
		public string NroDocumento { get; set; }
		public int? LocalidadId { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public DateTime? FechaNacimiento { get; set; }
		public int? Sexo { get; set; }
		public string Domicilio { get; set; }
		public Guid ProveedorId { get; set; }
		public string RazonSocial { get; set; }
		public int? ProveedorLocalidadId { get; set; }
		public string ProveedorTelefono { get; set; }
		public string Fax { get; set; }
		public string Web { get; set; }
		public string ProveedorEmail { get; set; }
		public string Idioma { get; set; }
		public int? CondicionIva { get; set; }
		public string Cuit { get; set; }
		public int? FormaPago { get; set; }
		public int? TipoDocumento { get; set; }
    }
}

