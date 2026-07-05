using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Servicio</summary>
    [Serializable]
    public class Servicio
    {
		public Guid ServicioId { get; set; }
		public string Descripcion { get; set; }
		public double? Precio { get; set; }
		public string Moneda { get; set; }
		public string Iva { get; set; }
		public double? Alicuota { get; set; }
		public DateTime? Validez { get; set; }
		public int? VisibilidadTarifa { get; set; }
		public Guid? ProveedorId { get; set; }
		public Guid? TransporteId { get; set; }
		public Guid? HotelId { get; set; }
		public int? TipoServicio { get; set; }
    }
}

