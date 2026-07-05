using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: DatosPaquete</summary>
    [Serializable]
    public class DatosPaquete
    {
		public Guid ViajeId { get; set; }
		public string ViajeOrigen { get; set; }
		public DateTime? ViajeFechaSalida { get; set; }
		public string ViajeHoraSalida { get; set; }
		public string ViajePaisOrigen { get; set; }
		public string ViajePaisDestino { get; set; }
		public string ViajePaso { get; set; }
		public string ViajeMedio { get; set; }
		public Guid PaqueteId { get; set; }
		public string Descripcion { get; set; }
		public double? Precio { get; set; }
		public int? Moneda { get; set; }
		public string Iva { get; set; }
		public string Alicuota { get; set; }
		public int? Temporada { get; set; }
		public double? Cotizacion { get; set; }
		public string Codigo { get; set; }
		public int? DestinoId { get; set; }
		public string Nombre { get; set; }
		public string DescripcionServicio { get; set; }
		public double? PrecioServicio { get; set; }
		public string MonedaServicio { get; set; }
    }
}

