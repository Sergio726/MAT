using MAT.Entities;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Acceso a datos de ReservaHabitacion, Habitacion (entidad) y la vista
    /// vConsultaReservaHabitacion vía SP + DBHelper (NetTiers F5).
    /// </summary>
    public static class ReservaHabitacionDataAccess
    {
        public static ReservaHabitacion GetById(Guid reservaHabitacionId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ReservaHabitacionID", SqlDbType.UniqueIdentifier, 0, reservaHabitacionId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_ReservaHabitacion_GetById", parameters))
            {
                return reader.Read() ? MapReservaHabitacion(reader) : null;
            }
        }

        public static List<ReservaHabitacion> GetByPasajeId(Guid pasajeId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PasajeID", SqlDbType.UniqueIdentifier, 0, pasajeId)
            };
            return ReadList("dbo.usp_MAT_ReservaHabitacion_GetByPasajeId", parameters, MapReservaHabitacion);
        }

        public static List<ReservaHabitacion> GetByHabitacionId(Guid habitacionId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@HabitacionID", SqlDbType.UniqueIdentifier, 0, habitacionId)
            };
            return ReadList("dbo.usp_MAT_ReservaHabitacion_GetByHabitacionId", parameters, MapReservaHabitacion);
        }

        public static void Update(ReservaHabitacion reserva)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ReservaHabitacionID", SqlDbType.UniqueIdentifier, 0, reserva.ReservaHabitacionId),
                DBHelper.MakeParam("@HabitacionID", SqlDbType.UniqueIdentifier, 0, (object)reserva.HabitacionId ?? DBNull.Value),
                DBHelper.MakeParam("@PasajeID", SqlDbType.UniqueIdentifier, 0, (object)reserva.PasajeId ?? DBNull.Value),
                DBHelper.MakeParam("@FechaReserva", SqlDbType.Date, 0, (object)reserva.FechaReserva ?? DBNull.Value),
                DBHelper.MakeParam("@Desde", SqlDbType.Date, 0, (object)reserva.Desde ?? DBNull.Value),
                DBHelper.MakeParam("@Hasta", SqlDbType.Date, 0, (object)reserva.Hasta ?? DBNull.Value),
                DBHelper.MakeParam("@Expiro", SqlDbType.Bit, 0, (object)reserva.Expiro ?? DBNull.Value),
                DBHelper.MakeParam("@HoraIngreso", SqlDbType.VarChar, 10, (object)reserva.HoraIngreso ?? DBNull.Value),
                DBHelper.MakeParam("@HoraSalida", SqlDbType.VarChar, 10, (object)reserva.HoraSalida ?? DBNull.Value),
                DBHelper.MakeParam("@PasajeroID", SqlDbType.UniqueIdentifier, 0, (object)reserva.PasajeroId ?? DBNull.Value),
                DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, (object)reserva.ViajeId ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_ReservaHabitacion_UpdateEntity", parameters);
        }

        /// <summary>
        /// Reservas por habitación sobre la vista vConsultaReservaHabitacion.
        /// Pasar expiro = null para no filtrar por expiradas.
        /// </summary>
        public static List<VConsultaReservaHabitacion> GetConsultaByHabitacionId(Guid habitacionId, bool? expiro)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@HabitacionID", SqlDbType.UniqueIdentifier, 0, habitacionId),
                DBHelper.MakeParam("@Expiro", SqlDbType.Bit, 0, (object)expiro ?? DBNull.Value)
            };
            return ReadList("dbo.usp_MAT_VConsultaReservaHabitacion_GetByHabitacionId", parameters, r => new VConsultaReservaHabitacion
            {
                ReservaHabitacionId = r.GetGuid(r.GetOrdinal("ReservaHabitacionID")),
                HotelId = GetNullableGuid(r, "HotelID"),
                Expiro = GetNullableBool(r, "Expiro"),
                HabitacionId = GetNullableGuid(r, "HabitacionID"),
                Capacidad = GetNullableInt(r, "Capacidad"),
                Ocupacion = GetNullableInt(r, "Ocupacion"),
                Estado = GetNullableInt(r, "Estado"),
                Desde = GetNullableDateTime(r, "Desde"),
                Hasta = GetNullableDateTime(r, "Hasta")
            });
        }

        public static List<Habitacion> GetHabitacionesByHotelId(Guid hotelId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@HotelID", SqlDbType.UniqueIdentifier, 0, hotelId)
            };
            return ReadList("dbo.usp_MAT_Habitacion_GetEntitiesByHotelId", parameters, r => new Habitacion
            {
                HabitacionId = r.GetGuid(r.GetOrdinal("habitacionid")),
                NroHabitacion = GetNullableInt(r, "nrohabitacion"),
                Tipo = r.GetInt32(r.GetOrdinal("tipo")),
                Estado = r.GetInt32(r.GetOrdinal("Estado")),
                Capacidad = r.GetInt32(r.GetOrdinal("Capacidad")),
                Ocupacion = r.GetInt32(r.GetOrdinal("Ocupacion")),
                Nombre = GetString(r, "nombre"),
                HotelId = GetNullableGuid(r, "HotelID")
            });
        }

        public static void UpdateHabitacion(Habitacion habitacion)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@HabitacionID", SqlDbType.UniqueIdentifier, 0, habitacion.HabitacionId),
                DBHelper.MakeParam("@NroHabitacion", SqlDbType.Int, 0, (object)habitacion.NroHabitacion ?? DBNull.Value),
                DBHelper.MakeParam("@Tipo", SqlDbType.Int, 0, habitacion.Tipo),
                DBHelper.MakeParam("@HotelID", SqlDbType.UniqueIdentifier, 0, (object)habitacion.HotelId ?? DBNull.Value),
                DBHelper.MakeParam("@Estado", SqlDbType.Int, 0, habitacion.Estado),
                DBHelper.MakeParam("@Capacidad", SqlDbType.Int, 0, habitacion.Capacidad),
                DBHelper.MakeParam("@Ocupacion", SqlDbType.Int, 0, habitacion.Ocupacion),
                DBHelper.MakeParam("@Nombre", SqlDbType.VarChar, 100, (object)habitacion.Nombre ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Habitacion_UpdateEntity", parameters);
        }

        private static ReservaHabitacion MapReservaHabitacion(SqlDataReader reader)
        {
            return new ReservaHabitacion
            {
                ReservaHabitacionId = reader.GetGuid(reader.GetOrdinal("ReservaHabitacionID")),
                HabitacionId = GetNullableGuid(reader, "HabitacionID"),
                PasajeId = GetNullableGuid(reader, "PasajeID"),
                FechaReserva = GetNullableDateTime(reader, "FechaReserva"),
                Desde = GetNullableDateTime(reader, "Desde"),
                Hasta = GetNullableDateTime(reader, "Hasta"),
                Expiro = GetNullableBool(reader, "Expiro"),
                HoraIngreso = GetString(reader, "HoraIngreso"),
                HoraSalida = GetString(reader, "HoraSalida"),
                PasajeroId = GetNullableGuid(reader, "PasajeroID"),
                ViajeId = GetNullableGuid(reader, "ViajeID")
            };
        }

        private static List<T> ReadList<T>(string spName, SqlParameter[] parameters, Func<SqlDataReader, T> map)
        {
            var list = new List<T>();
            using (var reader = DBHelper.ExecuteDataReader(spName, parameters))
            {
                while (reader.Read())
                {
                    list.Add(map(reader));
                }
            }
            return list;
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

        private static bool? GetNullableBool(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? (bool?)null : reader.GetBoolean(ordinal);
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
