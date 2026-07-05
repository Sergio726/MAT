using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Paquete</summary>
    [Serializable]
    public class Paquete
    {
		public Guid PaqueteId { get; set; }
		public string Descripcion { get; set; }
		public double? PrecioCama { get; set; }
		public int? Moneda { get; set; }
		public string Iva { get; set; }
		public string Alicuota { get; set; }
		public int? Temporada { get; set; }
		public double? Cotizacion { get; set; }
		public string Codigo { get; set; }
		public int DestinoId { get; set; }
		public double? PrecioSemiCama { get; set; }
		public string Foto { get; set; }
		public string ServiciosParticulares { get; set; }
		public DateTime? FechaCreacion { get; set; }
		public bool PublicWeb { get; set; }
		public DateTime LastUpdate { get; set; }
		public bool? ModePublicity { get; set; }
    }
}

