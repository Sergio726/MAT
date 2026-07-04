using MAT.Entities;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Info de saldo de una factura (proyección sin entidad NetTiers).
    /// La resta se hace por call-site: MATContext.Saldo no descuenta débitos,
    /// FacturaModel.CalcularSaldo sí.
    /// </summary>
    public class FacturaSaldoInfo
    {
        public double? Monto { get; set; }
        public double TotalPagos { get; set; }
        public double TotalDebitos { get; set; }
    }

    /// <summary>
    /// Acceso a datos de la entidad Factura vía SP + DBHelper (NetTiers F6).
    /// Las consultas DTO/joineadas siguen en FacturaMetod (Models/FacturaModel.cs).
    /// </summary>
    public static class FacturaDataAccess
    {
        public static Factura GetById(Guid facturaId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, facturaId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Factura_GetEntityById", parameters))
            {
                return reader.Read() ? MapFactura(reader) : null;
            }
        }

        public static List<Factura> GetByClienteId(Guid clienteId)
        {
            var list = new List<Factura>();
            var parameters = new[]
            {
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, clienteId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Factura_GetEntitiesByClienteId", parameters))
            {
                while (reader.Read())
                {
                    list.Add(MapFactura(reader));
                }
            }
            return list;
        }

        public static void Update(Factura factura)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, factura.FacturaId),
                DBHelper.MakeParam("@NroFactura", SqlDbType.VarChar, 50, (object)factura.NroFactura ?? DBNull.Value),
                DBHelper.MakeParam("@Monto", SqlDbType.Float, 0, (object)factura.Monto ?? DBNull.Value),
                DBHelper.MakeParam("@Fecha", SqlDbType.DateTime, 0, (object)factura.Fecha ?? DBNull.Value),
                DBHelper.MakeParam("@Tipo", SqlDbType.Int, 0, (object)factura.Tipo ?? DBNull.Value),
                DBHelper.MakeParam("@Estado", SqlDbType.Int, 0, (object)factura.Estado ?? DBNull.Value),
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, factura.ClienteId),
                DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0, factura.VendedorId),
                DBHelper.MakeParam("@DescuentoAplicado", SqlDbType.Float, 0, factura.DescuentoAplicado),
                DBHelper.MakeParam("@Observaciones", SqlDbType.VarChar, -1, (object)factura.Observaciones ?? DBNull.Value),
                DBHelper.MakeParam("@DiasPreReserva", SqlDbType.Int, 0, (object)factura.DiasPreReserva ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Factura_UpdateEntity", parameters);
        }

        public static FacturaSaldoInfo GetSaldoInfo(Guid facturaId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, facturaId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Factura_GetSaldoInfo", parameters))
            {
                if (!reader.Read())
                {
                    return null;
                }

                return new FacturaSaldoInfo
                {
                    Monto = GetNullableDouble(reader, "Monto"),
                    TotalPagos = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("TotalPagos"))),
                    TotalDebitos = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("TotalDebitos")))
                };
            }
        }

        private static Factura MapFactura(SqlDataReader reader)
        {
            return new Factura
            {
                FacturaId = reader.GetGuid(reader.GetOrdinal("FacturaID")),
                NroFactura = GetString(reader, "NroFactura"),
                Monto = GetNullableDouble(reader, "Monto"),
                Fecha = GetNullableDateTime(reader, "Fecha"),
                Tipo = GetNullableInt(reader, "Tipo"),
                Estado = GetNullableInt(reader, "Estado"),
                ClienteId = reader.GetGuid(reader.GetOrdinal("ClienteID")),
                VendedorId = reader.GetGuid(reader.GetOrdinal("VendedorID")),
                DescuentoAplicado = reader.GetDouble(reader.GetOrdinal("DescuentoAplicado")),
                Observaciones = GetString(reader, "Observaciones"),
                DiasPreReserva = GetNullableInt(reader, "DiasPreReserva")
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
    }
}
