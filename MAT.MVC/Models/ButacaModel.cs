using MAT.Entities;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Models
{
    public class ButacaMethod
    {
        public static List<Butaca> GetAll()
        {
            var list = new List<Butaca>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Butaca_GetAll", null))
            {
                while (reader.Read())
                {
                    list.Add(MapButaca(reader));
                }
            }
            return list;
        }

        public static List<Butaca> GetByTransporteId(Guid transporteId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@TransporteID", SqlDbType.UniqueIdentifier, 0, transporteId)
            };
            var list = new List<Butaca>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Butaca_GetByTransporteId", parameters))
            {
                while (reader.Read())
                {
                    list.Add(MapButaca(reader));
                }
            }
            return list;
        }

        public static Butaca GetById(Guid butacaId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ButacaID", SqlDbType.UniqueIdentifier, 0, butacaId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Butaca_GetById", parameters))
            {
                return reader.Read() ? MapButaca(reader) : null;
            }
        }

        public static void Insert(Butaca butaca)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ButacaID", SqlDbType.UniqueIdentifier, 0,
                    butaca.ButacaId == Guid.Empty ? (object)DBNull.Value : butaca.ButacaId),
                DBHelper.MakeParam("@NroButaca", SqlDbType.Int, 0, (object)butaca.NroButaca ?? DBNull.Value),
                DBHelper.MakeParam("@Piso", SqlDbType.Int, 0, (object)butaca.Piso ?? DBNull.Value),
                DBHelper.MakeParam("@Ubicacion", SqlDbType.Int, 0, (object)butaca.Ubicacion ?? DBNull.Value),
                DBHelper.MakeParam("@Tipo", SqlDbType.Int, 0, (object)butaca.Tipo ?? DBNull.Value),
                DBHelper.MakeParam("@TransporteID", SqlDbType.UniqueIdentifier, 0, (object)butaca.TransporteId ?? DBNull.Value),
                DBHelper.MakeParam("@Fila", SqlDbType.VarChar, 2, (object)butaca.Fila ?? DBNull.Value),
                DBHelper.MakeParam("@Posicion", SqlDbType.VarChar, 1, (object)butaca.Posicion ?? DBNull.Value),
                DBHelper.MakeParam("@CodigoButaca", SqlDbType.VarChar, 4, (object)butaca.CodigoButaca ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Butaca_Insert", parameters);
        }

        public static void Update(Butaca butaca)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ButacaID", SqlDbType.UniqueIdentifier, 0, butaca.ButacaId),
                DBHelper.MakeParam("@NroButaca", SqlDbType.Int, 0, (object)butaca.NroButaca ?? DBNull.Value),
                DBHelper.MakeParam("@Piso", SqlDbType.Int, 0, (object)butaca.Piso ?? DBNull.Value),
                DBHelper.MakeParam("@Ubicacion", SqlDbType.Int, 0, (object)butaca.Ubicacion ?? DBNull.Value),
                DBHelper.MakeParam("@Tipo", SqlDbType.Int, 0, (object)butaca.Tipo ?? DBNull.Value),
                DBHelper.MakeParam("@TransporteID", SqlDbType.UniqueIdentifier, 0, (object)butaca.TransporteId ?? DBNull.Value),
                DBHelper.MakeParam("@Fila", SqlDbType.VarChar, 2, (object)butaca.Fila ?? DBNull.Value),
                DBHelper.MakeParam("@Posicion", SqlDbType.VarChar, 1, (object)butaca.Posicion ?? DBNull.Value),
                DBHelper.MakeParam("@CodigoButaca", SqlDbType.VarChar, 4, (object)butaca.CodigoButaca ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Butaca_Update", parameters);
        }

        public static void Delete(Guid butacaId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ButacaID", SqlDbType.UniqueIdentifier, 0, butacaId)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Butaca_Delete", parameters);
        }

        private static Butaca MapButaca(SqlDataReader reader)
        {
            return new Butaca
            {
                ButacaId = reader.GetGuid(reader.GetOrdinal("ButacaID")),
                NroButaca = GetNullableInt(reader, "NroButaca"),
                Piso = GetNullableInt(reader, "Piso"),
                Ubicacion = GetNullableInt(reader, "Ubicacion"),
                Tipo = GetNullableInt(reader, "Tipo"),
                TransporteId = GetNullableGuid(reader, "TransporteID"),
                Fila = GetString(reader, "Fila"),
                Posicion = GetString(reader, "Posicion"),
                CodigoButaca = GetString(reader, "CodigoButaca")
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

        private static Guid? GetNullableGuid(SqlDataReader reader, string column)
        {
            var ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? (Guid?)null : reader.GetGuid(ordinal);
        }
    }
}
