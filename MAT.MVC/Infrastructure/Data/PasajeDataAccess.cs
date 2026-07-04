using MAT.Entities;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Acceso a datos de Pasaje vía SP + DBHelper (NetTiers F4/F6).
    /// </summary>
    public static class PasajeDataAccess
    {
        /// <summary>
        /// Genera un pasaje por cada butaca del transporte para el viaje, en una
        /// transacción del SP (set-based). Devuelve la cantidad de pasajes generados.
        /// </summary>
        public static int GenerarPasajesByViaje(Guid viajeId, Guid transporteId, int estadoPasaje)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, viajeId),
                DBHelper.MakeParam("@TransporteID", SqlDbType.UniqueIdentifier, 0, transporteId),
                DBHelper.MakeParam("@EstadoPasaje", SqlDbType.Int, 0, estadoPasaje)
            };
            return DBHelper.ExecuteNonQueryOutput("dbo.usp_MAT_Pasaje_GenerarByViaje", parameters, "@Generados", SqlDbType.Int, 0);
        }

        public static Pasaje GetById(Guid pasajeId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PasajeID", SqlDbType.UniqueIdentifier, 0, pasajeId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Pasaje_GetEntityById", parameters))
            {
                return reader.Read() ? MapPasaje(reader) : null;
            }
        }

        public static List<Pasaje> GetByFacturaId(Guid facturaId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, facturaId)
            };
            return ReadList("dbo.usp_MAT_Pasaje_GetByFacturaId", parameters);
        }

        public static List<Pasaje> GetByViajeId(Guid viajeId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, viajeId)
            };
            return ReadList("dbo.usp_MAT_Pasaje_GetEntitiesByViajeId", parameters);
        }

        public static List<Pasaje> GetByPasajeroId(Guid pasajeroId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PasajeroID", SqlDbType.UniqueIdentifier, 0, pasajeroId)
            };
            return ReadList("dbo.usp_MAT_Pasaje_GetByPasajeroId", parameters);
        }

        public static void Update(Pasaje pasaje)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PasajeID", SqlDbType.UniqueIdentifier, 0, pasaje.PasajeId),
                DBHelper.MakeParam("@PasajeroID", SqlDbType.UniqueIdentifier, 0, (object)pasaje.PasajeroId ?? DBNull.Value),
                DBHelper.MakeParam("@ButacaID", SqlDbType.UniqueIdentifier, 0, (object)pasaje.ButacaId ?? DBNull.Value),
                DBHelper.MakeParam("@FechaReserva", SqlDbType.Date, 0, (object)pasaje.FechaReserva ?? DBNull.Value),
                DBHelper.MakeParam("@FechaCompra", SqlDbType.Date, 0, (object)pasaje.FechaCompra ?? DBNull.Value),
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, (object)pasaje.ViajeId ?? DBNull.Value),
                DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, (object)pasaje.FacturaId ?? DBNull.Value),
                DBHelper.MakeParam("@EstadoPasaje", SqlDbType.Int, 0, pasaje.EstadoPasaje),
                DBHelper.MakeParam("@VoucherID", SqlDbType.UniqueIdentifier, 0, (object)pasaje.VoucherId ?? DBNull.Value),
                DBHelper.MakeParam("@PrecioID", SqlDbType.UniqueIdentifier, 0, (object)pasaje.PrecioId ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Pasaje_UpdateEntity", parameters);
        }

        private static List<Pasaje> ReadList(string spName, SqlParameter[] parameters)
        {
            var list = new List<Pasaje>();
            using (var reader = DBHelper.ExecuteDataReader(spName, parameters))
            {
                while (reader.Read())
                {
                    list.Add(MapPasaje(reader));
                }
            }
            return list;
        }

        private static Pasaje MapPasaje(SqlDataReader reader)
        {
            return new Pasaje
            {
                PasajeId = reader.GetGuid(reader.GetOrdinal("PasajeID")),
                PasajeroId = GetNullableGuid(reader, "PasajeroID"),
                ButacaId = GetNullableGuid(reader, "ButacaID"),
                FechaReserva = GetNullableDateTime(reader, "FechaReserva"),
                FechaCompra = GetNullableDateTime(reader, "FechaCompra"),
                ViajeId = GetNullableGuid(reader, "ViajeID"),
                FacturaId = GetNullableGuid(reader, "FacturaID"),
                EstadoPasaje = reader.GetInt32(reader.GetOrdinal("EstadoPasaje")),
                VoucherId = GetNullableGuid(reader, "VoucherID"),
                PrecioId = GetNullableGuid(reader, "PrecioID")
            };
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
