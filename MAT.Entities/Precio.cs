using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Precio</summary>
    [Serializable]
    public class Precio
    {
		public Guid PrecioId { get; set; }
		public double Monto { get; set; }
		public DateTime? Vigencia { get; set; }
		public string Descripcion { get; set; }
		public string Mes { get; set; }
    }
}

