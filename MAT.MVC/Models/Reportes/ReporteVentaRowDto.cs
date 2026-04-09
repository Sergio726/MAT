using System;

namespace MAT.MVC.Models.Reportes
{
    /// <summary>
    /// Fila del reporte de ventas (<c>usp_MAT_Reportes_Ventas</c>). Serializar JSON con <c>CamelCasePropertyNamesContractResolver</c>.
    /// </summary>
    public sealed class ReporteVentaRowDto
    {
        public string ViajeId { get; set; }
        public string ViajeDescripcion { get; set; }
        public string VendedorId { get; set; }
        public string VendedorFullName { get; set; }
        public string ClienteId { get; set; }
        public string ClienteFullName { get; set; }
        public string FacturaId { get; set; }
        /// <summary>Fecha de factura (solo fecha cuando aplica).</summary>
        public DateTime? FacturaFecha { get; set; }
        public string FacturaEstado { get; set; }
        /// <summary>Código de moneda como texto, p. ej. "1" o "3".</summary>
        public string MonedaTipo { get; set; }
        public decimal? TotalFactura { get; set; }
        public decimal? MontoPagado { get; set; }
        public decimal? Saldo { get; set; }
        public DateTime? FechaSalida { get; set; }
        public int? CantidadButacas { get; set; }
    }
}
