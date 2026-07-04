using MAT.Entities;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Acceso a datos de Pago y MovimientoCuenta vía SP + DBHelper (NetTiers F6).
    /// </summary>
    public static class PagoDataAccess
    {
        public static Pago GetPagoById(Guid pagoId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PagoID", SqlDbType.UniqueIdentifier, 0, pagoId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Pago_GetEntityById", parameters))
            {
                return reader.Read() ? MapPago(reader) : null;
            }
        }

        /// <summary>
        /// Pagos de una factura vía MovimientoCuenta (una sola consulta, sin N+1).
        /// </summary>
        public static List<Pago> GetPagosByFacturaId(Guid facturaId)
        {
            var list = new List<Pago>();
            var parameters = new[]
            {
                DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, facturaId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Pago_GetByFacturaId", parameters))
            {
                while (reader.Read())
                {
                    list.Add(MapPago(reader));
                }
            }
            return list;
        }

        public static List<Pago> GetPagosByVendedorId(Guid vendedorId)
        {
            var list = new List<Pago>();
            var parameters = new[]
            {
                DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0, vendedorId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Pago_GetByVendedorId", parameters))
            {
                while (reader.Read())
                {
                    list.Add(MapPago(reader));
                }
            }
            return list;
        }

        public static void UpdatePago(Pago pago)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PagoID", SqlDbType.UniqueIdentifier, 0, pago.PagoId),
                DBHelper.MakeParam("@FechaPago", SqlDbType.DateTime, 0, (object)pago.FechaPago ?? DBNull.Value),
                DBHelper.MakeParam("@Monto", SqlDbType.Money, 0, (object)pago.Monto ?? DBNull.Value),
                DBHelper.MakeParam("@TipoPago", SqlDbType.Int, 0, (object)pago.TipoPago ?? DBNull.Value),
                DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0, (object)pago.VendedorId ?? DBNull.Value),
                DBHelper.MakeParam("@NroRecibo", SqlDbType.VarChar, 50, (object)pago.NroRecibo ?? DBNull.Value),
                DBHelper.MakeParam("@TransaccionID", SqlDbType.VarChar, 50, (object)pago.TransaccionId ?? DBNull.Value),
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, (object)pago.ClienteId ?? DBNull.Value),
                DBHelper.MakeParam("@EstadoRendicion", SqlDbType.Int, 0, (object)pago.EstadoRendicion ?? DBNull.Value),
                DBHelper.MakeParam("@CuentaCorrienteID", SqlDbType.UniqueIdentifier, 0, (object)pago.CuentaCorrienteId ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Pago_UpdateEntity", parameters);
        }

        public static List<MovimientoCuenta> GetMovimientosByPagoId(Guid pagoId)
        {
            var list = new List<MovimientoCuenta>();
            var parameters = new[]
            {
                DBHelper.MakeParam("@PagoID", SqlDbType.UniqueIdentifier, 0, pagoId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_MovimientoCuenta_GetByPagoId", parameters))
            {
                while (reader.Read())
                {
                    list.Add(new MovimientoCuenta
                    {
                        MovimientoId = reader.GetGuid(reader.GetOrdinal("MovimientoID")),
                        PagoId = GetNullableGuid(reader, "PagoID"),
                        FacturaId = reader.GetGuid(reader.GetOrdinal("FacturaID")),
                        FechaRegistro = GetNullableDateTime(reader, "FechaRegistro"),
                        CuentaId = reader.GetGuid(reader.GetOrdinal("CuentaID")),
                        NotaId = GetNullableGuid(reader, "NotaID"),
                        CuentaCorrienteId = GetNullableGuid(reader, "CuentaCorrienteID"),
                        DebitoId = GetNullableGuid(reader, "DebitoID")
                    });
                }
            }
            return list;
        }

        private static Pago MapPago(SqlDataReader reader)
        {
            return new Pago
            {
                PagoId = reader.GetGuid(reader.GetOrdinal("PagoID")),
                FechaPago = GetNullableDateTime(reader, "FechaPago"),
                Monto = GetNullableDouble(reader, "Monto"),
                TipoPago = GetNullableInt(reader, "TipoPago"),
                VendedorId = GetNullableGuid(reader, "VendedorId"),
                NroRecibo = GetString(reader, "NroRecibo"),
                TransaccionId = GetString(reader, "TransaccionID"),
                ClienteId = GetNullableGuid(reader, "ClienteID"),
                EstadoRendicion = GetNullableInt(reader, "EstadoRendicion"),
                CuentaCorrienteId = GetNullableGuid(reader, "CuentaCorrienteID")
            };
        }

        private static string GetString(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        private static int? GetNullableInt(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? (int?)null : Convert.ToInt32(reader.GetValue(ordinal));
        }

        private static double? GetNullableDouble(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? (double?)null : Convert.ToDouble(reader.GetValue(ordinal));
        }

        private static DateTime? GetNullableDateTime(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? (DateTime?)null : reader.GetDateTime(ordinal);
        }

        private static Guid? GetNullableGuid(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? (Guid?)null : reader.GetGuid(ordinal);
        }
    }
}
