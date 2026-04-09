using System;

namespace MAT.MVC.Models.Reportes
{
    /// <summary>
    /// Fila del ranking de compras (<c>usp_MAT_Reportes_RankingCompras</c>). Serializar JSON con <c>CamelCasePropertyNamesContractResolver</c>.
    /// </summary>
    public sealed class ReporteRankingRowDto
    {
        public string FacturaId { get; set; }
        public DateTime? Fecha { get; set; }
        public string ClienteId { get; set; }
        public string ClienteFullName { get; set; }
        public string ViajeId { get; set; }
        public string ViajeDescripcion { get; set; }
        public DateTime? ViajeFechaSalida { get; set; }
        public int? CantidadPasajesXFactura { get; set; }
        public int? CantViajesCompradosXCliente { get; set; }
        public int? CantPasajesCompradosXCliente { get; set; }
        public int? CantClientesEligieronViaje { get; set; }
        public int? RankingClientesCompradoresViajes { get; set; }
        public int? RankingViajes { get; set; }
    }
}
