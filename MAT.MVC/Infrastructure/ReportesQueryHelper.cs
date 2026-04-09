using System;
using System.Globalization;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Valida y normaliza parámetros HTTP para los reportes que llaman a
    /// <c>usp_MAT_Reportes_Ventas</c>, <c>usp_MAT_Reportes_Pagos</c> y <c>usp_MAT_Reportes_RankingCompras</c>.
    /// </summary>
    /// <remarks>
    /// Reglas (SPEC reportes):
    /// <list type="bullet">
    /// <item>Ejecución solo si hay <c>from</c> y <c>to</c> válidos <b>o</b> <c>viajeId</c> válido.</item>
    /// <item><b>Rango de fechas y viaje son mutuamente excluyentes:</b> si vienen ambos, se rechaza (no se ignora uno silenciosamente).</item>
    /// <item>Fechas de entrada: <c>DD-MM-YYYY</c> o <c>YYYY-MM-DD</c>; para Ventas/Pagos se normaliza a <c>dd-MM-yyyy</c> como en <c>HomeController</c>.</item>
    /// <item>Rango máximo 365 días de calendario inclusive entre <c>from</c> y <c>to</c>.</item>
    /// <item>Para Ranking, usar <see cref="ReportesQueryParseResult.FromDate"/> / <see cref="ReportesQueryParseResult.ToDate"/> (tipo SQL <c>DATE</c>).</item>
    /// </list>
    /// </remarks>
    public static class ReportesQueryHelper
    {
        private const int MaxInclusiveCalendarDays = 365;

        /// <summary>
        /// Interpreta parámetros de query para los tres reportes. <c>tipoVentaId</c> solo se usa en Pagos.
        /// </summary>
        public static ReportesQueryParseResult Parse(
            string from,
            string to,
            string viajeId,
            string vendedorId = null,
            string clienteId = null,
            string tipoVentaId = null)
        {
            var hasFrom = !string.IsNullOrWhiteSpace(from);
            var hasTo = !string.IsNullOrWhiteSpace(to);
            var hasViaje = !string.IsNullOrWhiteSpace(viajeId);

            Guid? vendedor;
            string errGuid;
            if (!TryOptionalGuid(vendedorId, "vendedor", out vendedor, out errGuid))
                return ReportesQueryParseResult.Invalid(errGuid);

            Guid? cliente;
            if (!TryOptionalGuid(clienteId, "cliente", out cliente, out errGuid))
                return ReportesQueryParseResult.Invalid(errGuid);

            int? tipo = TryParseOptionalInt(tipoVentaId);
            if (!string.IsNullOrWhiteSpace(tipoVentaId) && tipo == null)
                return ReportesQueryParseResult.Invalid("El tipo de venta no es válido.");
            if (tipo.HasValue && tipo.Value != 1 && tipo.Value != 2)
                return ReportesQueryParseResult.Invalid("El tipo de venta debe ser 1 (oficina) o 2 (online).");

            if (hasFrom != hasTo)
                return ReportesQueryParseResult.Invalid("Debe indicar fecha de inicio y fin juntas (from y to).");

            Guid? viaje = null;
            if (hasViaje)
            {
                Guid g;
                if (!Guid.TryParse(viajeId.Trim(), out g))
                    return ReportesQueryParseResult.Invalid("El identificador de viaje no es válido.");
                viaje = g;
            }

            if (hasViaje && hasFrom)
            {
                // Mutuamente excluyentes: no priorizar uno silenciosamente (documentado en SPEC).
                return ReportesQueryParseResult.Invalid("No combine filtro por viaje y por rango de fechas. Use solo uno.");
            }

            if (!hasViaje && !hasFrom)
                return ReportesQueryParseResult.Empty();

            if (hasViaje)
                return ReportesQueryParseResult.ForTrip(viaje.Value, vendedor, cliente, tipo);

            DateTime? dFrom = TryParseDateParameter(from);
            DateTime? dTo = TryParseDateParameter(to);
            if (!dFrom.HasValue || !dTo.HasValue)
                return ReportesQueryParseResult.Invalid("Las fechas deben estar en formato DD-MM-YYYY o YYYY-MM-DD.");

            var fd = dFrom.Value.Date;
            var td = dTo.Value.Date;
            if (fd > td)
                return ReportesQueryParseResult.Invalid("La fecha inicial no puede ser posterior a la fecha final.");

            var inclusiveDays = (td - fd).Days + 1;
            if (inclusiveDays > MaxInclusiveCalendarDays)
                return ReportesQueryParseResult.Invalid("El rango de fechas no puede superar 365 días.");

            return ReportesQueryParseResult.ForDateRange(fd, td, vendedor, cliente, tipo);
        }

        private static bool TryOptionalGuid(string raw, string campoNombre, out Guid? value, out string error)
        {
            value = null;
            error = null;
            if (string.IsNullOrWhiteSpace(raw))
                return true;
            Guid g;
            if (!Guid.TryParse(raw.Trim(), out g))
            {
                error = "El identificador de " + campoNombre + " no es válido.";
                return false;
            }
            value = g;
            return true;
        }

        internal static int? TryParseOptionalInt(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return null;
            int v;
            if (!int.TryParse(raw.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out v))
                return null;
            return v;
        }

        /// <summary>
        /// Acepta <c>DD-MM-YYYY</c> o <c>YYYY-MM-DD</c> (invariante).
        /// Expuesto para pruebas unitarias del helper.
        /// </summary>
        public static DateTime? TryParseDateParameter(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return null;
            var s = raw.Trim();
            string[] formats =
            {
                "dd-MM-yyyy",
                "yyyy-MM-dd"
            };
            DateTime dt;
            if (DateTime.TryParseExact(s, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                return dt;
            return null;
        }
    }
}
