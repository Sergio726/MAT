using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.Entities;
using MAT.Enums;
using MAT.Utilities;
using MAT.MVC.Models;
using MAT.MVC.Infrastructure;
using MAT.MVC.Infrastructure.Data;
using System.Data.SqlClient;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MAT.MVC.Controllers.PasajeroViaje
{
    public class PasajeroViajeController : Controller
    {


        public ActionResult Index(Guid Id)
        {
            //int totalCount;
            //MAT.Services.PasajeroViajeService Servicio = new PasajeroViajeService();
            //List<MAT.Entities.PasajeroViaje> EPasajeroViaje = Servicio.GetAll().Where(p => p.ViajeId == Id).ToList();
            //load Lista Pasajeros
            List<MAT.MVC.Models.PasajeroViaje> ListPasajeros = ViajeMethod.GetListPasajerosByViajeID(Id);

            return View(ListPasajeros);
        }

        public ActionResult Manifiesto(Guid Id)
        {
           
            //MAT.Services.PasajeroViajeService Servicio = new PasajeroViajeService();
            //IList<MAT.Entities.PasajeroViaje> EPasajeroViaje = Servicio.GetAll().Where(p => p.ViajeId == Id).OrderBy(ps => ps.Apellido).ToList();
            List<MAT.Entities.PasajeroViaje> EPasajeroViaje = GetListPasajeroViaje(Id);

            // NetTiers F5: la vista ya no instancia ViajeService; el viaje viaja por ViewBag
            ViewBag.Viaje = (EPasajeroViaje != null && EPasajeroViaje.Count > 0) ? ViajeDataAccess.GetById(Id) : null;

            return View(EPasajeroViaje);
        }
        public ActionResult CNRT(Guid Id)
        {

            List<MAT.Entities.PasajeroViaje> EPasajeroViaje = GetListPasajeroViaje(Id);
            return PartialView(EPasajeroViaje);
        }

        public ActionResult CNRTHojaUno()
        {
            return PartialView();
        }


        public ActionResult ListadoSimple(Guid Id)
        {

            //List<MAT.Entities.PasajeroViaje> EPasajeroViaje = new List<MAT.Entities.PasajeroViaje>();
            //try {
            //    EPasajeroViaje = GetListPasajeroViaje(Id);
            //    return PartialView(EPasajeroViaje);
            //}
            //catch {
            //    return PartialView(EPasajeroViaje);
            //}

            List<PasajeroViajeModel> LPasajeroViaje = new List<PasajeroViajeModel>();
            ViewBag.Paquete = new PaqueteStandard();
            try
            {
                LPasajeroViaje = MAT.MVC.Models.PasajeroViajeMethod.GetPasajeroViajeByViajeID(Id.ToString());

                ViewBag.Paquete = MAT.MVC.Models.PaqueteVinculos.GetPaqueteByID(Id.ToString());
                ViewBag.Viaje = ViajeDataAccess.GetById(Id);

                return View(LPasajeroViaje);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(ex, "PasajeroViajeController.ListadoSimple");
                return View(LPasajeroViaje);
            }


        }

        public ActionResult ListadoSimpleToExport(Guid Id)
        {
            List<PasajeroViajeModel> LPasajeroViaje = new List<PasajeroViajeModel>();
            try
            {
                LPasajeroViaje = PasajeroViajeMethod.GetPasajeroViajeByViajeID(Id.ToString());
                ViewBag.Viaje = ViajeDataAccess.GetById(Id);
                return View(LPasajeroViaje);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ErrorUtil.LogAndGetPublicMessage(ex, "PasajeroViajeController.ListadoSimpleToExport");
                return View(LPasajeroViaje);
            }
        }

        [HttpGet]
        public ActionResult ExportListadoPasajerosExcel(Guid Id)
        {
            try
            {
                var list = PasajeroViajeMethod.GetPasajeroViajeByViajeID(Id.ToString());
                var bytes = BuildListadoPasajerosExcelWorkbook(list);
                var fileName = $"ListadoPasajeros_{Id:N}.xlsx";
                return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                var msg = ErrorUtil.LogAndGetPublicMessage(ex, "PasajeroViajeController.ExportListadoPasajerosExcel");
                return new HttpStatusCodeResult(500, msg);
            }
        }

        private static byte[] BuildListadoPasajerosExcelWorkbook(IList<PasajeroViajeModel> list)
        {
            using (var pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add("Hoja1");
                var headers = new[]
                {
                    "N", "APELLIDO", "NOMBRES", "TIPO DE DOCUMENTO", "N° DE DOCUMENTO", "FECHA DE NAC.", "SEXO", "MENOR", "NACIONALIDAD", "TRIPULANTE"
                };
                var colWidths = new[] { 5.14, 40.57, 32.14, 22.29, 21d, 16.14, 9.57, 10.14, 16d, 19.43 };

                for (var c = 0; c < headers.Length; c++)
                {
                    var cell = ws.Cells[1, c + 1];
                    cell.Value = headers[c];
                    cell.Style.Font.Name = "Calibri";
                    cell.Style.Font.Size = 11;
                }

                for (var i = 0; i < colWidths.Length; i++)
                {
                    ws.Column(i + 1).Width = colWidths[i];
                }

                var row = 2;
                var idx = 1;
                foreach (var item in list)
                {
                    ws.Cells[row, 1].Value = idx++;
                    ws.Cells[row, 2].Value = (item.Apellido ?? "").ToUpperInvariant();
                    ws.Cells[row, 3].Value = (item.Nombre ?? "").ToUpperInvariant();
                    ws.Cells[row, 4].Value = GetTipoDocumentoDescription(item.TipoDocumento);
                    ws.Cells[row, 5].Value = FormatNroDocumentoListado(item.NroDocumento);
                    ws.Cells[row, 6].Value = item.FechaNacimiento ?? "";
                    ws.Cells[row, 7].Value = FormatSexoLetra(item.Sexo);
                    ws.Cells[row, 8].Value = item.EsMenorVinculado ? 1 : 0;
                    ws.Cells[row, 9].Value = item.Nacionalidad ?? "";
                    ws.Cells[row, 10].Value = "";

                    for (var c = 1; c <= 10; c++)
                    {
                        var cell = ws.Cells[row, c];
                        cell.Style.Font.Name = "Times New Roman";
                        cell.Style.Font.Size = 11;
                    }

                    row++;
                }

                var lastRow = Math.Max(1, row - 1);
                ApplyListadoModeloThinBorders(ws, 1, lastRow, 1, 10);

                return pck.GetAsByteArray();
            }
        }

        private static void ApplyListadoModeloThinBorders(ExcelWorksheet ws, int row1, int row2, int col1, int col2)
        {
            for (var r = row1; r <= row2; r++)
            {
                for (var c = col1; c <= col2; c++)
                {
                    var b = ws.Cells[r, c].Style.Border;
                    b.Top.Style = ExcelBorderStyle.Thin;
                    b.Bottom.Style = ExcelBorderStyle.Thin;
                    b.Left.Style = ExcelBorderStyle.Thin;
                    b.Right.Style = ExcelBorderStyle.Thin;
                }
            }
        }

        private static string FormatNroDocumentoListado(string nro)
        {
            if (string.IsNullOrEmpty(nro))
            {
                return "";
            }
            return nro.Replace(".", "");
        }

        private static string GetTipoDocumentoDescription(int tipoDocumento)
        {
            if (!Enum.IsDefined(typeof(eTipoDocumento), tipoDocumento))
            {
                return tipoDocumento.ToString();
            }
            return ((eTipoDocumento)tipoDocumento).GetDescription();
        }

        private static string FormatSexoLetra(int? sexo)
        {
            if (!sexo.HasValue || !Enum.IsDefined(typeof(eSexo), sexo.Value))
            {
                return "";
            }
            return sexo.Value == (int)eSexo.Masculino ? "M" : sexo.Value == (int)eSexo.Femenino ? "F" : "";
        }

        public List<MAT.Entities.PasajeroViaje> GetListPasajeroViaje(Guid Id)
        {

            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, Convert.ToString(Id)),
                    };
            List<MAT.Entities.PasajeroViaje> EPasajeroViaje = new List<MAT.Entities.PasajeroViaje>();
            
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_PasajeroViaje_GetByViajeID", dbParams))
            {
                while (_reader.Read())
                {
                    MAT.Entities.PasajeroViaje item = new MAT.Entities.PasajeroViaje();
                    item.ViajeId = new Guid(_reader["ViajeID"].ToString());
                    item.PersonaId = new Guid(_reader["PersonaID"].ToString());
                    item.Apellido = _reader["Apellido"].ToString();
                    item.Nombre = _reader["Nombre"].ToString();
                    item.TipoDocumento = Convert.ToInt32(_reader["TipoDocumento"].ToString());
                    item.NroDocumento = _reader["NroDocumento"].ToString();
                    item.FechaNacimiento = Convert.ToDateTime(_reader["FechaNacimiento"].ToString());
                    item.Telefono = _reader["Telefono"].ToString();
                    item.Sexo = Convert.ToInt32(_reader["Sexo"]);
                    item.Nacionalidad = _reader["Nacionalidad"].ToString();
                    item.PaisResidencia = _reader["PaisResidencia"].ToString();
                    item.Ocupacion = _reader["Ocupacion"].ToString();
                    EPasajeroViaje.Add(item);
                }
            }

            return EPasajeroViaje;
        }
        
    }
}
