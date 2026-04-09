using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MAT.MVC.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Excel (EPPlus) para el resumen de pagos por viaje (Admin / <c>GridResumenPagos</c>).
    /// </summary>
    public static class AdminResumenPagosExcelExport
    {
        public static byte[] Build(IList<PagoModel> rows, Guid viajeId)
        {
            if (rows == null) rows = new List<PagoModel>();

            var headers = new[] { "Cliente", "Fecha", "Tipo de pago", "Nro. transacción", "Monto" };

            using (var pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add("Pagos");
                ws.Cells[1, 1].Value = "Resumen de pagos por viaje";
                ws.Cells[1, 1].Style.Font.Bold = true;
                ws.Cells[2, 1].Value = "Viaje ID:";
                ws.Cells[2, 2].Value = viajeId.ToString();
                ws.Cells[2, 1].Style.Font.Bold = true;

                var headerRow = 4;
                for (var i = 0; i < headers.Length; i++)
                {
                    var cell = ws.Cells[headerRow, i + 1];
                    cell.Value = headers[i];
                    cell.Style.Font.Bold = true;
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(68, 114, 196));
                    cell.Style.Font.Color.SetColor(Color.White);
                }

                var r = headerRow + 1;
                foreach (var row in rows)
                {
                    ws.Cells[r, 1].Value = row.Cliente ?? "";
                    ws.Cells[r, 2].Value = row.sFechaPago ?? "";
                    ws.Cells[r, 3].Value = row.TipoPagoDescripcion ?? "";
                    ws.Cells[r, 4].Value = row.TransaccionID ?? "";
                    SetDecimal(ws.Cells[r, 5], row.Monto);
                    r++;
                }

                var total = rows.Sum(x => x.Monto);
                ws.Cells[r, 1].Value = "Total";
                ws.Cells[r, 1].Style.Font.Bold = true;
                SetDecimal(ws.Cells[r, 5], total);
                ws.Cells[r, 5].Style.Font.Bold = true;

                ws.Cells.AutoFitColumns();
                return pck.GetAsByteArray();
            }
        }

        private static void SetDecimal(ExcelRange cell, decimal value)
        {
            cell.Value = value;
            cell.Style.Numberformat.Format = "#,##0.00";
        }
    }
}
