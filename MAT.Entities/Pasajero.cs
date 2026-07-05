using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Pasajero</summary>
    [Serializable]
    public class Pasajero
    {
		public Guid PasajeroId { get; set; }
		public string Pasaporte { get; set; }
		public DateTime? VencimientoPasaporte { get; set; }
		public DateTime? EmisionPasaporte { get; set; }
		public string PaisOrigen { get; set; }
    }
}

