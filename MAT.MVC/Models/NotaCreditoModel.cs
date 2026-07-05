using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.Entities;
using MAT.Enums;
using MAT.MVC.Common;
using System.Data.SqlClient;
using MAT.Utilities;
using System.Data;
namespace MAT.MVC.Models
{
    public class NotaCreditoModel
    {
        public Guid NotaID { get; set; }
        public decimal PorcentajeRetencion { get; set; }
        public decimal MontoRetencion { get; set; }
        public DateTime Fecha { get; set; }
        public int Dias { get; set; }
        public Guid ClienteID { get; set; }
        public Guid VendedorID { get; set; }
        public string NroNota { get; set; }
        public decimal MontoNota { get; set; }
        public decimal PorcentajeDevolucion { get; set; }
        public decimal MontoDevolucion { get; set; }
        public string Vendedor { get; set; }
        public string Cliente { get; set; }
        public string Detalle { get; set; }
        /// <summary>Fecha de salida del viaje asociado a la factura/nota.</summary>
        public DateTime? ViajeFecha { get; set; }
        /// <summary>Nombre/descripción del viaje asociado.</summary>
        public string ViajeNombre { get; set; }
    }

    public class MovimientoNotaCredito 
    {
        public string NotaCreditoID { get; set; }
        public double Monto { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Vendedor { get; set; }
        public Guid? PagoID { get; set; }
        public Guid? FacturaID { get; set; }
        public string NroRecibo { get; set; }
        public string NroFactura { get; set; }
    }

    /// <summary>Registro de aplicación de crédito de una nota a un pago/factura.</summary>
    public class NotaCreditoAplicacionModel
    {
        public int Id { get; set; }
        public Guid NotaCreditoID { get; set; }
        public Guid PagoID { get; set; }
        public Guid FacturaID { get; set; }
        public decimal MontoAplicado { get; set; }
        public DateTime Fecha { get; set; }
        public string NroRecibo { get; set; }
        public string NroFactura { get; set; }
    }

    public static class NotaCreditoMethod
    {
        public static NotaCreditoModel CalcularNota(decimal dMonto, DateTime dDateIn)
        {
            NotaCreditoModel _model = new NotaCreditoModel();
            int iDifDate = 0;
            decimal dPercReten = 0;

            // Difference in days, hours, and minutes.
            TimeSpan ts = dDateIn - DateTime.Now;

            // Difference in days.
            iDifDate = ts.Days;

            if (iDifDate >= 30)
            {
                dPercReten = 0.10M;
            }
            else if (iDifDate >= 10)
            {
                dPercReten = 0.4M;
            }
            else if (iDifDate < 10)
            {
                dPercReten = 1.0M;
            }

            _model.MontoNota = dMonto - (dMonto * dPercReten);
            _model.MontoRetencion = dMonto * dPercReten;
            _model.PorcentajeRetencion = dPercReten;
            _model.Dias = iDifDate;

            return _model;
        }

        /// <summary>
        /// Obtiene el próximo número de nota de crédito (formato NC-YYYY-NNNN).
        /// </summary>
        public static string GetNextNroNota()
        {
            object result = DBHelper.ExecuteScalar("dbo.usp_MAT_Nota_GetNextNroNota", new SqlParameter[0]);
            return result != null && result != DBNull.Value ? result.ToString().Trim() : string.Empty;
        }
                
        public static void InsertNewNota(NotaCreditoModel _model, Guid FacturaID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {
                        DBHelper.MakeParam("@PorcentajeRetencion", SqlDbType.Float, 0, _model.PorcentajeRetencion),
                        DBHelper.MakeParam("@MontoRetencion", SqlDbType.Float, 0, _model.MontoRetencion),
                        DBHelper.MakeParam("@Dias", SqlDbType.Int, 0, _model.Dias),
                        DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, _model.ClienteID),
                        DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0, MATContext.CurrentVendedor.VendedorId),
                        DBHelper.MakeParam("@NroNota", SqlDbType.VarChar, 0, _model.NroNota),
                        DBHelper.MakeParam("@MontoNota", SqlDbType.Float, 0, _model.MontoNota),
                        DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, FacturaID),
                        DBHelper.MakeParam("@Detalle", SqlDbType.VarChar, 1000, _model.Detalle),
                        DBHelper.MakeParam("@MontoDevolucion", SqlDbType.Float, 0, _model.MontoDevolucion)
                    };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Nota_IsertNewNota", dbParams);
        }

        public static List<MovimientoNotaCredito> GetMovimientoNotaCreditoByClienteID(Guid ClienteID, out string sCliente)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, ClienteID)
                        
                    };
            DataSet ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_CreditoCliente_GetMovimientoNotaCreditoByClienteID", dbParams);

            List<MovimientoNotaCredito> _list = new List<MovimientoNotaCredito>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                MovimientoNotaCredito _item = new MovimientoNotaCredito();
                _item.Descripcion = row["Descripcion"].ToString();
                _item.Fecha = Convert.ToDateTime(row["Fecha"]);
                _item.Monto = Convert.ToDouble(row["Monto"]);
                _item.NotaCreditoID = row["NotaCreditoID"] != DBNull.Value && row["NotaCreditoID"] != null ? row["NotaCreditoID"].ToString() : null;
                _item.Vendedor = row["Vendedor"].ToString();
                if (row.Table.Columns.Contains("PagoID") && row["PagoID"] != DBNull.Value && row["PagoID"] != null)
                    _item.PagoID = (Guid)row["PagoID"];
                if (row.Table.Columns.Contains("FacturaID") && row["FacturaID"] != DBNull.Value && row["FacturaID"] != null)
                    _item.FacturaID = (Guid)row["FacturaID"];
                if (row.Table.Columns.Contains("NroRecibo") && row["NroRecibo"] != DBNull.Value && row["NroRecibo"] != null)
                    _item.NroRecibo = row["NroRecibo"].ToString();
                if (row.Table.Columns.Contains("NroFactura") && row["NroFactura"] != DBNull.Value && row["NroFactura"] != null)
                    _item.NroFactura = row["NroFactura"].ToString();
                _list.Add(_item);
            }

            string sReturnCliente = "";
            foreach (DataRow row in ds.Tables[1].Rows)
            {
                sReturnCliente = row["Cliente"].ToString();  
            }
            sCliente = sReturnCliente;

            return _list;
        }

        public static NotaCreditoModel GetNotaByID(Guid NotaID)
        {
            NotaCreditoModel _nota = new NotaCreditoModel();
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@NotaID", SqlDbType.UniqueIdentifier, 0, NotaID)
                        
                    };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Nota_GetNotaByID", dbParams);

            _nota.NotaID = NotaID;
            while (_reader.Read())
            {
                _nota.Dias = _reader["Dias"] != DBNull.Value && _reader["Dias"] != null ? Convert.ToInt32(_reader["Dias"]) : 0;
                _nota.Fecha = _reader["Fecha"] != DBNull.Value && _reader["Fecha"] != null ? Convert.ToDateTime(_reader["Fecha"]) : default(DateTime);
                _nota.MontoNota = _reader["MontoNota"] != DBNull.Value && _reader["MontoNota"] != null ? Convert.ToDecimal(_reader["MontoNota"]) : 0;
                _nota.MontoRetencion = _reader["MontoRetencion"] != DBNull.Value && _reader["MontoRetencion"] != null ? Convert.ToDecimal(_reader["MontoRetencion"]) : 0;
                _nota.NroNota = _reader["NroNota"] != DBNull.Value && _reader["NroNota"] != null ? _reader["NroNota"].ToString() : null;
                _nota.PorcentajeRetencion = _reader["PorcentajeRetencion"] != DBNull.Value && _reader["PorcentajeRetencion"] != null ? Convert.ToDecimal(_reader["PorcentajeRetencion"]) : 0;
                _nota.Vendedor = _reader["Vendedor"] != DBNull.Value && _reader["Vendedor"] != null ? _reader["Vendedor"].ToString() : null;
                _nota.Cliente = _reader["Cliente"] != DBNull.Value && _reader["Cliente"] != null ? _reader["Cliente"].ToString() : null;
                _nota.Detalle = _reader["Detalle"] != DBNull.Value && _reader["Detalle"] != null ? _reader["Detalle"].ToString() : null;
                if (_reader["MontoDevolucion"] != DBNull.Value && _reader["MontoDevolucion"] != null)
                    _nota.MontoDevolucion = Convert.ToDecimal(_reader["MontoDevolucion"]);
                if (_reader["ViajeFecha"] != DBNull.Value && _reader["ViajeFecha"] != null)
                    _nota.ViajeFecha = Convert.ToDateTime(_reader["ViajeFecha"]);
                if (_reader["ViajeNombre"] != DBNull.Value && _reader["ViajeNombre"] != null)
                    _nota.ViajeNombre = _reader["ViajeNombre"].ToString();
            }

            return _nota;
        }

        /// <summary>Obtiene las aplicaciones (pagos/facturas) donde se usó el crédito de una nota.</summary>
        public static List<NotaCreditoAplicacionModel> GetAplicacionesByNotaID(Guid NotaID)
        {
            var list = new List<NotaCreditoAplicacionModel>();
            SqlParameter[] dbParams = new SqlParameter[] { DBHelper.MakeParam("@NotaID", SqlDbType.UniqueIdentifier, 0, NotaID) };
            using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_NotaCreditoAplicacion_GetByNotaID", dbParams))
            {
                while (reader.Read())
                {
                    list.Add(new NotaCreditoAplicacionModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        NotaCreditoID = (Guid)reader["NotaCreditoID"],
                        PagoID = (Guid)reader["PagoID"],
                        FacturaID = (Guid)reader["FacturaID"],
                        MontoAplicado = Convert.ToDecimal(reader["MontoAplicado"]),
                        Fecha = Convert.ToDateTime(reader["Fecha"]),
                        NroRecibo = reader["NroRecibo"] != DBNull.Value && reader["NroRecibo"] != null ? reader["NroRecibo"].ToString() : null,
                        NroFactura = reader["NroFactura"] != DBNull.Value && reader["NroFactura"] != null ? reader["NroFactura"].ToString() : null
                    });
                }
            }
            return list;
        }

        /// <summary>Saldo disponible de una nota (MontoNota - suma de montos ya aplicados).</summary>
        public static decimal GetSaldoDisponiblePorNota(Guid NotaID)
        {
            var aplicaciones = GetAplicacionesByNotaID(NotaID);
            var nota = GetNotaByID(NotaID);
            var aplicado = aplicaciones.Sum(a => a.MontoAplicado);
            return nota.MontoNota - aplicado;
        }
    }
}