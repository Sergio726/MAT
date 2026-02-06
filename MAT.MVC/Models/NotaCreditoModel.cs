using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.Entities;
using MAT.Services;
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
                _item.NotaCreditoID = row["NotaCreditoID"].ToString();
                _item.Vendedor = row["Vendedor"].ToString();
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

            while (_reader.Read())
            {
                _nota.NotaID = NotaID;
                _nota.Dias = Convert.ToInt32(_reader["Dias"]);
                _nota.Fecha = Convert.ToDateTime(_reader["Fecha"]);
                _nota.MontoNota = Convert.ToDecimal(_reader["MontoNota"]);
                _nota.MontoRetencion = Convert.ToDecimal(_reader["MontoRetencion"]);
                _nota.NroNota = _reader["NroNota"].ToString();
                _nota.PorcentajeRetencion = Convert.ToDecimal(_reader["PorcentajeRetencion"]);
                _nota.Vendedor = _reader["Vendedor"].ToString();
                _nota.Cliente = _reader["Cliente"] != DBNull.Value && _reader["Cliente"] != null ? _reader["Cliente"].ToString() : null;
                _nota.Detalle = _reader["Detalle"].ToString();
                if (_reader["MontoDevolucion"] != DBNull.Value && _reader["MontoDevolucion"] != null)
                    _nota.MontoDevolucion = Convert.ToDecimal(_reader["MontoDevolucion"]);
                if (_reader["ViajeFecha"] != DBNull.Value && _reader["ViajeFecha"] != null)
                    _nota.ViajeFecha = Convert.ToDateTime(_reader["ViajeFecha"]);
                if (_reader["ViajeNombre"] != DBNull.Value && _reader["ViajeNombre"] != null)
                    _nota.ViajeNombre = _reader["ViajeNombre"].ToString();
            }

            return _nota;
        }
    }
}