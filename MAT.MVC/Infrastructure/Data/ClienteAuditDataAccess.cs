using System;
using System.Collections.Generic;
using System.Data;
using MAT.MVC.Models.Clientes;
using MAT.Utilities;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Baja / reactivación de clientes e historial de cambios (<c>ClienteAudit</c>, módulo Clientes F6).
    /// </summary>
    public static class ClienteAuditDataAccess
    {
        public static void Registrar(Guid clienteId, string usuario, string accion, string detalle)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, clienteId),
                DBHelper.MakeParam("@Usuario", SqlDbType.VarChar, 100, (object)usuario ?? DBNull.Value),
                DBHelper.MakeParam("@Accion", SqlDbType.VarChar, 20, accion),
                DBHelper.MakeParam("@Detalle", SqlDbType.NVarChar, -1, (object)detalle ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_ClienteAudit_Insert", parameters);
        }

        /// <returns>true si cambió el estado; false si ya estaba así o el cliente no existe.</returns>
        public static bool CambiarEstado(Guid clienteId, bool activo, string motivo, string usuario)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, clienteId),
                DBHelper.MakeParam("@Activo", SqlDbType.Bit, 0, activo),
                DBHelper.MakeParam("@Motivo", SqlDbType.VarChar, 250, (object)motivo ?? DBNull.Value),
                DBHelper.MakeParam("@Usuario", SqlDbType.VarChar, 100, (object)usuario ?? DBNull.Value)
            };
            return Convert.ToInt32(DBHelper.ExecuteScalar("dbo.usp_MAT_Cliente_CambiarEstado", parameters)) == 1;
        }

        public static IList<ClienteAuditItemDto> Historial(Guid clienteId)
        {
            var parameters = new[] { DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, clienteId) };
            var lista = new List<ClienteAuditItemDto>();
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_ClienteAudit_GetByCliente", parameters))
            {
                while (reader.Read())
                {
                    lista.Add(new ClienteAuditItemDto
                    {
                        Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                        Usuario = reader.GetString("Usuario"),
                        Accion = reader.GetString("Accion"),
                        Detalle = reader.GetString("Detalle")
                    });
                }
            }
            return lista;
        }
    }
}
