using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Transporte</summary>
    [Serializable]
    public class Transporte
    {
		public Guid TransporteId { get; set; }
		public string NroCoche { get; set; }
		public int? MaxPasajeros { get; set; }
		public int? KmRecorridos { get; set; }
		public DateTime? UltimoService { get; set; }
		public string Matricula { get; set; }
    }
}

