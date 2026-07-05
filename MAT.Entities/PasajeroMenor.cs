using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: PasajeroMenor</summary>
    [Serializable]
    public class PasajeroMenor
    {
		public int Id { get; set; }
		public Guid Pasajeid { get; set; }
		public Guid Pasajeroid { get; set; }
		public Guid Menorid { get; set; }
    }
}

