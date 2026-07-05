using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Cliente</summary>
    [Serializable]
    public class Cliente
    {
		public Guid ClienteId { get; set; }
		public string RazonSocial { get; set; }
		public string Cuit { get; set; }
		public string Moneda { get; set; }
		public string Empresa { get; set; }
		public string Ocupacion { get; set; }
		public int? FormaPago { get; set; }
		public int? CondicionIva { get; set; }
		public Guid? VendedorId { get; set; }
		public string Fax { get; set; }
		public string Web { get; set; }
		public string Idioma { get; set; }
		public string Promotor { get; set; }
		public string Observacion { get; set; }
		public int TipoId { get; set; }
    }
}

