using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: PasajeroViaje</summary>
    [Serializable]
    public class PasajeroViaje
    {
		public long? Nro { get; set; }
		public Guid? ViajeId { get; set; }
		public Guid PersonaId { get; set; }
		public Guid? BusId { get; set; }
		public string Apellido { get; set; }
		public string Nombre { get; set; }
		public int? TipoDocumento { get; set; }
		public string NroDocumento { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public DateTime? FechaNacimiento { get; set; }
		public int? Sexo { get; set; }
		public int? LocalidadId { get; set; }
		public string Nacionalidad { get; set; }
		public string PaisResidencia { get; set; }
		public string Domicilio { get; set; }
		public string Ocupacion { get; set; }
		public string Pasaporte { get; set; }
		public DateTime? VencimientoPasaporte { get; set; }
		public DateTime? EmisionPasaporte { get; set; }
		public string PaisOrigen { get; set; }
    }
}

