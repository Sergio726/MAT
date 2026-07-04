using MAT.Entities;
using MAT.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Acceso a datos de la entidad Cliente vía SP + DBHelper (NetTiers F7). Reemplaza ClienteService.
    /// Solo columnas conocidas por la entidad (no toca FechaAlta, paridad con NetTiers).
    /// </summary>
    public static class ClienteDataAccess
    {
        public static Cliente GetById(Guid clienteId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, clienteId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Cliente_GetEntityById", parameters))
            {
                return reader.Read() ? Map(reader) : null;
            }
        }

        public static void Insert(Cliente cliente)
        {
            if (cliente.ClienteId == Guid.Empty)
            {
                cliente.ClienteId = Guid.NewGuid();
            }
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Cliente_InsertEntity", BuildParams(cliente));
        }

        public static void Update(Cliente cliente)
        {
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Cliente_UpdateEntity", BuildParams(cliente));
        }

        public static void Delete(Guid clienteId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, clienteId)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Cliente_DeleteEntity", parameters);
        }

        private static SqlParameter[] BuildParams(Cliente c)
        {
            return new[]
            {
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, c.ClienteId),
                DBHelper.MakeParam("@RazonSocial", SqlDbType.VarChar, 50, (object)c.RazonSocial ?? DBNull.Value),
                DBHelper.MakeParam("@Cuit", SqlDbType.VarChar, 50, (object)c.Cuit ?? DBNull.Value),
                DBHelper.MakeParam("@Moneda", SqlDbType.VarChar, 50, (object)c.Moneda ?? DBNull.Value),
                DBHelper.MakeParam("@Empresa", SqlDbType.VarChar, 50, (object)c.Empresa ?? DBNull.Value),
                DBHelper.MakeParam("@Ocupacion", SqlDbType.VarChar, 50, (object)c.Ocupacion ?? DBNull.Value),
                DBHelper.MakeParam("@FormaPago", SqlDbType.Int, 0, (object)c.FormaPago ?? DBNull.Value),
                DBHelper.MakeParam("@CondicionIva", SqlDbType.Int, 0, (object)c.CondicionIva ?? DBNull.Value),
                DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0, (object)c.VendedorId ?? DBNull.Value),
                DBHelper.MakeParam("@Fax", SqlDbType.VarChar, 50, (object)c.Fax ?? DBNull.Value),
                DBHelper.MakeParam("@Web", SqlDbType.VarChar, 50, (object)c.Web ?? DBNull.Value),
                DBHelper.MakeParam("@Idioma", SqlDbType.VarChar, 50, (object)c.Idioma ?? DBNull.Value),
                DBHelper.MakeParam("@Promotor", SqlDbType.VarChar, 50, (object)c.Promotor ?? DBNull.Value),
                DBHelper.MakeParam("@Observacion", SqlDbType.VarChar, 250, (object)c.Observacion ?? DBNull.Value),
                DBHelper.MakeParam("@TipoID", SqlDbType.Int, 0, c.TipoId)
            };
        }

        private static Cliente Map(SqlDataReader reader)
        {
            return new Cliente
            {
                ClienteId = reader.GetGuid("ClienteID"),
                RazonSocial = reader.GetString("RazonSocial"),
                Cuit = reader.GetString("Cuit"),
                Moneda = reader.GetString("Moneda"),
                Empresa = reader.GetString("Empresa"),
                Ocupacion = reader.GetString("Ocupacion"),
                FormaPago = reader.GetNullableInt("FormaPago"),
                CondicionIva = reader.GetNullableInt("CondicionIva"),
                VendedorId = reader.GetNullableGuid("VendedorID"),
                Fax = reader.GetString("Fax"),
                Web = reader.GetString("Web"),
                Idioma = reader.GetString("Idioma"),
                Promotor = reader.GetString("Promotor"),
                Observacion = reader.GetString("Observacion"),
                TipoId = reader.GetInt("TipoID")
            };
        }
    }
}
