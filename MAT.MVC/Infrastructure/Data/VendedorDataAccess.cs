using MAT.Entities;
using MAT.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Acceso a datos de la entidad Vendedor vía SP + DBHelper (NetTiers F7). Reemplaza VendedorService.
    /// </summary>
    public static class VendedorDataAccess
    {
        public static Vendedor GetById(Guid vendedorId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0, vendedorId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Vendedor_GetEntityById", parameters))
            {
                return reader.Read() ? Map(reader) : null;
            }
        }

        public static void Insert(Vendedor vendedor)
        {
            if (vendedor.VendedorId == Guid.Empty)
            {
                vendedor.VendedorId = Guid.NewGuid();
            }
            var parameters = new[]
            {
                DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0, vendedor.VendedorId),
                DBHelper.MakeParam("@Descripcion", SqlDbType.VarChar, 100, (object)vendedor.Descripcion ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Vendedor_InsertEntity", parameters);
        }

        private static Vendedor Map(SqlDataReader reader)
        {
            return new Vendedor
            {
                VendedorId = reader.GetGuid("VendedorID"),
                Descripcion = reader.GetString("Descripcion")
            };
        }
    }
}
