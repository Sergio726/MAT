using System;

namespace MAT.MVC.Models.Reportes
{
    /// <summary>
    /// Fila del ranking de compras V2 (<c>usp_MAT_Reportes_RankingCompras_V2</c>).
    /// Una fila por factura × viaje. Serializar JSON con <c>CamelCasePropertyNamesContractResolver</c>.
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
        public int? CantPasajerosDistintos { get; set; }
    }
}
