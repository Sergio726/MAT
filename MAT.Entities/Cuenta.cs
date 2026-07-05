using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Cuenta</summary>
    [Serializable]
    public class Cuenta
    {
		public Guid CuentaId { get; set; }
		public Guid ClienteId { get; set; }
		public bool Estado { get; set; }
    }
}

