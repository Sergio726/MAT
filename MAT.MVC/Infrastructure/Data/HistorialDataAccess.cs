using MAT.Entities;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Acceso a datos de la entidad Historial vía SP + DBHelper (NetTiers F8).
    /// Reemplaza HistorialService.
    /// </summary>
    public static class HistorialDataAccess
    {
        public static List<Historial> GetAll()
        {
            var list = new List<Historial>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Historial_GetAll", null))
            {
                while (reader.Read())
                {
                    list.Add(Map(reader));
                }
            }
            return list;
        }

        public static Historial GetByHistorialId(Guid historialId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@HistorialID", SqlDbType.UniqueIdentifier, 0, historialId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Historial_GetByHistorialId", parameters))
            {
                return reader.Read() ? Map(reader) : null;
            }
        }

        private static Historial Map(SqlDataReader reader)
        {
            return new Historial
            {
                HistorialId = reader.GetGuid("HistorialID"),
                Tabla = reader.GetInt("Tabla"),
                Operacion = reader.GetInt("Operacion"),
                FechaHoraRegistro = reader.GetDateTime(reader.GetOrdinal("FechaHoraRegistro")),
                Cliente = reader.GetGuid("Cliente"),
                Vendedor = reader.GetGuid("Vendedor"),
                Observaciones = reader.GetString("Observaciones"),
                Monto = reader.GetDouble(reader.GetOrdinal("Monto"))
            };
        }
    }
}
