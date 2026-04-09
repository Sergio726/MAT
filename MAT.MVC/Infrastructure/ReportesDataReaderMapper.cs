using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using MAT.MVC.Models.Reportes;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Mapea filas de <see cref="SqlDataReader"/> a DTOs de reportes con lectura de columnas <b>case-insensitive</b>
    /// y normalización de tipos alineada a <c>REPORTES_MAT_WEB.md</c>.
    /// </summary>
    /// <remarks>
    /// Para JSON camelCase usar al serializar: <c>new CamelCasePropertyNamesContractResolver()</c> (Newtonsoft).
    /// </remarks>
    public static class ReportesDataReaderMapper
    {
        /// <summary>Lee todas las filas del reader (consume el cursor). El caller abre y cierra el reader.</summary>
        public static List<ReporteVentaRowDto> ReadVentas(SqlDataReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            var map = ReportesColumnMap.FromReader(reader);
            var list = new List<ReporteVentaRowDto>();
            while (reader.Read())
                list.Add(MapVenta(reader, map));
            return list;
        }

        public static List<ReportePagoRowDto> ReadPagos(SqlDataReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            var map = ReportesColumnMap.FromReader(reader);
            var list = new List<ReportePagoRowDto>();
            while (reader.Read())
                list.Add(MapPago(reader, map));
            return list;
        }

        public static List<ReporteRankingRowDto> ReadRanking(SqlDataReader reader)
        {
            if (reader == null) throw new ArgumentNullException(nameof(reader));
            var map = ReportesColumnMap.FromReader(reader);
            var list = new List<ReporteRankingRowDto>();
            while (reader.Read())
                list.Add(MapRanking(reader, map));
            return list;
        }

        public static ReporteVentaRowDto MapVenta(SqlDataReader reader, ReportesColumnMap map)
        {
            return new ReporteVentaRowDto
            {
                ViajeId = ReadGuidString(map.GetValue(reader, "ViajeId")),
                ViajeDescripcion = ReadString(map.GetValue(reader, "ViajeDescripcion")),
                VendedorId = ReadGuidString(map.GetValue(reader, "VendedorId")),
                VendedorFullName = ReadString(map.GetValue(reader, "VendedorFullName")),
                ClienteId = ReadGuidString(map.GetValue(reader, "ClienteId", "ClienteID")),
                ClienteFullName = ReadString(map.GetValue(reader, "ClienteFullName")),
                FacturaId = ReadGuidString(map.GetValue(reader, "FacturaId", "FacturaID")),
                FacturaFecha = ReadDateOnly(map.GetValue(reader, "FacturaFecha")),
                FacturaEstado = ReadString(map.GetValue(reader, "FacturaEstado")),
                MonedaTipo = ReadMonedaTipoString(map.GetValue(reader, "MonedaTipo")),
                TotalFactura = ReadDecimal(map.GetValue(reader, "TotalFactura")),
                MontoPagado = ReadDecimal(map.GetValue(reader, "MontoPagado")),
                Saldo = ReadDecimal(map.GetValue(reader, "Saldo")),
                FechaSalida = ReadDateOnly(map.GetValue(reader, "FechaSalida")),
                CantidadButacas = ReadInt32(map.GetValue(reader, "CantidadButacas"))
            };
        }

        public static ReportePagoRowDto MapPago(SqlDataReader reader, ReportesColumnMap map)
        {
            var descripcionPago = ReadString(map.GetValue(reader, "Descripcion"));

            return new ReportePagoRowDto
            {
                FacturaId = ReadGuidString(map.GetValue(reader, "FacturaID", "FacturaId")),
                FechaPago = ReadDateTime(map.GetValue(reader, "FechaPago")),
                Monto = ReadDecimal(map.GetValue(reader, "Monto")),
                MonedaTipo = ReadMonedaTipoString(map.GetValue(reader, "MonedaTipo")),
                PagoTipoId = ReadInt32(map.GetValue(reader, "PagoTipoId")),
                PagoDescripcion = descripcionPago,
                TipoVentaId = ReadInt32(map.GetValue(reader, "TipoVentaId")),
                TipoVentaDescripcion = ReadString(map.GetValue(reader, "TipoVentaDescripcion")),
                CantidadTipoPago = ReadInt32(map.GetValue(reader, "CantidadTipoPago")),
                RankingTipoPago = ReadInt32(map.GetValue(reader, "RankingTipoPago")),
                VendedorId = ReadGuidString(map.GetValue(reader, "VendedorId")),
                VendedorFullName = ReadString(map.GetValue(reader, "VendedorFullName")),
                ClienteId = ReadGuidString(map.GetValue(reader, "ClienteId", "ClienteID")),
                ClienteFullName = ReadString(map.GetValue(reader, "ClienteFullName")),
                Viaje = ReadString(map.GetValue(reader, "Viaje"))
            };
        }

        public static ReporteRankingRowDto MapRanking(SqlDataReader reader, ReportesColumnMap map)
        {
            var fullName = ReadString(map.GetValue(reader, "FullName"));

            return new ReporteRankingRowDto
            {
                FacturaId = ReadGuidString(map.GetValue(reader, "FacturaID", "FacturaId")),
                Fecha = ReadDateTime(map.GetValue(reader, "Fecha")),
                ClienteId = ReadGuidString(map.GetValue(reader, "ClienteID", "ClienteId")),
                ClienteFullName = fullName,
                ViajeId = ReadGuidString(map.GetValue(reader, "ViajeID", "ViajeId")),
                ViajeDescripcion = ReadString(map.GetValue(reader, "ViajeDescripcion")),
                ViajeFechaSalida = ReadDateTime(map.GetValue(reader, "ViajeFechaSalida")),
                CantidadPasajesXFactura = ReadInt32(map.GetValue(reader, "CantidadPasajesXFactura")),
                CantViajesCompradosXCliente = ReadInt32(map.GetValue(reader, "CantViajesCompradosXCliente")),
                CantPasajesCompradosXCliente = ReadInt32(map.GetValue(reader, "CantPasajesCompradosXCliente")),
                CantClientesEligieronViaje = ReadInt32(map.GetValue(reader, "CantClientesEligieronViaje")),
                RankingClientesCompradoresViajes = ReadInt32(map.GetValue(reader, "RankingClientesCompradoresViajes")),
                RankingViajes = ReadInt32(map.GetValue(reader, "RankingViajes"))
            };
        }

        internal static string ReadMonedaTipoString(object raw)
        {
            if (raw == null || raw == DBNull.Value)
                return null;
            return Convert.ToString(raw, CultureInfo.InvariantCulture);
        }

        internal static string ReadString(object raw)
        {
            if (raw == null || raw == DBNull.Value)
                return null;
            return Convert.ToString(raw, CultureInfo.InvariantCulture);
        }

        internal static string ReadGuidString(object raw)
        {
            if (raw == null || raw == DBNull.Value)
                return null;
            if (raw is Guid g)
                return g.ToString();
            var s = Convert.ToString(raw, CultureInfo.InvariantCulture);
            if (string.IsNullOrWhiteSpace(s))
                return null;
            Guid parsed;
            return Guid.TryParse(s, out parsed) ? parsed.ToString() : s.Trim();
        }

        internal static decimal? ReadDecimal(object raw)
        {
            if (raw == null || raw == DBNull.Value)
                return null;
            if (raw is decimal d)
                return d;
            return Convert.ToDecimal(raw, CultureInfo.InvariantCulture);
        }

        internal static int? ReadInt32(object raw)
        {
            if (raw == null || raw == DBNull.Value)
                return null;
            if (raw is int i)
                return i;
            if (raw is long l)
                return checked((int)l);
            return Convert.ToInt32(raw, CultureInfo.InvariantCulture);
        }

        /// <summary>Fecha/hora cuando el valor es nativo; si es string, primer parse exitoso.</summary>
        internal static DateTime? ReadDateTime(object raw)
        {
            if (raw == null || raw == DBNull.Value)
                return null;
            if (raw is DateTime dt)
                return dt;
            if (raw is DateTimeOffset dto)
                return dto.DateTime;
            return ParseDateString(Convert.ToString(raw, CultureInfo.InvariantCulture));
        }

        /// <summary>Solo componente fecha (para columnas que en negocio son día).</summary>
        internal static DateTime? ReadDateOnly(object raw)
        {
            var dt = ReadDateTime(raw);
            return dt.HasValue ? dt.Value.Date : (DateTime?)null;
        }

        internal static DateTime? ParseDateString(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return null;
            s = s.Trim();
            DateTime parsed;
            var formats = new[]
            {
                "dd/MM/yyyy",
                "dd-MM-yyyy",
                "yyyy-MM-dd",
                "dd/MM/yyyy HH:mm:ss",
                "dd-MM-yyyy HH:mm:ss",
                "yyyy-MM-dd HH:mm:ss"
            };
            foreach (var f in formats)
            {
                if (DateTime.TryParseExact(s, f, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
                    return parsed;
            }
            if (DateTime.TryParse(s, CultureInfo.GetCultureInfo("es-AR"), DateTimeStyles.None, out parsed))
                return parsed;
            if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed))
                return parsed;
            return null;
        }

        /// <summary>Índices de columnas case-insensitive (primer nombre gana en alias duplicado).</summary>
        public sealed class ReportesColumnMap
        {
            private readonly Dictionary<string, int> _ordinals;

            private ReportesColumnMap(Dictionary<string, int> ordinals)
            {
                _ordinals = ordinals;
            }

            public static ReportesColumnMap FromReader(SqlDataReader reader)
            {
                if (reader == null) throw new ArgumentNullException(nameof(reader));
                var d = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i);
                    if (!d.ContainsKey(name))
                        d[name] = i;
                }
                return new ReportesColumnMap(d);
            }

            public object GetValue(SqlDataReader reader, params string[] candidates)
            {
                if (reader == null) throw new ArgumentNullException(nameof(reader));
                if (candidates == null || candidates.Length == 0)
                    return DBNull.Value;
                foreach (var c in candidates)
                {
                    int o;
                    if (_ordinals.TryGetValue(c, out o))
                        return reader.IsDBNull(o) ? (object)DBNull.Value : reader.GetValue(o);
                }
                return DBNull.Value;
            }
        }
    }
}
