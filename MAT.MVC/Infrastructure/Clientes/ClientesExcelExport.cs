using System.Collections.Generic;
using System.Drawing;
using MAT.MVC.Models.Clientes;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MAT.MVC.Infrastructure.Clientes
{
    /// <summary>
    /// Excel de "Altas por período" (EPPlus 4.x), con el mismo estilo de cabecera que los reportes de Admin.
    /// </summary>
    public static class ClientesExcelExport
    {
        private const string FormatoFecha = "dd/mm/yyyy";

        public static byte[] BuildAltas(IList<ClienteAltaDto> filas)
        {
            var cabecera = new[] { "Cliente", "Documento", "Teléfono", "Email", "Fecha de alta", "Primera factura", "Vendedor" };
            using (var paquete = new ExcelPackage())
            {
                var hoja = paquete.Workbook.Worksheets.Add("Altas");
                for (var c = 0; c < cabecera.Length; c++) hoja.Cells[1, c + 1].Value = cabecera[c];
                using (var rango = hoja.Cells[1, 1, 1, cabecera.Length])
                {
                    rango.Style.Font.Bold = true;
                    rango.Style.Font.Color.SetColor(Color.White);
                    rango.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rango.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#4472C4"));
                }

                var fila = 2;
                foreach (var a in filas ?? new List<ClienteAltaDto>())
                {
                    hoja.Cells[fila, 1].Value = a.ClienteNombre;
                    hoja.Cells[fila, 2].Value = a.NroDocumento;
                    hoja.Cells[fila, 3].Value = a.Telefono;
                    hoja.Cells[fila, 4].Value = a.Email;
                    hoja.Cells[fila, 5].Value = a.FechaAlta;
                    hoja.Cells[fila, 5].Style.Numberformat.Format = FormatoFecha;
                    hoja.Cells[fila, 6].Value = a.PrimeraFactura;
                    hoja.Cells[fila, 6].Style.Numberformat.Format = FormatoFecha;
                    hoja.Cells[fila, 7].Value = a.VendedorNombre;
                    fila++;
                }

                hoja.View.FreezePanes(2, 1);
                if (fila > 2) hoja.Cells[1, 1, fila - 1, cabecera.Length].AutoFilter = true;
                hoja.Cells.AutoFitColumns();
                return paquete.GetAsByteArray();
            }
        }
    }
}
