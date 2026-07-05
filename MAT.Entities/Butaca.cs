using System;

namespace MAT.Entities
{
	/// <summary>POCO manual (NetTiers F11). Tabla/vista: Butaca</summary>
    [Serializable]
    public class Butaca
    {
		public Guid ButacaId { get; set; }
		public int? NroButaca { get; set; }
		public int? Piso { get; set; }
		public int? Ubicacion { get; set; }
		public int? Tipo { get; set; }
		public Guid? TransporteId { get; set; }
		public string Fila { get; set; }
		public string Posicion { get; set; }
		public string CodigoButaca { get; set; }
    }
}

