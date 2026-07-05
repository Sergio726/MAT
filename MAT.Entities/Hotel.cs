using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Hotel</summary>
    [Serializable]
    public class Hotel
    {
		public Guid HotelId { get; set; }
		public string Nombre { get; set; }
		public string Direccion { get; set; }
		public string Cp { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public string Contacto { get; set; }
		public int? CantidadHabitaciones { get; set; }
		public int? Categoria { get; set; }
		public string Child1 { get; set; }
		public string Child2 { get; set; }
		public int? ChildHabitacion { get; set; }
		public string CheckIn { get; set; }
		public string CheckOut { get; set; }
		public string GoogleMapHtml { get; set; }
		public int? LocalidadId { get; set; }
    }
}

