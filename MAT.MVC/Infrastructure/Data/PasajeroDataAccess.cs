using MAT.Entities;
using MAT.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Acceso a datos de la entidad Pasajero vía SP + DBHelper (NetTiers F7). Reemplaza PasajeroService.
    /// </summary>
    public static class PasajeroDataAccess
    {
        public static Pasajero GetById(Guid pasajeroId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PasajeroID", SqlDbType.UniqueIdentifier, 0, pasajeroId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Pasajero_GetEntityById", parameters))
            {
                return reader.Read() ? Map(reader) : null;
            }
        }

        public static void Insert(Pasajero pasajero)
        {
            if (pasajero.PasajeroId == Guid.Empty)
            {
                pasajero.PasajeroId = Guid.NewGuid();
            }
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Pasajero_InsertEntity", BuildParams(pasajero));
        }

        public static void Update(Pasajero pasajero)
        {
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Pasajero_UpdateEntity", BuildParams(pasajero));
        }

        public static void Delete(Guid pasajeroId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@PasajeroID", SqlDbType.UniqueIdentifier, 0, pasajeroId)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Pasajero_DeleteEntity", parameters);
        }

        private static SqlParameter[] BuildParams(Pasajero p)
        {
            return new[]
            {
                DBHelper.MakeParam("@PasajeroID", SqlDbType.UniqueIdentifier, 0, p.PasajeroId),
                DBHelper.MakeParam("@Pasaporte", SqlDbType.VarChar, 100, (object)p.Pasaporte ?? DBNull.Value),
                DBHelper.MakeParam("@VencimientoPasaporte", SqlDbType.Date, 0, (object)p.VencimientoPasaporte ?? DBNull.Value),
                DBHelper.MakeParam("@EmisionPasaporte", SqlDbType.Date, 0, (object)p.EmisionPasaporte ?? DBNull.Value),
                DBHelper.MakeParam("@PaisOrigen", SqlDbType.VarChar, 50, (object)p.PaisOrigen ?? DBNull.Value)
            };
        }

        private static Pasajero Map(SqlDataReader reader)
        {
            return new Pasajero
            {
                PasajeroId = reader.GetGuid("PasajeroID"),
                Pasaporte = reader.GetString("Pasaporte"),
                VencimientoPasaporte = reader.GetNullableDateTime("VencimientoPasaporte"),
                EmisionPasaporte = reader.GetNullableDateTime("EmisionPasaporte"),
                PaisOrigen = reader.GetString("PaisOrigen")
            };
        }
    }
}
