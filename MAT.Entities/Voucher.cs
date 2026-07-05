using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Voucher</summary>
    [Serializable]
    public class Voucher
    {
		public Guid VoucherId { get; set; }
		public long NroVoucher { get; set; }
		public DateTime? FechaEmision { get; set; }
		public Guid? VendedorId { get; set; }
    }
}

