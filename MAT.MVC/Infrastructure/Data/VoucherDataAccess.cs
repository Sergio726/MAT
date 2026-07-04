using MAT.Entities;
using MAT.Utilities;
using System;
using System.Data;

namespace MAT.MVC.Infrastructure.Data
{
    /// <summary>
    /// Acceso a datos de Voucher vía SP + DBHelper (NetTiers F4).
    /// </summary>
    public static class VoucherDataAccess
    {
        public static Voucher GetVoucherById(Guid voucherId)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@VoucherID", SqlDbType.UniqueIdentifier, 0, voucherId)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Voucher_GetById", parameters))
            {
                if (!reader.Read())
                {
                    return null;
                }

                return new Voucher
                {
                    VoucherId = reader.GetGuid(reader.GetOrdinal("VoucherID")),
                    NroVoucher = reader.GetInt64(reader.GetOrdinal("NroVoucher")),
                    FechaEmision = reader.IsDBNull(reader.GetOrdinal("FechaEmision"))
                        ? (DateTime?)null
                        : reader.GetDateTime(reader.GetOrdinal("FechaEmision")),
                    VendedorId = reader.IsDBNull(reader.GetOrdinal("VendedorID"))
                        ? (Guid?)null
                        : reader.GetGuid(reader.GetOrdinal("VendedorID"))
                };
            }
        }

        public static void InsertVoucher(Voucher voucher)
        {
            var parameters = new[]
            {
                DBHelper.MakeParam("@VoucherID", SqlDbType.UniqueIdentifier, 0, voucher.VoucherId),
                DBHelper.MakeParam("@FechaEmision", SqlDbType.DateTime, 0, (object)voucher.FechaEmision ?? DBNull.Value),
                DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0, (object)voucher.VendedorId ?? DBNull.Value)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Voucher_Insert", parameters);
        }
    }
}
