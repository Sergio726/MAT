using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: PersonaVendedor</summary>
    [Serializable]
    public class PersonaVendedor
    {
		public Guid PersonaId { get; set; }
		public string Apellido { get; set; }
		public string Nombre { get; set; }
		public string NroDocumento { get; set; }
		public string Domicilio { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public DateTime? FechaNacimiento { get; set; }
		public int? Sexo { get; set; }
		public int? LocalidadId { get; set; }
		public string Descripcion { get; set; }
		public Guid VendedorId { get; set; }
		public int? TipoDocumento { get; set; }
    }
}

