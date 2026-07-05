using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: PersonaCliente</summary>
    [Serializable]
    public class PersonaCliente
    {
		public Guid PersonaId { get; set; }
		public string Apellido { get; set; }
		public string Nombre { get; set; }
		public string NroDocumento { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public DateTime? FechaNacimiento { get; set; }
		public string Domicilio { get; set; }
		public int? Sexo { get; set; }
		public int? LocalidadId { get; set; }
		public Guid ClienteId { get; set; }
		public string RazonSocial { get; set; }
		public string Cuit { get; set; }
		public string Moneda { get; set; }
		public string Empresa { get; set; }
		public string Ocupacion { get; set; }
		public int? FormaPago { get; set; }
		public int? CondicionIva { get; set; }
		public Guid? VendedorId { get; set; }
		public string Fax { get; set; }
		public string Web { get; set; }
		public string Idioma { get; set; }
		public string Promotor { get; set; }
		public string Observacion { get; set; }
		public int TipoId { get; set; }
		public int? TipoDocumento { get; set; }
		public string Celular { get; set; }
		public string Nacionalidad { get; set; }
		public string PaisResidencia { get; set; }
		public int? Provincia { get; set; }
    }
}

