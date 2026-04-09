using System;

namespace MAT.MVC.Models.Reportes
{
    /// <summary>
    /// Fila del reporte de pagos (<c>usp_MAT_Reportes_Pagos</c>). Serializar JSON con <c>CamelCasePropertyNamesContractResolver</c>.
    /// </summary>
    public sealed class ReportePagoRowDto
    {
        public string FacturaId { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal? Monto { get; set; }
        public string MonedaTipo { get; set; }
        public int? PagoTipoId { get; set; }
        public string PagoDescripcion { get; set; }
        public int? TipoVentaId { get; set; }
        public string TipoVentaDescripcion { get; set; }
        public int? CantidadTipoPago { get; set; }
        public int? RankingTipoPago { get; set; }
        public string VendedorId { get; set; }
        public string VendedorFullName { get; set; }
        public string ClienteId { get; set; }
        public string ClienteFullName { get; set; }
        public string Viaje { get; set; }
    }
}
