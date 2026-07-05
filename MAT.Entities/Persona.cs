using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Persona</summary>
    [Serializable]
    public class Persona
    {
		public Guid PersonaId { get; set; }
		public string Apellido { get; set; }
		public string Nombre { get; set; }
		public int? TipoDocumento { get; set; }
		public string NroDocumento { get; set; }
		public string Celular { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public DateTime? FechaNacimiento { get; set; }
		public int? LocalidadId { get; set; }
		public int? UserId { get; set; }
		public string Domicilio { get; set; }
		public int? Sexo { get; set; }
		public string Ocupacion { get; set; }
		public string Nacionalidad { get; set; }
		public string PaisResidencia { get; set; }
		public int? Provincia { get; set; }
    }
}

