using MAT.Entities;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Acceso a datos de la entidad Cuenta (cuenta corriente) vía SP + DBHelper (NetTiers F7).
    /// Reemplaza CuentaService.
    /// </summary>
    public static class CuentaDataAccess
    {
        public static List<Cuenta> GetByClienteId(Guid clienteId)
        {
            var list = new List<Cuenta>();
            var parameters = new[]
            {
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, clienteId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Cuenta_GetByClienteId", parameters))
            {
                while (reader.Read())
                {
                    list.Add(Map(reader));
                }
            }
            return list;
        }

        public static void Insert(Cuenta cuenta)
        {
            if (cuenta.CuentaId == Guid.Empty)
            {
                cuenta.CuentaId = Guid.NewGuid();
            }
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Cuenta_InsertEntity", BuildParams(cuenta));
        }

        public static void Update(Cuenta cuenta)
        {
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Cuenta_UpdateEntity", BuildParams(cuenta));
        }

        private static SqlParameter[] BuildParams(Cuenta c)
        {
            return new[]
            {
                DBHelper.MakeParam("@CuentaID", SqlDbType.UniqueIdentifier, 0, c.CuentaId),
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, c.ClienteId),
                DBHelper.MakeParam("@Estado", SqlDbType.Bit, 0, c.Estado)
            };
        }

        private static Cuenta Map(SqlDataReader reader)
        {
            return new Cuenta
            {
                CuentaId = reader.GetGuid("CuentaID"),
                ClienteId = reader.GetGuid("ClienteID"),
                Estado = reader.GetBool("Estado")
            };
        }
    }
}
