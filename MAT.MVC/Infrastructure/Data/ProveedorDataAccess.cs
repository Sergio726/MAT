using MAT.Entities;
using MAT.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Acceso a datos de la entidad Proveedor vía SP + DBHelper (NetTiers F7). Reemplaza ProveedorService.
    /// Solo columnas conocidas por la entidad (no toca Domicilio ni Estado, paridad con NetTiers).
    /// </summary>
    public static class ProveedorDataAccess
    {
        public static Proveedor GetById(Guid proveedorId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ProveedorID", SqlDbType.UniqueIdentifier, 0, proveedorId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Proveedor_GetEntityById", parameters))
            {
                return reader.Read() ? Map(reader) : null;
            }
        }

        public static void Insert(Proveedor proveedor)
        {
            if (proveedor.ProveedorId == Guid.Empty)
            {
                proveedor.ProveedorId = Guid.NewGuid();
            }
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Proveedor_InsertEntity", BuildParams(proveedor));
        }

        public static void Update(Proveedor proveedor)
        {
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Proveedor_UpdateEntity", BuildParams(proveedor));
        }

        public static void Delete(Guid proveedorId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@ProveedorID", SqlDbType.UniqueIdentifier, 0, proveedorId)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Proveedor_DeleteEntity", parameters);
        }

        private static SqlParameter[] BuildParams(Proveedor p)
        {
            return new[]
            {
                DBHelper.MakeParam("@ProveedorID", SqlDbType.UniqueIdentifier, 0, p.ProveedorId),
                DBHelper.MakeParam("@RazonSocial", SqlDbType.VarChar, 50, (object)p.RazonSocial ?? DBNull.Value),
                DBHelper.MakeParam("@Telefono", SqlDbType.VarChar, 50, (object)p.Telefono ?? DBNull.Value),
                DBHelper.MakeParam("@Fax", SqlDbType.VarChar, 50, (object)p.Fax ?? DBNull.Value),
                DBHelper.MakeParam("@Web", SqlDbType.VarChar, 50, (object)p.Web ?? DBNull.Value),
                DBHelper.MakeParam("@Email", SqlDbType.VarChar, 50, (object)p.Email ?? DBNull.Value),
                DBHelper.MakeParam("@Idioma", SqlDbType.VarChar, 50, (object)p.Idioma ?? DBNull.Value),
                DBHelper.MakeParam("@CondicionIva", SqlDbType.Int, 0, (object)p.CondicionIva ?? DBNull.Value),
                DBHelper.MakeParam("@Cuit", SqlDbType.VarChar, 50, (object)p.Cuit ?? DBNull.Value),
                DBHelper.MakeParam("@FormaPago", SqlDbType.Int, 0, (object)p.FormaPago ?? DBNull.Value),
                DBHelper.MakeParam("@LocalidadID", SqlDbType.Int, 0, (object)p.LocalidadId ?? DBNull.Value)
            };
        }

        private static Proveedor Map(SqlDataReader reader)
        {
            return new Proveedor
            {
                ProveedorId = reader.GetGuid("ProveedorID"),
                RazonSocial = reader.GetString("RazonSocial"),
                Telefono = reader.GetString("Telefono"),
                Fax = reader.GetString("Fax"),
                Web = reader.GetString("Web"),
                Email = reader.GetString("Email"),
                Idioma = reader.GetString("Idioma"),
                CondicionIva = reader.GetNullableInt("CondicionIva"),
                Cuit = reader.GetString("Cuit"),
                FormaPago = reader.GetNullableInt("FormaPago"),
                LocalidadId = reader.GetNullableInt("LocalidadID")
            };
        }
    }
}
