using System;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Resultado de validar y normalizar parámetros de consulta para reportes administrativos (Ventas, Pagos, Ranking).
    /// </summary>
    public sealed class ReportesQueryParseResult
    {
        private ReportesQueryParseResult()
        {
        }

        /// <summary>False si hay error de validación (formato, conflicto de filtros, rango demasiado amplio).</summary>
        public bool IsValid { get; private set; }

        /// <summary>Mensaje seguro para mostrar al usuario cuando <see cref="IsValid"/> es false.</summary>
        public string ValidationMessage { get; private set; }

        /// <summary>
        /// True cuando se debe ejecutar el SP: hay <c>viajeId</c> válido o par <c>from</c>/<c>to</c> válido.
        /// False si no hay criterio (respuesta vacía sin error).
        /// </summary>
        public bool ShouldExecute { get; private set; }

        /// <summary>Filtro por viaje (rama SP). Null en modo rango de fechas.</summary>
        public Guid? ViajeId { get; private set; }

        /// <summary>Cadena <c>dd-MM-yyyy</c> para <c>@From</c> en Ventas/Pagos. Null si modo solo viaje o sin criterio.</summary>
        public string FromDdMmYyyy { get; private set; }

        /// <summary>Cadena <c>dd-MM-yyyy</c> para <c>@To</c> en Ventas/Pagos.</summary>
        public string ToDdMmYyyy { get; private set; }

        /// <summary>Fecha (solo fecha) para <c>@From</c> en Ranking. Null si modo solo viaje o sin fechas.</summary>
        public DateTime? FromDate { get; private set; }

        /// <summary>Fecha (solo fecha) para <c>@To</c> en Ranking.</summary>
        public DateTime? ToDate { get; private set; }

        public Guid? VendedorId { get; private set; }

        public Guid? ClienteId { get; private set; }

        /// <summary>Solo aplica a Pagos; null si no se envió o no es necesario.</summary>
        public int? TipoVentaId { get; private set; }

        internal static ReportesQueryParseResult Invalid(string message)
        {
            return new ReportesQueryParseResult
            {
                IsValid = false,
                ValidationMessage = message ?? "Solicitud no válida.",
                ShouldExecute = false
            };
        }

        internal static ReportesQueryParseResult Empty()
        {
            return new ReportesQueryParseResult
            {
                IsValid = true,
                ValidationMessage = null,
                ShouldExecute = false
            };
        }

        internal static ReportesQueryParseResult ForTrip(Guid viajeId, Guid? vendedorId, Guid? clienteId, int? tipoVentaId)
        {
            return new ReportesQueryParseResult
            {
                IsValid = true,
                ShouldExecute = true,
                ViajeId = viajeId,
                VendedorId = vendedorId,
                ClienteId = clienteId,
                TipoVentaId = tipoVentaId
            };
        }

        internal static ReportesQueryParseResult ForDateRange(
            DateTime fromDate,
            DateTime toDate,
            Guid? vendedorId,
            Guid? clienteId,
            int? tipoVentaId)
        {
            return new ReportesQueryParseResult
            {
                IsValid = true,
                ShouldExecute = true,
                FromDdMmYyyy = fromDate.ToString("dd-MM-yyyy"),
                ToDdMmYyyy = toDate.ToString("dd-MM-yyyy"),
                FromDate = fromDate.Date,
                ToDate = toDate.Date,
                VendedorId = vendedorId,
                ClienteId = clienteId,
                TipoVentaId = tipoVentaId
            };
        }
    }
}
