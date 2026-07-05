using MAT.MVC.Models.Reportes;
using MAT.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Carga pagos para <c>/Home/HistorialPagos</c> vía <c>usp_MAT_HistorialPagos_GetByRango</c>.
    /// Tabla principal: <c>Pago</c> (sin depender de <c>Cuenta</c> legacy). Filtro por <c>FechaPago</c>.
    /// Scoping opcional por vendedor (<c>pa.VendedorId</c>).
    /// </summary>
    public static class HistorialPagosLoader
    {
        public static List<ReportePagoRowDto> Load(ReportesQueryParseResult q)
        {
            if (q == null || !q.ShouldExecute)
                return new List<ReportePagoRowDto>();

            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_HistorialPagos_GetByRango", BuildPagosParams(q)))
                return ReportesDataReaderMapper.ReadPagos(reader);
        }

        private static SqlParameter[] BuildPagosParams(ReportesQueryParseResult q)
        {
            object from = q.FromDdMmYyyy != null ? (object)q.FromDdMmYyyy : System.DBNull.Value;
            object to = q.ToDdMmYyyy != null ? (object)q.ToDdMmYyyy : System.DBNull.Value;

            return new[]
            {
                DBHelper.MakeParam("@From", SqlDbType.NVarChar, 10, from),
                DBHelper.MakeParam("@To", SqlDbType.NVarChar, 10, to),
                DBHelper.MakeParam("@VendedorId", SqlDbType.UniqueIdentifier, 0, q.VendedorId.HasValue ? (object)q.VendedorId.Value : System.DBNull.Value),
                DBHelper.MakeParam("@TipoVentaId", SqlDbType.Int, 0, q.TipoVentaId.HasValue ? (object)q.TipoVentaId.Value : System.DBNull.Value)
            };
        }
    }
}
