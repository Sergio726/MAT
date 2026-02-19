using MAT.Utilities;
using MAT.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MAT.MVC.Models
{
    public class FacturaFiscalStandard
    {
        public Guid FacturaFiscalID { get; set; }
        public eTipoFacturaFiscal Tipo { get; set; }
        public eTipoComprobante TipoComprobante { get; set; }
        public int PuntoVenta { get; set; }
        public long Numero { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public Guid? ProveedorID { get; set; }
        public Guid? ClienteID { get; set; }
        public string Cuit { get; set; }
        public eCondicionIVA CondicionIva { get; set; }
        public decimal Neto { get; set; }
        public decimal Iva { get; set; }
        public decimal OtrosImpuestos { get; set; }
        public decimal Total { get; set; }
        public int Moneda { get; set; }
        public string CAE { get; set; }
        public string ArchivoAdjunto { get; set; }
        public eEstadoFacturaFiscal Estado { get; set; }
        public eAlicuotaIva AlicuotaIva { get; set; }
        public decimal Percepciones { get; set; }
        public string CondicionVenta { get; set; }
        public string Observaciones { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // JOIN fields
        public string ProveedorRazonSocial { get; set; }
        public string ClienteNombre { get; set; }
        public string MonedaDescripcion { get; set; }

        // Computed
        public string NumeroCompleto
        {
            get
            {
                return PuntoVenta.ToString("00000") + "-" + Numero.ToString("00000000");
            }
        }
    }

    public class FacturaFiscalTotales
    {
        public decimal TotalNeto { get; set; }
        public decimal TotalIva { get; set; }
        public decimal TotalOtrosImpuestos { get; set; }
        public decimal TotalGeneral { get; set; }
        public int CantidadRegistros { get; set; }
    }

    public class FacturaFiscalMethod
    {
        public static void Insert(FacturaFiscalStandard factura)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@FacturaFiscalID", SqlDbType.UniqueIdentifier, 0, factura.FacturaFiscalID),
                DBHelper.MakeParam("@Tipo", SqlDbType.Int, 0, (int)factura.Tipo),
                DBHelper.MakeParam("@TipoComprobante", SqlDbType.Int, 0, (int)factura.TipoComprobante),
                DBHelper.MakeParam("@PuntoVenta", SqlDbType.Int, 0, factura.PuntoVenta),
                DBHelper.MakeParam("@Numero", SqlDbType.BigInt, 0, factura.Numero),
                DBHelper.MakeParam("@FechaEmision", SqlDbType.Date, 0, factura.FechaEmision),
                DBHelper.MakeParam("@FechaVencimiento", SqlDbType.Date, 0, factura.FechaVencimiento.HasValue ? (object)factura.FechaVencimiento.Value : DBNull.Value),
                DBHelper.MakeParam("@ProveedorID", SqlDbType.UniqueIdentifier, 0, factura.ProveedorID.HasValue ? (object)factura.ProveedorID.Value : DBNull.Value),
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, factura.ClienteID.HasValue ? (object)factura.ClienteID.Value : DBNull.Value),
                DBHelper.MakeParam("@Cuit", SqlDbType.VarChar, 13, factura.Cuit),
                DBHelper.MakeParam("@CondicionIva", SqlDbType.Int, 0, (int)factura.CondicionIva),
                DBHelper.MakeParam("@Neto", SqlDbType.Decimal, 0, factura.Neto),
                DBHelper.MakeParam("@Iva", SqlDbType.Decimal, 0, factura.Iva),
                DBHelper.MakeParam("@OtrosImpuestos", SqlDbType.Decimal, 0, factura.OtrosImpuestos),
                DBHelper.MakeParam("@Total", SqlDbType.Decimal, 0, factura.Total),
                DBHelper.MakeParam("@Moneda", SqlDbType.Int, 0, factura.Moneda),
                DBHelper.MakeParam("@CAE", SqlDbType.VarChar, 20, string.IsNullOrEmpty(factura.CAE) ? (object)DBNull.Value : factura.CAE),
                DBHelper.MakeParam("@ArchivoAdjunto", SqlDbType.VarChar, 500, string.IsNullOrEmpty(factura.ArchivoAdjunto) ? (object)DBNull.Value : factura.ArchivoAdjunto),
                DBHelper.MakeParam("@Estado", SqlDbType.Int, 0, (int)factura.Estado),
                DBHelper.MakeParam("@AlicuotaIva", SqlDbType.Int, 0, (int)factura.AlicuotaIva),
                DBHelper.MakeParam("@Percepciones", SqlDbType.Decimal, 0, factura.Percepciones),
                DBHelper.MakeParam("@CondicionVenta", SqlDbType.VarChar, 100, string.IsNullOrEmpty(factura.CondicionVenta) ? (object)DBNull.Value : factura.CondicionVenta),
                DBHelper.MakeParam("@Observaciones", SqlDbType.VarChar, 500, string.IsNullOrEmpty(factura.Observaciones) ? (object)DBNull.Value : factura.Observaciones)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_FacturaFiscal_Insert", dbParams);
        }

        public static void Update(FacturaFiscalStandard factura)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@FacturaFiscalID", SqlDbType.UniqueIdentifier, 0, factura.FacturaFiscalID),
                DBHelper.MakeParam("@Tipo", SqlDbType.Int, 0, (int)factura.Tipo),
                DBHelper.MakeParam("@TipoComprobante", SqlDbType.Int, 0, (int)factura.TipoComprobante),
                DBHelper.MakeParam("@PuntoVenta", SqlDbType.Int, 0, factura.PuntoVenta),
                DBHelper.MakeParam("@Numero", SqlDbType.BigInt, 0, factura.Numero),
                DBHelper.MakeParam("@FechaEmision", SqlDbType.Date, 0, factura.FechaEmision),
                DBHelper.MakeParam("@FechaVencimiento", SqlDbType.Date, 0, factura.FechaVencimiento.HasValue ? (object)factura.FechaVencimiento.Value : DBNull.Value),
                DBHelper.MakeParam("@ProveedorID", SqlDbType.UniqueIdentifier, 0, factura.ProveedorID.HasValue ? (object)factura.ProveedorID.Value : DBNull.Value),
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, factura.ClienteID.HasValue ? (object)factura.ClienteID.Value : DBNull.Value),
                DBHelper.MakeParam("@Cuit", SqlDbType.VarChar, 13, factura.Cuit),
                DBHelper.MakeParam("@CondicionIva", SqlDbType.Int, 0, (int)factura.CondicionIva),
                DBHelper.MakeParam("@Neto", SqlDbType.Decimal, 0, factura.Neto),
                DBHelper.MakeParam("@Iva", SqlDbType.Decimal, 0, factura.Iva),
                DBHelper.MakeParam("@OtrosImpuestos", SqlDbType.Decimal, 0, factura.OtrosImpuestos),
                DBHelper.MakeParam("@Total", SqlDbType.Decimal, 0, factura.Total),
                DBHelper.MakeParam("@Moneda", SqlDbType.Int, 0, factura.Moneda),
                DBHelper.MakeParam("@CAE", SqlDbType.VarChar, 20, string.IsNullOrEmpty(factura.CAE) ? (object)DBNull.Value : factura.CAE),
                DBHelper.MakeParam("@ArchivoAdjunto", SqlDbType.VarChar, 500, string.IsNullOrEmpty(factura.ArchivoAdjunto) ? (object)DBNull.Value : factura.ArchivoAdjunto),
                DBHelper.MakeParam("@Estado", SqlDbType.Int, 0, (int)factura.Estado),
                DBHelper.MakeParam("@AlicuotaIva", SqlDbType.Int, 0, (int)factura.AlicuotaIva),
                DBHelper.MakeParam("@Percepciones", SqlDbType.Decimal, 0, factura.Percepciones),
                DBHelper.MakeParam("@CondicionVenta", SqlDbType.VarChar, 100, string.IsNullOrEmpty(factura.CondicionVenta) ? (object)DBNull.Value : factura.CondicionVenta),
                DBHelper.MakeParam("@Observaciones", SqlDbType.VarChar, 500, string.IsNullOrEmpty(factura.Observaciones) ? (object)DBNull.Value : factura.Observaciones)
            };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_FacturaFiscal_Update", dbParams);
        }

        // Helper to read a FacturaFiscalStandard from a SqlDataReader
        private static FacturaFiscalStandard ReadFromReader(SqlDataReader _reader)
        {
            var f = new FacturaFiscalStandard();
            f.FacturaFiscalID = new Guid(_reader["FacturaFiscalID"].ToString());
            f.Tipo = (eTipoFacturaFiscal)Convert.ToInt32(_reader["Tipo"]);
            f.TipoComprobante = (eTipoComprobante)Convert.ToInt32(_reader["TipoComprobante"]);
            f.PuntoVenta = Convert.ToInt32(_reader["PuntoVenta"]);
            f.Numero = Convert.ToInt64(_reader["Numero"]);
            f.FechaEmision = Convert.ToDateTime(_reader["FechaEmision"]);
            if (_reader["FechaVencimiento"] != DBNull.Value)
                f.FechaVencimiento = Convert.ToDateTime(_reader["FechaVencimiento"]);
            if (_reader["ProveedorID"] != DBNull.Value)
                f.ProveedorID = new Guid(_reader["ProveedorID"].ToString());
            if (_reader["ClienteID"] != DBNull.Value)
                f.ClienteID = new Guid(_reader["ClienteID"].ToString());
            f.Cuit = _reader["Cuit"]?.ToString() ?? string.Empty;
            f.CondicionIva = (eCondicionIVA)Convert.ToInt32(_reader["CondicionIva"]);
            f.Neto = Convert.ToDecimal(_reader["Neto"]);
            f.Iva = Convert.ToDecimal(_reader["Iva"]);
            f.OtrosImpuestos = Convert.ToDecimal(_reader["OtrosImpuestos"]);
            f.Total = Convert.ToDecimal(_reader["Total"]);
            f.Moneda = Convert.ToInt32(_reader["Moneda"]);
            f.CAE = _reader["CAE"]?.ToString() ?? string.Empty;
            f.ArchivoAdjunto = _reader["ArchivoAdjunto"]?.ToString() ?? string.Empty;
            f.Estado = (eEstadoFacturaFiscal)Convert.ToInt32(_reader["Estado"]);
            f.AlicuotaIva = (eAlicuotaIva)Convert.ToInt32(_reader["AlicuotaIva"]);
            f.Percepciones = Convert.ToDecimal(_reader["Percepciones"]);
            f.CondicionVenta = _reader["CondicionVenta"]?.ToString() ?? string.Empty;
            f.Observaciones = _reader["Observaciones"]?.ToString() ?? string.Empty;
            f.CreatedAt = Convert.ToDateTime(_reader["CreatedAt"]);
            if (_reader["UpdatedAt"] != DBNull.Value)
                f.UpdatedAt = Convert.ToDateTime(_reader["UpdatedAt"]);
            // JOIN fields
            f.ProveedorRazonSocial = _reader["ProveedorRazonSocial"]?.ToString() ?? string.Empty;
            f.ClienteNombre = _reader["ClienteNombre"]?.ToString() ?? string.Empty;
            f.MonedaDescripcion = _reader["MonedaDescripcion"]?.ToString() ?? string.Empty;
            return f;
        }

        public static List<FacturaFiscalStandard> GetAll(int? tipo = null, int? tipoComprobante = null, Guid? proveedorId = null, Guid? clienteId = null, string cuit = null, int? estado = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null, string busqueda = null)
        {
            List<FacturaFiscalStandard> lista = new List<FacturaFiscalStandard>();
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@Tipo", SqlDbType.Int, 0, tipo.HasValue ? (object)tipo.Value : DBNull.Value),
                DBHelper.MakeParam("@TipoComprobante", SqlDbType.Int, 0, tipoComprobante.HasValue ? (object)tipoComprobante.Value : DBNull.Value),
                DBHelper.MakeParam("@ProveedorID", SqlDbType.UniqueIdentifier, 0, proveedorId.HasValue ? (object)proveedorId.Value : DBNull.Value),
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, clienteId.HasValue ? (object)clienteId.Value : DBNull.Value),
                DBHelper.MakeParam("@Cuit", SqlDbType.VarChar, 13, string.IsNullOrWhiteSpace(cuit) ? (object)DBNull.Value : cuit),
                DBHelper.MakeParam("@Estado", SqlDbType.Int, 0, estado.HasValue ? (object)estado.Value : DBNull.Value),
                DBHelper.MakeParam("@FechaDesde", SqlDbType.Date, 0, fechaDesde.HasValue ? (object)fechaDesde.Value : DBNull.Value),
                DBHelper.MakeParam("@FechaHasta", SqlDbType.Date, 0, fechaHasta.HasValue ? (object)fechaHasta.Value : DBNull.Value),
                DBHelper.MakeParam("@Busqueda", SqlDbType.VarChar, 100, string.IsNullOrWhiteSpace(busqueda) ? (object)DBNull.Value : busqueda)
            };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_FacturaFiscal_GetAll", dbParams))
            {
                while (_reader.Read())
                {
                    lista.Add(ReadFromReader(_reader));
                }
            }
            return lista;
        }

        public static FacturaFiscalStandard GetById(Guid facturaFiscalId)
        {
            FacturaFiscalStandard factura = null;
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@FacturaFiscalID", SqlDbType.UniqueIdentifier, 0, facturaFiscalId)
            };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_FacturaFiscal_GetById", dbParams))
            {
                if (_reader.Read())
                {
                    factura = ReadFromReader(_reader);
                }
            }
            return factura;
        }

        public static bool Anular(Guid facturaFiscalId)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@FacturaFiscalID", SqlDbType.UniqueIdentifier, 0, facturaFiscalId)
            };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_FacturaFiscal_Anular", dbParams))
            {
                if (_reader.Read())
                {
                    return Convert.ToInt32(_reader["RowsAffected"]) > 0;
                }
            }
            return false;
        }

        public static bool CheckDuplicate(string cuit, int tipoComprobante, int puntoVenta, long numero, Guid? excludeId = null)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@Cuit", SqlDbType.VarChar, 13, cuit),
                DBHelper.MakeParam("@TipoComprobante", SqlDbType.Int, 0, tipoComprobante),
                DBHelper.MakeParam("@PuntoVenta", SqlDbType.Int, 0, puntoVenta),
                DBHelper.MakeParam("@Numero", SqlDbType.BigInt, 0, numero),
                DBHelper.MakeParam("@ExcludeID", SqlDbType.UniqueIdentifier, 0, excludeId.HasValue ? (object)excludeId.Value : DBNull.Value)
            };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_FacturaFiscal_CheckDuplicate", dbParams))
            {
                if (_reader.Read())
                {
                    return Convert.ToInt32(_reader["Duplicados"]) > 0;
                }
            }
            return false;
        }

        public static List<FacturaFiscalStandard> GetForExport(int tipo, DateTime fechaDesde, DateTime fechaHasta)
        {
            List<FacturaFiscalStandard> lista = new List<FacturaFiscalStandard>();
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@Tipo", SqlDbType.Int, 0, tipo),
                DBHelper.MakeParam("@FechaDesde", SqlDbType.Date, 0, fechaDesde),
                DBHelper.MakeParam("@FechaHasta", SqlDbType.Date, 0, fechaHasta)
            };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_FacturaFiscal_GetForExport", dbParams))
            {
                while (_reader.Read())
                {
                    lista.Add(ReadFromReader(_reader));
                }
            }
            return lista;
        }

        public static FacturaFiscalTotales GetTotales(int? tipo = null, int? tipoComprobante = null, Guid? proveedorId = null, Guid? clienteId = null, string cuit = null, int? estado = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null, string busqueda = null)
        {
            var totales = new FacturaFiscalTotales();
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@Tipo", SqlDbType.Int, 0, tipo.HasValue ? (object)tipo.Value : DBNull.Value),
                DBHelper.MakeParam("@TipoComprobante", SqlDbType.Int, 0, tipoComprobante.HasValue ? (object)tipoComprobante.Value : DBNull.Value),
                DBHelper.MakeParam("@ProveedorID", SqlDbType.UniqueIdentifier, 0, proveedorId.HasValue ? (object)proveedorId.Value : DBNull.Value),
                DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, clienteId.HasValue ? (object)clienteId.Value : DBNull.Value),
                DBHelper.MakeParam("@Cuit", SqlDbType.VarChar, 13, string.IsNullOrWhiteSpace(cuit) ? (object)DBNull.Value : cuit),
                DBHelper.MakeParam("@Estado", SqlDbType.Int, 0, estado.HasValue ? (object)estado.Value : DBNull.Value),
                DBHelper.MakeParam("@FechaDesde", SqlDbType.Date, 0, fechaDesde.HasValue ? (object)fechaDesde.Value : DBNull.Value),
                DBHelper.MakeParam("@FechaHasta", SqlDbType.Date, 0, fechaHasta.HasValue ? (object)fechaHasta.Value : DBNull.Value),
                DBHelper.MakeParam("@Busqueda", SqlDbType.VarChar, 100, string.IsNullOrWhiteSpace(busqueda) ? (object)DBNull.Value : busqueda)
            };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_FacturaFiscal_GetTotales", dbParams))
            {
                if (_reader.Read())
                {
                    totales.TotalNeto = _reader["TotalNeto"] != DBNull.Value ? Convert.ToDecimal(_reader["TotalNeto"]) : 0;
                    totales.TotalIva = _reader["TotalIva"] != DBNull.Value ? Convert.ToDecimal(_reader["TotalIva"]) : 0;
                    totales.TotalOtrosImpuestos = _reader["TotalOtrosImpuestos"] != DBNull.Value ? Convert.ToDecimal(_reader["TotalOtrosImpuestos"]) : 0;
                    totales.TotalGeneral = _reader["TotalGeneral"] != DBNull.Value ? Convert.ToDecimal(_reader["TotalGeneral"]) : 0;
                    totales.CantidadRegistros = _reader["CantidadRegistros"] != DBNull.Value ? Convert.ToInt32(_reader["CantidadRegistros"]) : 0;
                }
            }
            return totales;
        }
    }
}
