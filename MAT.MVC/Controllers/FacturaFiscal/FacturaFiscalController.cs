using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MAT.MVC.Models;
using MAT.Enums;
using MAT.Utilities;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace MAT.MVC.Controllers.FacturaFiscal
{
    [Authorize]
    public class FacturaFiscalController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en FacturaFiscalController.Index: {ex.Message}", 1);
                ViewBag.Error = "Error al cargar el listado de facturas fiscales.";
                return View();
            }
        }

        public ActionResult Create(int? tipo)
        {
            try
            {
                ViewBag.TipoFactura = tipo ?? 1;
                return View();
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en FacturaFiscalController.Create: {ex.Message}", 1);
                ViewBag.Error = "Error al cargar el formulario.";
                return View();
            }
        }

        public ActionResult Proveedores()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en FacturaFiscalController.Proveedores: {ex.Message}", 1);
                ViewBag.Error = "Error al cargar el listado de proveedores.";
                return View();
            }
        }

        [HttpPost]
        public JsonResult GetAll(int? tipo = null, int? tipoComprobante = null, string proveedorId = null, string clienteId = null, string cuit = null, int? estado = null, string fechaDesde = null, string fechaHasta = null, string busqueda = null)
        {
            try
            {
                Guid? provGuid = null;
                Guid? cliGuid = null;
                DateTime? fechaDesdeDate = null;
                DateTime? fechaHastaDate = null;

                if (!string.IsNullOrWhiteSpace(proveedorId))
                {
                    Guid parsed;
                    if (Guid.TryParse(proveedorId, out parsed)) provGuid = parsed;
                }
                if (!string.IsNullOrWhiteSpace(clienteId))
                {
                    Guid parsed;
                    if (Guid.TryParse(clienteId, out parsed)) cliGuid = parsed;
                }
                if (!string.IsNullOrWhiteSpace(fechaDesde))
                {
                    DateTime parsed;
                    if (DateTime.TryParse(fechaDesde, out parsed)) fechaDesdeDate = parsed;
                }
                if (!string.IsNullOrWhiteSpace(fechaHasta))
                {
                    DateTime parsed;
                    if (DateTime.TryParse(fechaHasta, out parsed)) fechaHastaDate = parsed;
                }

                var facturas = FacturaFiscalMethod.GetAll(tipo, tipoComprobante, provGuid, cliGuid, cuit, estado, fechaDesdeDate, fechaHastaDate, busqueda);

                var resultado = facturas.Select(f => new
                {
                    facturaFiscalId = f.FacturaFiscalID.ToString(),
                    tipo = (int)f.Tipo,
                    tipoNombre = f.Tipo.ToString(),
                    tipoComprobante = (int)f.TipoComprobante,
                    tipoComprobanteNombre = GetComprobanteNombre(f.TipoComprobante),
                    puntoVenta = f.PuntoVenta,
                    numero = f.Numero,
                    numeroCompleto = f.NumeroCompleto,
                    fechaEmision = f.FechaEmision.ToString("dd/MM/yyyy"),
                    fechaVencimiento = f.FechaVencimiento?.ToString("dd/MM/yyyy") ?? "",
                    proveedorId = f.ProveedorID?.ToString(),
                    clienteId = f.ClienteID?.ToString(),
                    cuit = f.Cuit,
                    condicionIva = (int)f.CondicionIva,
                    neto = f.Neto,
                    iva = f.Iva,
                    otrosImpuestos = f.OtrosImpuestos,
                    total = f.Total,
                    moneda = f.Moneda,
                    cae = f.CAE,
                    archivoAdjunto = f.ArchivoAdjunto,
                    estado = (int)f.Estado,
                    estadoNombre = f.Estado.ToString(),
                    alicuotaIva = (int)f.AlicuotaIva,
                    percepciones = f.Percepciones,
                    condicionVenta = f.CondicionVenta,
                    observaciones = f.Observaciones,
                    proveedorRazonSocial = f.ProveedorRazonSocial,
                    clienteNombre = f.ClienteNombre,
                    monedaDescripcion = f.MonedaDescripcion
                }).ToList();

                return Json(new { success = true, data = resultado, total = resultado.Count });
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en FacturaFiscalController.GetAll: {ex.Message} - {ex.StackTrace}", 1);
                return Json(new { success = false, message = "Error al obtener las facturas fiscales." });
            }
        }

        [HttpPost]
        public JsonResult GetDetalle(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                    return Json(new { success = false, message = "ID requerido." });

                Guid facturaId;
                if (!Guid.TryParse(id, out facturaId))
                    return Json(new { success = false, message = "ID no valido." });

                var f = FacturaFiscalMethod.GetById(facturaId);
                if (f == null)
                    return Json(new { success = false, message = "Factura no encontrada." });

                var resultado = new
                {
                    facturaFiscalId = f.FacturaFiscalID.ToString(),
                    tipo = (int)f.Tipo,
                    tipoNombre = f.Tipo.ToString(),
                    tipoComprobante = (int)f.TipoComprobante,
                    tipoComprobanteNombre = GetComprobanteNombre(f.TipoComprobante),
                    puntoVenta = f.PuntoVenta,
                    numero = f.Numero,
                    numeroCompleto = f.NumeroCompleto,
                    fechaEmision = f.FechaEmision.ToString("dd/MM/yyyy"),
                    fechaVencimiento = f.FechaVencimiento?.ToString("dd/MM/yyyy") ?? "",
                    proveedorId = f.ProveedorID?.ToString(),
                    clienteId = f.ClienteID?.ToString(),
                    cuit = f.Cuit,
                    condicionIva = (int)f.CondicionIva,
                    neto = f.Neto,
                    iva = f.Iva,
                    otrosImpuestos = f.OtrosImpuestos,
                    total = f.Total,
                    moneda = f.Moneda,
                    cae = f.CAE,
                    archivoAdjunto = f.ArchivoAdjunto,
                    estado = (int)f.Estado,
                    estadoNombre = f.Estado.ToString(),
                    alicuotaIva = (int)f.AlicuotaIva,
                    percepciones = f.Percepciones,
                    condicionVenta = f.CondicionVenta,
                    observaciones = f.Observaciones,
                    proveedorRazonSocial = f.ProveedorRazonSocial,
                    clienteNombre = f.ClienteNombre,
                    monedaDescripcion = f.MonedaDescripcion,
                    createdAt = f.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                    updatedAt = f.UpdatedAt?.ToString("dd/MM/yyyy HH:mm") ?? ""
                };

                return Json(new { success = true, factura = resultado });
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en FacturaFiscalController.GetDetalle: {ex.Message}", 1);
                return Json(new { success = false, message = "Error al obtener el detalle." });
            }
        }

        [HttpPost]
        public JsonResult CreateFactura(int tipo, int tipoComprobante, int puntoVenta, long numero,
            string fechaEmision, string fechaVencimiento, string proveedorId, string clienteId,
            string cuit, int condicionIva, decimal neto, decimal iva, decimal otrosImpuestos,
            decimal total, int moneda, string cae, int alicuotaIva, decimal percepciones,
            string condicionVenta, string observaciones, string archivoAdjunto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cuit))
                    return Json(new { success = false, message = "El CUIT es requerido." });
                if (puntoVenta <= 0)
                    return Json(new { success = false, message = "El punto de venta es requerido." });
                if (numero <= 0)
                    return Json(new { success = false, message = "El numero de comprobante es requerido." });

                DateTime fechaEmisionDate;
                if (!DateTime.TryParse(fechaEmision, out fechaEmisionDate))
                    return Json(new { success = false, message = "Fecha de emision invalida." });

                // Check duplicate
                bool isDuplicate = FacturaFiscalMethod.CheckDuplicate(cuit, tipoComprobante, puntoVenta, numero);
                if (isDuplicate)
                    return Json(new { success = false, message = "Ya existe una factura con el mismo CUIT, tipo de comprobante, punto de venta y numero." });

                var factura = new FacturaFiscalStandard
                {
                    FacturaFiscalID = Guid.NewGuid(),
                    Tipo = (eTipoFacturaFiscal)tipo,
                    TipoComprobante = (eTipoComprobante)tipoComprobante,
                    PuntoVenta = puntoVenta,
                    Numero = numero,
                    FechaEmision = fechaEmisionDate,
                    Cuit = cuit.Trim(),
                    CondicionIva = (eCondicionIVA)condicionIva,
                    Neto = neto,
                    Iva = iva,
                    OtrosImpuestos = otrosImpuestos,
                    Total = total,
                    Moneda = moneda,
                    CAE = cae,
                    ArchivoAdjunto = archivoAdjunto,
                    Estado = eEstadoFacturaFiscal.Activa,
                    AlicuotaIva = (eAlicuotaIva)alicuotaIva,
                    Percepciones = percepciones,
                    CondicionVenta = condicionVenta,
                    Observaciones = observaciones
                };

                DateTime fechaVencDate;
                if (!string.IsNullOrWhiteSpace(fechaVencimiento) && DateTime.TryParse(fechaVencimiento, out fechaVencDate))
                    factura.FechaVencimiento = fechaVencDate;

                Guid provGuid;
                if (!string.IsNullOrWhiteSpace(proveedorId) && Guid.TryParse(proveedorId, out provGuid))
                    factura.ProveedorID = provGuid;

                Guid cliGuid;
                if (!string.IsNullOrWhiteSpace(clienteId) && Guid.TryParse(clienteId, out cliGuid))
                    factura.ClienteID = cliGuid;

                FacturaFiscalMethod.Insert(factura);

                return Json(new { success = true, message = "Factura creada exitosamente.", facturaFiscalId = factura.FacturaFiscalID.ToString() });
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en FacturaFiscalController.CreateFactura: {ex.Message} - {ex.StackTrace}", 1);
                return Json(new { success = false, message = "Error al crear la factura." });
            }
        }

        [HttpPost]
        public JsonResult AnularFactura(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                    return Json(new { success = false, message = "ID requerido." });

                Guid facturaId;
                if (!Guid.TryParse(id, out facturaId))
                    return Json(new { success = false, message = "ID no valido." });

                bool ok = FacturaFiscalMethod.Anular(facturaId);
                if (ok)
                    return Json(new { success = true, message = "Factura anulada correctamente." });

                return Json(new { success = false, message = "No se pudo anular la factura. Verifique que existe y esta activa." });
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en FacturaFiscalController.AnularFactura: {ex.Message}", 1);
                return Json(new { success = false, message = "Error al anular la factura." });
            }
        }

        [HttpPost]
        public JsonResult CheckDuplicate(string cuit, int tipoComprobante, int puntoVenta, long numero, string excludeId = null)
        {
            try
            {
                Guid? exId = null;
                Guid parsed;
                if (!string.IsNullOrWhiteSpace(excludeId) && Guid.TryParse(excludeId, out parsed))
                    exId = parsed;

                bool isDuplicate = FacturaFiscalMethod.CheckDuplicate(cuit, tipoComprobante, puntoVenta, numero, exId);
                return Json(new { success = true, isDuplicate = isDuplicate });
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en FacturaFiscalController.CheckDuplicate: {ex.Message}", 1);
                return Json(new { success = false, message = "Error al verificar duplicados." });
            }
        }

        [HttpPost]
        public JsonResult GetTotales(int? tipo = null, int? tipoComprobante = null, string proveedorId = null, string clienteId = null, string cuit = null, int? estado = null, string fechaDesde = null, string fechaHasta = null, string busqueda = null)
        {
            try
            {
                Guid? provGuid = null;
                Guid? cliGuid = null;
                DateTime? fechaDesdeDate = null;
                DateTime? fechaHastaDate = null;

                Guid parsed;
                if (!string.IsNullOrWhiteSpace(proveedorId) && Guid.TryParse(proveedorId, out parsed)) provGuid = parsed;
                if (!string.IsNullOrWhiteSpace(clienteId) && Guid.TryParse(clienteId, out parsed)) cliGuid = parsed;
                DateTime dateParsed;
                if (!string.IsNullOrWhiteSpace(fechaDesde) && DateTime.TryParse(fechaDesde, out dateParsed)) fechaDesdeDate = dateParsed;
                if (!string.IsNullOrWhiteSpace(fechaHasta) && DateTime.TryParse(fechaHasta, out dateParsed)) fechaHastaDate = dateParsed;

                var totales = FacturaFiscalMethod.GetTotales(tipo, tipoComprobante, provGuid, cliGuid, cuit, estado, fechaDesdeDate, fechaHastaDate, busqueda);

                return Json(new
                {
                    success = true,
                    totalNeto = totales.TotalNeto,
                    totalIva = totales.TotalIva,
                    totalOtrosImpuestos = totales.TotalOtrosImpuestos,
                    totalGeneral = totales.TotalGeneral,
                    cantidadRegistros = totales.CantidadRegistros
                });
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en FacturaFiscalController.GetTotales: {ex.Message}", 1);
                return Json(new { success = false, message = "Error al obtener totales." });
            }
        }

        [HttpPost]
        public JsonResult GetProveedores()
        {
            try
            {
                var proveedores = ProveedorFiscalMethod.GetAll(1); // Solo activos
                var resultado = proveedores.Select(p => new
                {
                    proveedorId = p.ProveedorID.ToString(),
                    razonSocial = p.RazonSocial,
                    cuit = p.Cuit,
                    condicionIva = p.CondicionIva,
                    domicilio = p.Domicilio,
                    email = p.Email,
                    telefono = p.Telefono,
                    estado = p.Estado
                }).ToList();
                return Json(new { success = true, data = resultado });
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en FacturaFiscalController.GetProveedores: {ex.Message}", 1);
                return Json(new { success = false, message = "Error al obtener proveedores." });
            }
        }

        [HttpPost]
        public JsonResult GetAllProveedores()
        {
            try
            {
                var proveedores = ProveedorFiscalMethod.GetAll(null); // Todos (activos e inactivos)
                var resultado = proveedores.Select(p => new
                {
                    proveedorId = p.ProveedorID.ToString(),
                    razonSocial = p.RazonSocial,
                    cuit = p.Cuit,
                    condicionIva = p.CondicionIva,
                    domicilio = p.Domicilio,
                    email = p.Email,
                    telefono = p.Telefono,
                    estado = p.Estado
                }).ToList();
                return Json(new { success = true, data = resultado });
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en FacturaFiscalController.GetAllProveedores: {ex.Message}", 1);
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        [HttpPost]
        public JsonResult GetProveedorById(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                    return Json(new { success = false, message = "ID requerido." });

                Guid provId;
                if (!Guid.TryParse(id, out provId))
                    return Json(new { success = false, message = "ID no valido." });

                var p = ProveedorFiscalMethod.GetById(provId);
                if (p == null)
                    return Json(new { success = false, message = "Proveedor no encontrado." });

                return Json(new
                {
                    success = true,
                    proveedor = new
                    {
                        proveedorId = p.ProveedorID.ToString(),
                        razonSocial = p.RazonSocial,
                        cuit = p.Cuit,
                        condicionIva = p.CondicionIva,
                        domicilio = p.Domicilio,
                        email = p.Email,
                        telefono = p.Telefono,
                        estado = p.Estado
                    }
                });
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en FacturaFiscalController.GetProveedorById: {ex.Message}", 1);
                return Json(new { success = false, message = "Error al obtener proveedor." });
            }
        }

        [HttpPost]
        public JsonResult CreateProveedor(string razonSocial, string cuit, int? condicionIva, string domicilio, string email, string telefono)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(razonSocial))
                    return Json(new { success = false, message = "La razon social es requerida." });
                if (string.IsNullOrWhiteSpace(cuit))
                    return Json(new { success = false, message = "El CUIT es requerido." });

                var prov = new ProveedorFiscalStandard
                {
                    ProveedorID = Guid.NewGuid(),
                    RazonSocial = razonSocial.Trim(),
                    Cuit = cuit.Trim(),
                    CondicionIva = condicionIva,
                    Domicilio = domicilio,
                    Email = email,
                    Telefono = telefono,
                    Estado = 1
                };

                ProveedorFiscalMethod.Insert(prov);

                return Json(new { success = true, message = "Proveedor creado exitosamente.", proveedorId = prov.ProveedorID.ToString(), razonSocial = prov.RazonSocial, cuit = prov.Cuit });
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Contains("CUIT") ? ex.Message : "Error al crear el proveedor.";
                MATLogger.Log($"Error en FacturaFiscalController.CreateProveedor: {ex.Message}", 1);
                return Json(new { success = false, message = msg });
            }
        }

        [HttpPost]
        public JsonResult UpdateProveedor(string proveedorId, string razonSocial, string cuit, int? condicionIva, string domicilio, string email, string telefono, int estado)
        {
            try
            {
                Guid provGuid;
                if (!Guid.TryParse(proveedorId, out provGuid))
                    return Json(new { success = false, message = "ID no valido." });

                if (string.IsNullOrWhiteSpace(razonSocial))
                    return Json(new { success = false, message = "La razon social es requerida." });

                var prov = new ProveedorFiscalStandard
                {
                    ProveedorID = provGuid,
                    RazonSocial = razonSocial.Trim(),
                    Cuit = (cuit ?? "").Trim(),
                    CondicionIva = condicionIva,
                    Domicilio = domicilio,
                    Email = email,
                    Telefono = telefono,
                    Estado = estado
                };

                ProveedorFiscalMethod.Update(prov);

                return Json(new { success = true, message = "Proveedor actualizado correctamente." });
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Contains("CUIT") ? ex.Message : "Error al actualizar el proveedor.";
                MATLogger.Log($"Error en FacturaFiscalController.UpdateProveedor: {ex.Message}", 1);
                return Json(new { success = false, message = msg });
            }
        }

        [HttpGet]
        public ActionResult ExportExcel(int tipo, string fechaDesde, string fechaHasta)
        {
            try
            {
                DateTime desde, hasta;
                if (!DateTime.TryParse(fechaDesde, out desde) || !DateTime.TryParse(fechaHasta, out hasta))
                    return new HttpStatusCodeResult(400, "Fechas invalidas.");

                var facturas = FacturaFiscalMethod.GetForExport(tipo, desde, hasta);
                string tipoNombre = tipo == 1 ? "Compras" : "Ventas";

                using (var pck = new ExcelPackage())
                {
                    var ws = pck.Workbook.Worksheets.Add("Libro IVA " + tipoNombre);

                    // Headers
                    string[] headers = { "Fecha", "Tipo Comp.", "Punto Vta", "Numero", "CUIT", "Razon Social", "Cond. IVA", "Neto", "IVA", "Percepciones", "Otros Imp.", "Total", "CAE", "Observaciones" };
                    for (int i = 0; i < headers.Length; i++)
                    {
                        ws.Cells[1, i + 1].Value = headers[i];
                        ws.Cells[1, i + 1].Style.Font.Bold = true;
                        ws.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(68, 114, 196));
                        ws.Cells[1, i + 1].Style.Font.Color.SetColor(Color.White);
                    }

                    int row = 2;
                    decimal sumNeto = 0, sumIva = 0, sumPercepciones = 0, sumOtros = 0, sumTotal = 0;
                    foreach (var f in facturas)
                    {
                        ws.Cells[row, 1].Value = f.FechaEmision.ToString("dd/MM/yyyy");
                        ws.Cells[row, 2].Value = GetComprobanteNombre(f.TipoComprobante);
                        ws.Cells[row, 3].Value = f.PuntoVenta.ToString("00000");
                        ws.Cells[row, 4].Value = f.Numero.ToString("00000000");
                        ws.Cells[row, 5].Value = f.Cuit;
                        ws.Cells[row, 6].Value = tipo == 1 ? f.ProveedorRazonSocial : f.ClienteNombre;
                        ws.Cells[row, 7].Value = f.CondicionIva.ToString();
                        ws.Cells[row, 8].Value = f.Neto;
                        ws.Cells[row, 8].Style.Numberformat.Format = "#,##0.00";
                        ws.Cells[row, 9].Value = f.Iva;
                        ws.Cells[row, 9].Style.Numberformat.Format = "#,##0.00";
                        ws.Cells[row, 10].Value = f.Percepciones;
                        ws.Cells[row, 10].Style.Numberformat.Format = "#,##0.00";
                        ws.Cells[row, 11].Value = f.OtrosImpuestos;
                        ws.Cells[row, 11].Style.Numberformat.Format = "#,##0.00";
                        ws.Cells[row, 12].Value = f.Total;
                        ws.Cells[row, 12].Style.Numberformat.Format = "#,##0.00";
                        ws.Cells[row, 13].Value = f.CAE;
                        ws.Cells[row, 14].Value = f.Observaciones;

                        sumNeto += f.Neto;
                        sumIva += f.Iva;
                        sumPercepciones += f.Percepciones;
                        sumOtros += f.OtrosImpuestos;
                        sumTotal += f.Total;
                        row++;
                    }

                    // Totals row
                    ws.Cells[row, 7].Value = "TOTALES";
                    ws.Cells[row, 7].Style.Font.Bold = true;
                    ws.Cells[row, 8].Value = sumNeto;
                    ws.Cells[row, 8].Style.Numberformat.Format = "#,##0.00";
                    ws.Cells[row, 8].Style.Font.Bold = true;
                    ws.Cells[row, 9].Value = sumIva;
                    ws.Cells[row, 9].Style.Numberformat.Format = "#,##0.00";
                    ws.Cells[row, 9].Style.Font.Bold = true;
                    ws.Cells[row, 10].Value = sumPercepciones;
                    ws.Cells[row, 10].Style.Numberformat.Format = "#,##0.00";
                    ws.Cells[row, 10].Style.Font.Bold = true;
                    ws.Cells[row, 11].Value = sumOtros;
                    ws.Cells[row, 11].Style.Numberformat.Format = "#,##0.00";
                    ws.Cells[row, 11].Style.Font.Bold = true;
                    ws.Cells[row, 12].Value = sumTotal;
                    ws.Cells[row, 12].Style.Numberformat.Format = "#,##0.00";
                    ws.Cells[row, 12].Style.Font.Bold = true;

                    ws.Cells[ws.Dimension.Address].AutoFitColumns();

                    var bytes = pck.GetAsByteArray();
                    string fileName = $"LibroIVA_{tipoNombre}_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.xlsx";
                    return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en FacturaFiscalController.ExportExcel: {ex.Message}", 1);
                return new HttpStatusCodeResult(500, "Error al exportar a Excel.");
            }
        }

        [HttpGet]
        public ActionResult ExportARCA(int tipo, string fechaDesde, string fechaHasta)
        {
            try
            {
                DateTime desde, hasta;
                if (!DateTime.TryParse(fechaDesde, out desde) || !DateTime.TryParse(fechaHasta, out hasta))
                    return new HttpStatusCodeResult(400, "Fechas invalidas.");

                var facturas = FacturaFiscalMethod.GetForExport(tipo, desde, hasta);
                string tipoNombre = tipo == 1 ? "Compras" : "Ventas";

                var lines = new List<string>();
                foreach (var f in facturas)
                {
                    string codigoAlicuota = GetCodigoAlicuotaARCA(f.AlicuotaIva);
                    string line = string.Join("|",
                        f.FechaEmision.ToString("yyyyMMdd"),
                        ((int)f.TipoComprobante).ToString("000"),
                        f.PuntoVenta.ToString("00000"),
                        f.Numero.ToString("00000000"),
                        f.Cuit.Replace("-", ""),
                        f.Neto.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                        f.Iva.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                        f.Total.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture),
                        codigoAlicuota
                    );
                    lines.Add(line);
                }

                var content = string.Join("\r\n", lines);
                var bytes = System.Text.Encoding.UTF8.GetBytes(content);
                string fileName = $"ARCA_{tipoNombre}_{desde:yyyyMMdd}_{hasta:yyyyMMdd}.txt";
                return File(bytes, "text/plain", fileName);
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en FacturaFiscalController.ExportARCA: {ex.Message}", 1);
                return new HttpStatusCodeResult(500, "Error al exportar formato ARCA.");
            }
        }

        [HttpPost]
        public JsonResult UploadAdjunto(HttpPostedFileBase archivo)
        {
            try
            {
                if (archivo == null || archivo.ContentLength == 0)
                    return Json(new { success = false, message = "No se selecciono ningun archivo." });

                if (archivo.ContentLength > 10 * 1024 * 1024)
                    return Json(new { success = false, message = "El archivo excede el limite de 10MB." });

                string extension = Path.GetExtension(archivo.FileName).ToLower();
                string[] allowedExtensions = { ".pdf", ".jpg", ".jpeg", ".png" };
                if (!allowedExtensions.Contains(extension))
                    return Json(new { success = false, message = "Extension no permitida. Solo se permiten: PDF, JPG, JPEG, PNG." });

                string uploadsDir = Server.MapPath("~/Uploads/FacturasFiscales/");
                if (!Directory.Exists(uploadsDir))
                    Directory.CreateDirectory(uploadsDir);

                string uniqueName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(uploadsDir, uniqueName);
                archivo.SaveAs(filePath);

                string relativePath = "/Uploads/FacturasFiscales/" + uniqueName;
                return Json(new { success = true, filePath = relativePath, fileName = archivo.FileName });
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error en FacturaFiscalController.UploadAdjunto: {ex.Message}", 1);
                return Json(new { success = false, message = "Error al subir el archivo." });
            }
        }

        private string GetComprobanteNombre(eTipoComprobante tipo)
        {
            switch (tipo)
            {
                case eTipoComprobante.FacturaA: return "Factura A";
                case eTipoComprobante.FacturaB: return "Factura B";
                case eTipoComprobante.FacturaC: return "Factura C";
                case eTipoComprobante.NotaCreditoA: return "NC A";
                case eTipoComprobante.NotaCreditoB: return "NC B";
                case eTipoComprobante.NotaCreditoC: return "NC C";
                default: return tipo.ToString();
            }
        }

        private string GetCodigoAlicuotaARCA(eAlicuotaIva alicuota)
        {
            switch (alicuota)
            {
                case eAlicuotaIva.Cero: return "0003";
                case eAlicuotaIva.DosYMedio: return "0009";
                case eAlicuotaIva.Cinco: return "0008";
                case eAlicuotaIva.DiezYMedio: return "0004";
                case eAlicuotaIva.Veintiuno: return "0005";
                case eAlicuotaIva.Veintisiete: return "0006";
                default: return "0005";
            }
        }
    }
}
