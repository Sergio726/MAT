using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using MAT.MVC.Models.Reportes;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Genera libros Excel para reportes administrativos (EPPlus 4.x). Estilo de cabecera alineado a <c>FacturaFiscalController.ExportExcel</c>.
    /// </summary>
    public static class ReportesExcelExport
    {
        private static readonly CultureInfo EsAr = CultureInfo.GetCultureInfo("es-AR");

        public static byte[] BuildVentas(IList<ReporteVentaRowDto> rows)
        {
            if (rows == null) rows = new List<ReporteVentaRowDto>();

            var headers = new[]
            {
                "Viaje ID", "Viaje", "Fecha Salida", "Cantidad Butacas", "Vendedor ID", "Vendedor",
                "Cliente ID", "Cliente", "Factura ID", "Fecha Factura", "Estado", "Moneda",
                "Total Factura", "Monto Pagado", "Saldo"
            };

            using (var pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add("Ventas");
                WriteHeader(ws, headers);

                var r = 2;
                foreach (var row in rows)
                {
                    SetGuidText(ws.Cells[r, 1], row.ViajeId);
                    ws.Cells[r, 2].Value = row.ViajeDescripcion;
                    ws.Cells[r, 3].Value = row.FechaSalida.HasValue ? row.FechaSalida.Value.ToString("d", EsAr) : "";
                    ws.Cells[r, 4].Value = row.CantidadButacas;
                    SetGuidText(ws.Cells[r, 5], row.VendedorId);
                    ws.Cells[r, 6].Value = row.VendedorFullName;
                    SetGuidText(ws.Cells[r, 7], row.ClienteId);
                    ws.Cells[r, 8].Value = row.ClienteFullName;
                    SetGuidText(ws.Cells[r, 9], row.FacturaId);
                    ws.Cells[r, 10].Value = row.FacturaFecha.HasValue ? row.FacturaFecha.Value.ToString("d", EsAr) : "";
                    ws.Cells[r, 11].Value = row.FacturaEstado;
                    ws.Cells[r, 12].Value = row.MonedaTipo;
                    SetDecimal(ws.Cells[r, 13], row.TotalFactura);
                    SetDecimal(ws.Cells[r, 14], row.MontoPagado);
                    SetDecimal(ws.Cells[r, 15], row.Saldo);
                    r++;
                }

                ws.Cells.AutoFitColumns();
                return pck.GetAsByteArray();
            }
        }

        public static byte[] BuildPagos(IList<ReportePagoRowDto> rows)
        {
            if (rows == null) rows = new List<ReportePagoRowDto>();

            var headers = new[]
            {
                "Factura ID", "Fecha Pago", "Monto", "Moneda", "Tipo Pago ID", "Tipo Pago",
                "Tipo Venta ID", "Tipo Venta", "Cant. Tipo Pago", "Ranking Tipo Pago",
                "Vendedor ID", "Vendedor", "Cliente ID", "Cliente", "Viaje"
            };

            using (var pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add("Pagos");
                WriteHeader(ws, headers);

                var r = 2;
                foreach (var row in rows)
                {
                    SetGuidText(ws.Cells[r, 1], row.FacturaId);
                    ws.Cells[r, 2].Value = row.FechaPago.HasValue ? row.FechaPago.Value.ToString("g", EsAr) : "";
                    SetDecimal(ws.Cells[r, 3], row.Monto);
                    ws.Cells[r, 4].Value = row.MonedaTipo;
                    ws.Cells[r, 5].Value = row.PagoTipoId;
                    ws.Cells[r, 6].Value = row.PagoDescripcion;
                    ws.Cells[r, 7].Value = row.TipoVentaId;
                    ws.Cells[r, 8].Value = row.TipoVentaDescripcion;
                    ws.Cells[r, 9].Value = row.CantidadTipoPago;
                    ws.Cells[r, 10].Value = row.RankingTipoPago;
                    SetGuidText(ws.Cells[r, 11], row.VendedorId);
                    ws.Cells[r, 12].Value = row.VendedorFullName;
                    SetGuidText(ws.Cells[r, 13], row.ClienteId);
                    ws.Cells[r, 14].Value = row.ClienteFullName;
                    ws.Cells[r, 15].Value = row.Viaje;
                    r++;
                }

                ws.Cells.AutoFitColumns();
                return pck.GetAsByteArray();
            }
        }

        public static byte[] BuildRanking(IList<ReporteRankingRowDto> rows)
        {
            if (rows == null) rows = new List<ReporteRankingRowDto>();

            var headers = new[]
            {
                "Factura ID", "Fecha", "Cliente ID", "Cliente", "Viaje ID", "Viaje", "Fecha Salida",
                "Pasajes x Factura", "Viajes Comprados", "Pasajes Comprados", "Clientes Eligieron Viaje",
                "Ranking Clientes", "Ranking Viajes"
            };

            using (var pck = new ExcelPackage())
            {
                var ws = pck.Workbook.Worksheets.Add("Ranking");
                WriteHeader(ws, headers);

                var r = 2;
                foreach (var row in rows)
                {
                    SetGuidText(ws.Cells[r, 1], row.FacturaId);
                    ws.Cells[r, 2].Value = row.Fecha.HasValue ? row.Fecha.Value.ToString("d", EsAr) : "";
                    SetGuidText(ws.Cells[r, 3], row.ClienteId);
                    ws.Cells[r, 4].Value = row.ClienteFullName;
                    SetGuidText(ws.Cells[r, 5], row.ViajeId);
                    ws.Cells[r, 6].Value = row.ViajeDescripcion;
                    ws.Cells[r, 7].Value = row.ViajeFechaSalida.HasValue ? row.ViajeFechaSalida.Value.ToString("d", EsAr) : "";
                    ws.Cells[r, 8].Value = row.CantidadPasajesXFactura;
                    ws.Cells[r, 9].Value = row.CantViajesCompradosXCliente;
                    ws.Cells[r, 10].Value = row.CantPasajesCompradosXCliente;
                    ws.Cells[r, 11].Value = row.CantClientesEligieronViaje;
                    ws.Cells[r, 12].Value = row.RankingClientesCompradoresViajes;
                    ws.Cells[r, 13].Value = row.RankingViajes;
                    r++;
                }

                ws.Cells.AutoFitColumns();
                return pck.GetAsByteArray();
            }
        }

        private static void WriteHeader(ExcelWorksheet ws, string[] headers)
        {
            for (var i = 0; i < headers.Length; i++)
            {
                var cell = ws.Cells[1, i + 1];
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(68, 114, 196));
                cell.Style.Font.Color.SetColor(Color.White);
            }
        }

        /// <summary>Fuerza formato texto para GUIDs (evita notación científica en Excel).</summary>
        private static void SetGuidText(ExcelRange cell, string value)
        {
            cell.Style.Numberformat.Format = "@";
            cell.Value = value ?? "";
        }

        private static void SetDecimal(ExcelRange cell, decimal? value)
        {
            if (value.HasValue)
            {
                cell.Value = value.Value;
                cell.Style.Numberformat.Format = "#,##0.00";
            }
            else
            {
                cell.Value = "";
            }
        }
    }
}
