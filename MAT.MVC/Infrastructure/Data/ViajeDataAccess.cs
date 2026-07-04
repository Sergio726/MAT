using MAT.Entities;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Acceso a datos de Viaje vía SP + DBHelper (NetTiers F5).
    /// </summary>
    public static class ViajeDataAccess
    {
        public static List<Viaje> GetAll()
        {
            var list = new List<Viaje>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Viaje_GetAllEntities", null))
            {
                while (reader.Read())
                {
                    list.Add(MapViaje(reader));
                }
            }
            return list;
        }

        public static Viaje GetById(Guid viajeId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, viajeId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Viaje_GetEntityById", parameters))
            {
                return reader.Read() ? MapViaje(reader) : null;
            }
        }

        /// <summary>
        /// Alta de entidad Viaje. Si la entidad no trae ViajeId (Guid.Empty) se
        /// genera uno nuevo antes del insert, para que el caller pueda usarlo.
        /// </summary>
        public static void Insert(Viaje viaje)
        {
            if (viaje.ViajeId == Guid.Empty)
            {
                viaje.ViajeId = Guid.NewGuid();
            }

            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Viaje_InsertEntity", BuildViajeParams(viaje));
        }

        public static void Update(Viaje viaje)
        {
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Viaje_UpdateEntity", BuildViajeParams(viaje));
        }

        private static SqlParameter[] BuildViajeParams(Viaje viaje)
        {
            return new[]
            {
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, viaje.ViajeId),
                DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, (object)viaje.PaqueteId ?? DBNull.Value),
                DBHelper.MakeParam("@Origen", SqlDbType.VarChar, 50, (object)viaje.Origen ?? DBNull.Value),
                DBHelper.MakeParam("@FechaSalida", SqlDbType.Date, 0, (object)viaje.FechaSalida ?? DBNull.Value),
                DBHelper.MakeParam("@HoraSalida", SqlDbType.VarChar, 50, (object)viaje.HoraSalida ?? DBNull.Value),
                DBHelper.MakeParam("@PaisOrigen", SqlDbType.VarChar, 50, (object)viaje.PaisOrigen ?? DBNull.Value),
                DBHelper.MakeParam("@PaisDestino", SqlDbType.VarChar, 50, (object)viaje.PaisDestino ?? DBNull.Value),
                DBHelper.MakeParam("@Paso", SqlDbType.VarChar, 50, (object)viaje.Paso ?? DBNull.Value),
                DBHelper.MakeParam("@Medio", SqlDbType.VarChar, 50, (object)viaje.Medio ?? DBNull.Value),
                DBHelper.MakeParam("@BusID", SqlDbType.UniqueIdentifier, 0, (object)viaje.BusId ?? DBNull.Value),
                DBHelper.MakeParam("@FechaRegreso", SqlDbType.Date, 0, (object)viaje.FechaRegreso ?? DBNull.Value),
                DBHelper.MakeParam("@HoraRegreso", SqlDbType.VarChar, 50, (object)viaje.HoraRegreso ?? DBNull.Value),
                DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 200, (object)viaje.Descripcion ?? DBNull.Value),
                DBHelper.MakeParam("@PrecioSemicama", SqlDbType.Float, 0, (object)viaje.PrecioSemicama ?? DBNull.Value),
                DBHelper.MakeParam("@PrecioCama", SqlDbType.Float, 0, (object)viaje.PrecioCama ?? DBNull.Value),
                DBHelper.MakeParam("@PrecioPromocional", SqlDbType.Float, 0, (object)viaje.PrecioPromocional ?? DBNull.Value),
                DBHelper.MakeParam("@FechaPromocion", SqlDbType.DateTime, 0, (object)viaje.FechaPromocion ?? DBNull.Value),
                DBHelper.MakeParam("@nDias", SqlDbType.Int, 0, (object)viaje.NDias ?? DBNull.Value),
                DBHelper.MakeParam("@nNoches", SqlDbType.Int, 0, (object)viaje.NNoches ?? DBNull.Value)
            };
        }

        private static Viaje MapViaje(SqlDataReader reader)
        {
            return new Viaje
            {
                ViajeId = reader.GetGuid(reader.GetOrdinal("ViajeID")),
                PaqueteId = GetNullableGuid(reader, "PaqueteID"),
                Origen = GetString(reader, "Origen"),
                FechaSalida = GetNullableDateTime(reader, "FechaSalida"),
                HoraSalida = GetString(reader, "HoraSalida"),
                PaisOrigen = GetString(reader, "PaisOrigen"),
                PaisDestino = GetString(reader, "PaisDestino"),
                Paso = GetString(reader, "Paso"),
                Medio = GetString(reader, "Medio"),
                BusId = GetNullableGuid(reader, "BusID"),
                FechaRegreso = GetNullableDateTime(reader, "FechaRegreso"),
                HoraRegreso = GetString(reader, "HoraRegreso"),
                Descripcion = GetString(reader, "Descripcion"),
                PrecioSemicama = GetNullableDouble(reader, "PrecioSemicama"),
                PrecioCama = GetNullableDouble(reader, "PrecioCama"),
                PrecioPromocional = GetNullableDouble(reader, "PrecioPromocional"),
                FechaPromocion = GetNullableDateTime(reader, "FechaPromocion"),
                NDias = GetNullableInt(reader, "nDias"),
                NNoches = GetNullableInt(reader, "nNoches")
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
