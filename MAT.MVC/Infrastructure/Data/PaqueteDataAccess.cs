using MAT.Entities;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Acceso a datos de Paquete, tablas de vinculo, Precio y PrecioHabitacion vía SP + DBHelper (NetTiers F4).
    /// </summary>
    public static class PaqueteDataAccess
    {
        #region Paquete

        public static Paquete GetPaqueteById(Guid paqueteId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, paqueteId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Paquete_GetById", parameters))
            {
                return reader.Read() ? MapPaquete(reader) : null;
            }
        }

        public static void UpdatePaquete(Paquete paquete)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, paquete.PaqueteId),
                DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 100, (object)paquete.Descripcion ?? DBNull.Value),
                DBHelper.MakeParam("@PrecioCama", SqlDbType.Float, 0, (object)paquete.PrecioCama ?? DBNull.Value),
                DBHelper.MakeParam("@Moneda", SqlDbType.Int, 0, (object)paquete.Moneda ?? DBNull.Value),
                DBHelper.MakeParam("@Iva", SqlDbType.VarChar, 50, (object)paquete.Iva ?? DBNull.Value),
                DBHelper.MakeParam("@Alicuota", SqlDbType.VarChar, 50, (object)paquete.Alicuota ?? DBNull.Value),
                DBHelper.MakeParam("@Temporada", SqlDbType.Int, 0, (object)paquete.Temporada ?? DBNull.Value),
                DBHelper.MakeParam("@Cotizacion", SqlDbType.Float, 0, (object)paquete.Cotizacion ?? DBNull.Value),
                DBHelper.MakeParam("@Codigo", SqlDbType.VarChar, 50, (object)paquete.Codigo ?? DBNull.Value),
                DBHelper.MakeParam("@DestinoID", SqlDbType.Int, 0, paquete.DestinoId),
                DBHelper.MakeParam("@PrecioSemiCama", SqlDbType.Float, 0, (object)paquete.PrecioSemiCama ?? DBNull.Value),
                DBHelper.MakeParam("@Foto", SqlDbType.VarChar, 200, (object)paquete.Foto ?? DBNull.Value),
                DBHelper.MakeParam("@ServiciosParticulares", SqlDbType.VarChar, -1, (object)paquete.ServiciosParticulares ?? DBNull.Value),
                DBHelper.MakeParam("@FechaCreacion", SqlDbType.DateTime, 0, (object)paquete.FechaCreacion ?? DBNull.Value),
                DBHelper.MakeParam("@LastUpdate", SqlDbType.DateTime, 0, paquete.LastUpdate)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Paquete_UpdateEntity", parameters);
        }

        public static int CountViajesByPaqueteId(Guid paqueteId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, paqueteId)
            };
            return Convert.ToInt32(DBHelper.ExecuteScalar("dbo.usp_MAT_Viaje_CountByPaqueteId", parameters));
        }

        /// <summary>
        /// Elimina el paquete y sus vinculos en una transacción del SP.
        /// Lanza excepción si el paquete tiene viajes vinculados.
        /// </summary>
        public static void DeletePaqueteCascade(Guid paqueteId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, paqueteId)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Paquete_DeleteCascade", parameters);
        }

        #endregion

        #region PaqueteServicio

        public static List<PaqueteServicio> GetPaqueteServiciosByPaqueteId(Guid paqueteId)
        {
            return ReadList("dbo.usp_MAT_PaqueteServicio_GetByPaqueteId", PaqueteIdParam(paqueteId), r => new PaqueteServicio
            {
                PaqueteServicioId = r.GetGuid(r.GetOrdinal("PaqueteServicioID")),
                ServicioId = GetNullableGuid(r, "ServicioID"),
                PaqueteId = GetNullableGuid(r, "PaqueteID")
            });
        }

        public static void InsertPaqueteServicio(PaqueteServicio paqueteServicio)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PaqueteServicioID", SqlDbType.UniqueIdentifier, 0, paqueteServicio.PaqueteServicioId),
                DBHelper.MakeParam("@ServicioID", SqlDbType.UniqueIdentifier, 0, (object)paqueteServicio.ServicioId ?? DBNull.Value),
                DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, (object)paqueteServicio.PaqueteId ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_PaqueteServicio_Insert", parameters);
        }

        public static bool DeletePaqueteServicio(Guid servicioId, Guid paqueteId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ServicioID", SqlDbType.UniqueIdentifier, 0, servicioId),
                DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, paqueteId)
            };
            return ReadEliminados("dbo.usp_MAT_PaqueteServicio_DeleteByServicioAndPaquete", parameters) > 0;
        }

        #endregion

        #region PaqueteExcursion

        public static List<PaqueteExcursion> GetPaqueteExcursionesByPaqueteId(Guid paqueteId)
        {
            // La entidad NetTiers PaqueteExcursion no conoce la columna IsOpcional
            // (se agregó a la tabla después de la generación); paridad con el service.
            return ReadList("dbo.usp_MAT_PaqueteExcursion_GetByPaqueteId", PaqueteIdParam(paqueteId), r => new PaqueteExcursion
            {
                PaqueteExcursionId = r.GetGuid(r.GetOrdinal("PaqueteExcursionID")),
                ExcursionId = r.GetGuid(r.GetOrdinal("ExcursionID")),
                PaqueteId = r.GetGuid(r.GetOrdinal("PaqueteID"))
            });
        }

        #endregion

        #region PaquetePrecio

        public static List<Entities.PaquetePrecio> GetPaquetePreciosByPaqueteId(Guid paqueteId)
        {
            return ReadList("dbo.usp_MAT_PaquetePrecio_GetByPaqueteId", PaqueteIdParam(paqueteId), r => new Entities.PaquetePrecio
            {
                PaquetePrecioId = r.GetGuid(r.GetOrdinal("PaquetePrecioID")),
                PaqueteId = GetNullableGuid(r, "PaqueteID"),
                PrecioId = GetNullableGuid(r, "PrecioID")
            });
        }

        public static void InsertPaquetePrecio(Entities.PaquetePrecio paquetePrecio)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PaquetePrecioID", SqlDbType.UniqueIdentifier, 0, paquetePrecio.PaquetePrecioId),
                DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, (object)paquetePrecio.PaqueteId ?? DBNull.Value),
                DBHelper.MakeParam("@PrecioID", SqlDbType.UniqueIdentifier, 0, (object)paquetePrecio.PrecioId ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_PaquetePrecio_Insert", parameters);
        }

        public static bool DeletePaquetePrecio(Guid precioId, Guid paqueteId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PrecioID", SqlDbType.UniqueIdentifier, 0, precioId),
                DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, paqueteId)
            };
            return ReadEliminados("dbo.usp_MAT_PaquetePrecio_DeleteByPrecioAndPaquete", parameters) > 0;
        }

        #endregion

        #region PaqueteAdicional

        public static List<PaqueteAdicional> GetPaqueteAdicionalesByPaqueteId(Guid paqueteId)
        {
            return ReadList("dbo.usp_MAT_PaqueteAdicional_GetByPaqueteId", PaqueteIdParam(paqueteId), r => new PaqueteAdicional
            {
                PaqueteAdicionalId = r.GetGuid(r.GetOrdinal("PaqueteAdicionalID")),
                PaqueteId = GetNullableGuid(r, "PaqueteID"),
                AdicionalId = GetNullableGuid(r, "AdicionalID")
            });
        }

        public static void InsertPaqueteAdicional(PaqueteAdicional paqueteAdicional)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PaqueteAdicionalID", SqlDbType.UniqueIdentifier, 0, paqueteAdicional.PaqueteAdicionalId),
                DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, (object)paqueteAdicional.PaqueteId ?? DBNull.Value),
                DBHelper.MakeParam("@AdicionalID", SqlDbType.UniqueIdentifier, 0, (object)paqueteAdicional.AdicionalId ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_PaqueteAdicional_Insert", parameters);
        }

        public static bool DeletePaqueteAdicional(Guid adicionalId, Guid paqueteId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@AdicionalID", SqlDbType.UniqueIdentifier, 0, adicionalId),
                DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, paqueteId)
            };
            return ReadEliminados("dbo.usp_MAT_PaqueteAdicional_DeleteByAdicionalAndPaquete", parameters) > 0;
        }

        #endregion

        #region Precio

        public static Precio GetPrecioById(Guid precioId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PrecioID", SqlDbType.UniqueIdentifier, 0, precioId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Precio_GetById", parameters))
            {
                return reader.Read() ? MapPrecio(reader) : null;
            }
        }

        public static List<Precio> GetAllPrecios()
        {
            return ReadList("dbo.usp_MAT_Precio_GetAllEntities", null, MapPrecio);
        }

        public static void InsertPrecio(Precio precio)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@Monto", SqlDbType.Float, 0, precio.Monto),
                DBHelper.MakeParam("@Vigencia", SqlDbType.DateTime, 0, (object)precio.Vigencia ?? DBNull.Value),
                DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 100, (object)precio.Descripcion ?? DBNull.Value),
                DBHelper.MakeParam("@Mes", SqlDbType.VarChar, 100, (object)precio.Mes ?? DBNull.Value),
                DBHelper.MakeParam("@DescripcionVoucher", SqlDbType.VarChar, 500, DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Precio_Insert", parameters);
        }

        /// <summary>
        /// Borra el precio, sus vinculos PaquetePrecio y desasocia pasajes, en una transacción del SP.
        /// </summary>
        public static void DeletePrecioCascade(Guid precioId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PrecioID", SqlDbType.UniqueIdentifier, 0, precioId)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Precio_DeleteCascade", parameters);
        }

        #endregion

        #region PrecioHabitacion

        public static List<PrecioHabitacion> GetAllPrecioHabitaciones()
        {
            return ReadList("dbo.usp_MAT_PrecioHabitacion_GetAll", null, r => new PrecioHabitacion
            {
                PrecioHabitacionId = r.GetGuid(r.GetOrdinal("PrecioHabitacionID")),
                TipoHabitacion = r.GetInt32(r.GetOrdinal("TipoHabitacion")),
                HotelId = r.GetGuid(r.GetOrdinal("HotelID")),
                FechaRegistro = r.GetDateTime(r.GetOrdinal("FechaRegistro")),
                Activo = r.GetBoolean(r.GetOrdinal("Activo")),
                Precio = r.GetDouble(r.GetOrdinal("Precio"))
            });
        }

        #endregion

        #region Mapeos y helpers

        private static Paquete MapPaquete(SqlDataReader reader)
        {
            return new Paquete
            {
                PaqueteId = reader.GetGuid(reader.GetOrdinal("PaqueteID")),
                Descripcion = GetString(reader, "Descripcion"),
                PrecioCama = GetNullableDouble(reader, "PrecioCama"),
                Moneda = GetNullableInt(reader, "Moneda"),
                Iva = GetString(reader, "Iva"),
                Alicuota = GetString(reader, "Alicuota"),
                Temporada = GetNullableInt(reader, "Temporada"),
                Cotizacion = GetNullableDouble(reader, "Cotizacion"),
                Codigo = GetString(reader, "Codigo"),
                DestinoId = reader.GetInt32(reader.GetOrdinal("DestinoID")),
                PrecioSemiCama = GetNullableDouble(reader, "PrecioSemiCama"),
                Foto = GetString(reader, "Foto"),
                ServiciosParticulares = GetString(reader, "ServiciosParticulares"),
                FechaCreacion = GetNullableDateTime(reader, "FechaCreacion"),
                LastUpdate = reader.GetDateTime(reader.GetOrdinal("LastUpdate"))
            };
        }

        private static Precio MapPrecio(SqlDataReader reader)
        {
            return new Precio
            {
                PrecioId = reader.GetGuid(reader.GetOrdinal("PrecioID")),
                Monto = reader.GetDouble(reader.GetOrdinal("Monto")),
                Vigencia = GetNullableDateTime(reader, "Vigencia"),
                Descripcion = GetString(reader, "Descripcion"),
                Mes = GetString(reader, "Mes")
            };
        }

        private static SqlParameter[] PaqueteIdParam(Guid paqueteId)
        {
            return new[]
            {
                DBHelper.MakeParam("@PaqueteID", SqlDbType.UniqueIdentifier, 0, paqueteId)
            };
        }

        private static int ReadEliminados(string spName, SqlParameter[] parameters)
        {
            using (var reader = DBHelper.ExecuteDataReader(spName, parameters))
            {
                return reader.Read() ? Convert.ToInt32(reader["Eliminados"]) : 0;
            }
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

        #endregion
    }
}
