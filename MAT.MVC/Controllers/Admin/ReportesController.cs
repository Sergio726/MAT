using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Mvc;
using MAT.MVC.Filters;
using MAT.MVC.Infrastructure;
using MAT.MVC.Models.Reportes;
using MAT.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MAT.MVC.Controllers.Admin
{
    /// <summary>
    /// Reportes administrativos: JSON, Excel (EPPlus) y vistas Razor bajo <c>/Admin/Reportes/...</c>.
    /// JSON: <c>{ ok, message, data }</c> camelCase (Newtonsoft).
    /// </summary>
    [Authorize]
    [InitializeSimpleMembership]
    [RequireAdministrator]
    public class ReportesController : Controller
    {
        private static readonly JsonSerializerSettings JsonReportSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Include
        };

        #region Vistas

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReporteVentas()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReportePagos()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReporteRanking()
        {
            return View();
        }

        #endregion

        #region JSON

        [HttpGet]
        public ActionResult Ventas(string from, string to, string viajeId = null, string vendedorId = null, string clienteId = null)
        {
            var q = ReportesQueryHelper.Parse(from, to, viajeId, vendedorId, clienteId, tipoVentaId: null);
            if (!q.IsValid)
                return JsonMessage(ok: false, message: q.ValidationMessage, data: null);

            if (!q.ShouldExecute)
                return JsonMessage(ok: true, message: null, data: new List<ReporteVentaRowDto>());

            try
            {
                var list = LoadVentas(q);
                return JsonMessage(ok: true, message: null, data: list);
            }
            catch (Exception ex)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(ex, "ReportesController.Ventas");
                return JsonMessage(ok: false, message: msg, data: null);
            }
        }

        [HttpGet]
        public ActionResult Pagos(string from, string to, string viajeId = null, string vendedorId = null, string clienteId = null, string tipoVentaId = null)
        {
            var q = ReportesQueryHelper.Parse(from, to, viajeId, vendedorId, clienteId, tipoVentaId);
            if (!q.IsValid)
                return JsonMessage(ok: false, message: q.ValidationMessage, data: null);

            if (!q.ShouldExecute)
                return JsonMessage(ok: true, message: null, data: new List<ReportePagoRowDto>());

            try
            {
                var list = LoadPagos(q);
                return JsonMessage(ok: true, message: null, data: list);
            }
            catch (Exception ex)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(ex, "ReportesController.Pagos");
                return JsonMessage(ok: false, message: msg, data: null);
            }
        }

        [HttpGet]
        public ActionResult Ranking(string from, string to, string viajeId = null, string vendedorId = null, string clienteId = null)
        {
            var q = ReportesQueryHelper.Parse(from, to, viajeId, vendedorId, clienteId, tipoVentaId: null);
            if (!q.IsValid)
                return JsonMessage(ok: false, message: q.ValidationMessage, data: null);

            if (!q.ShouldExecute)
                return JsonMessage(ok: true, message: null, data: new List<ReporteRankingRowDto>());

            try
            {
                var list = LoadRanking(q);
                return JsonMessage(ok: true, message: null, data: list);
            }
            catch (Exception ex)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(ex, "ReportesController.Ranking");
                return JsonMessage(ok: false, message: msg, data: null);
            }
        }

        /// <summary>
        /// Autocompletar de viajes por nombre (reportes Admin). Mínimo 2 caracteres, salvo <paramref name="recent"/> (últimos 30 por fecha salida).
        /// </summary>
        [HttpGet]
        public ActionResult BuscarViajes(string q, bool recent = false)
        {
            try
            {
                if (recent)
                {
                    var listRecent = LoadViajesBusqueda(null, recentOnly: true);
                    return JsonMessage(ok: true, message: null, data: listRecent);
                }

                var term = (q ?? string.Empty).Trim();
                if (term.Length < 2)
                    return JsonMessage(ok: true, message: null, data: new List<ReporteViajeLookupDto>());

                if (term.Length > 200)
                    term = term.Substring(0, 200);

                var list = LoadViajesBusqueda(term, recentOnly: false);
                return JsonMessage(ok: true, message: null, data: list);
            }
            catch (Exception ex)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(ex, "ReportesController.BuscarViajes");
                return JsonMessage(ok: false, message: msg, data: null);
            }
        }

        private ContentResult JsonMessage(bool ok, string message, object data)
        {
            var payload = new { ok, message, data };
            var json = JsonConvert.SerializeObject(payload, JsonReportSettings);
            return Content(json, "application/json", Encoding.UTF8);
        }

        #endregion

        #region Excel (EPPlus)

        [HttpGet]
        public ActionResult VentasExcel(string from, string to, string viajeId = null, string vendedorId = null, string clienteId = null)
        {
            var q = ReportesQueryHelper.Parse(from, to, viajeId, vendedorId, clienteId, tipoVentaId: null);
            if (!q.IsValid)
                return new HttpStatusCodeResult(400, q.ValidationMessage);

            try
            {
                var list = q.ShouldExecute ? LoadVentas(q) : new List<ReporteVentaRowDto>();
                var bytes = ReportesExcelExport.BuildVentas(list);
                return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"ventas-{DateTime.Now:yyyy-MM-dd}.xlsx");
            }
            catch (Exception ex)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(ex, "ReportesController.VentasExcel");
                return new HttpStatusCodeResult(500, msg);
            }
        }

        [HttpGet]
        public ActionResult PagosExcel(string from, string to, string viajeId = null, string vendedorId = null, string clienteId = null, string tipoVentaId = null)
        {
            var q = ReportesQueryHelper.Parse(from, to, viajeId, vendedorId, clienteId, tipoVentaId);
            if (!q.IsValid)
                return new HttpStatusCodeResult(400, q.ValidationMessage);

            try
            {
                var list = q.ShouldExecute ? LoadPagos(q) : new List<ReportePagoRowDto>();
                var bytes = ReportesExcelExport.BuildPagos(list);
                return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"pagos-{DateTime.Now:yyyy-MM-dd}.xlsx");
            }
            catch (Exception ex)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(ex, "ReportesController.PagosExcel");
                return new HttpStatusCodeResult(500, msg);
            }
        }

        /* RankingExcel retirado — el dashboard V2 usa descarga PDF en cliente. */

        #endregion

        #region Datos / SQL

        private static List<ReporteVentaRowDto> LoadVentas(ReportesQueryParseResult q)
        {
            var prms = BuildVentasParams(q);
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Reportes_Ventas", prms))
                return ReportesDataReaderMapper.ReadVentas(reader);
        }

        private static List<ReportePagoRowDto> LoadPagos(ReportesQueryParseResult q)
        {
            var prms = BuildPagosParams(q);
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Reportes_Pagos", prms))
                return ReportesDataReaderMapper.ReadPagos(reader);
        }

        private static List<ReporteRankingRowDto> LoadRanking(ReportesQueryParseResult q)
        {
            var prms = BuildRankingParams(q);
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Reportes_RankingCompras_V2", prms))
                return ReportesDataReaderMapper.ReadRanking(reader);
        }

        private static List<ReporteViajeLookupDto> LoadViajesBusqueda(string q, bool recentOnly)
        {
            var list = new List<ReporteViajeLookupDto>();
            object qVal = string.IsNullOrEmpty(q) ? (object)DBNull.Value : q;
            var prms = new[]
            {
                DBHelper.MakeParam("@q", SqlDbType.NVarChar, 200, qVal),
                DBHelper.MakeParam("@RecentOnly", SqlDbType.Bit, 0, recentOnly)
            };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Reportes_BuscarViajes", prms))
            {
                var ordId = reader.GetOrdinal("ViajeID");
                var ordDesc = reader.GetOrdinal("Descripcion");
                var ordFs = reader.GetOrdinal("FechaSalida");
                while (reader.Read())
                {
                    list.Add(new ReporteViajeLookupDto
                    {
                        Id = reader.GetGuid(ordId).ToString("D"),
                        Descripcion = reader.IsDBNull(ordDesc) ? string.Empty : reader.GetString(ordDesc),
                        FechaSalida = reader.IsDBNull(ordFs)
                            ? null
                            : reader.GetDateTime(ordFs).ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
                    });
                }
            }

            return list;
        }

        private static SqlParameter[] BuildVentasParams(ReportesQueryParseResult q)
        {
            object from = q.FromDdMmYyyy != null ? (object)q.FromDdMmYyyy : DBNull.Value;
            object to = q.ToDdMmYyyy != null ? (object)q.ToDdMmYyyy : DBNull.Value;
            object viaje = q.ViajeId.HasValue ? (object)q.ViajeId.Value : DBNull.Value;

            return new[]
            {
                DBHelper.MakeParam("@From", SqlDbType.NVarChar, 10, from),
                DBHelper.MakeParam("@To", SqlDbType.NVarChar, 10, to),
                DBHelper.MakeParam("@ViajeId", SqlDbType.UniqueIdentifier, 0, viaje),
                DBHelper.MakeParam("@VendedorId", SqlDbType.UniqueIdentifier, 0, q.VendedorId.HasValue ? (object)q.VendedorId.Value : DBNull.Value),
                DBHelper.MakeParam("@ClienteId", SqlDbType.UniqueIdentifier, 0, q.ClienteId.HasValue ? (object)q.ClienteId.Value : DBNull.Value)
            };
        }

        private static SqlParameter[] BuildPagosParams(ReportesQueryParseResult q)
        {
            object from = q.FromDdMmYyyy != null ? (object)q.FromDdMmYyyy : DBNull.Value;
            object to = q.ToDdMmYyyy != null ? (object)q.ToDdMmYyyy : DBNull.Value;
            object viaje = q.ViajeId.HasValue ? (object)q.ViajeId.Value : DBNull.Value;

            return new[]
            {
                DBHelper.MakeParam("@From", SqlDbType.NVarChar, 10, from),
                DBHelper.MakeParam("@To", SqlDbType.NVarChar, 10, to),
                DBHelper.MakeParam("@VendedorId", SqlDbType.UniqueIdentifier, 0, q.VendedorId.HasValue ? (object)q.VendedorId.Value : DBNull.Value),
                DBHelper.MakeParam("@ClienteId", SqlDbType.UniqueIdentifier, 0, q.ClienteId.HasValue ? (object)q.ClienteId.Value : DBNull.Value),
                DBHelper.MakeParam("@TipoVentaId", SqlDbType.Int, 0, q.TipoVentaId.HasValue ? (object)q.TipoVentaId.Value : DBNull.Value),
                DBHelper.MakeParam("@ViajeId", SqlDbType.UniqueIdentifier, 0, viaje)
            };
        }

        private static SqlParameter[] BuildRankingParams(ReportesQueryParseResult q)
        {
            object from = q.FromDate.HasValue ? (object)q.FromDate.Value : DBNull.Value;
            object to = q.ToDate.HasValue ? (object)q.ToDate.Value : DBNull.Value;
            object viaje = q.ViajeId.HasValue ? (object)q.ViajeId.Value : DBNull.Value;

            return new[]
            {
                DBHelper.MakeParam("@From", SqlDbType.Date, 0, from),
                DBHelper.MakeParam("@To", SqlDbType.Date, 0, to),
                DBHelper.MakeParam("@ViajeId", SqlDbType.UniqueIdentifier, 0, viaje),
                DBHelper.MakeParam("@ClienteId", SqlDbType.UniqueIdentifier, 0, q.ClienteId.HasValue ? (object)q.ClienteId.Value : DBNull.Value)
            };
        }

        #endregion
    }
}
