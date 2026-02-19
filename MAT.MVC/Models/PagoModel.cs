using MAT.MVC.Common;
using MAT.Services;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class PagoModel
    {
        public string PagoID { get; set; }
        public DateTime FechaPago { get; set; }
        public string sFechaPago { get; set; }
        public decimal Monto { get; set; }
        public string NroRecibo { get; set; }
        public string TipoPagoDescripcion { get; set; }
        public int TipoPago { get; set; }
        public string TransaccionID { get; set; }
        public string Cliente { get; set; }
        public string Vendedor { get; set; }
        public string Moneda { get; set; }
        public string MonedaPaquete { get; set; }
        /// <summary>True cuando el pago fue cancelado por una nota de crédito (MontoRecibido = 0).</summary>
        public bool CanceladoPorNotaCredito { get; set; }
    }

    public class PagoDetalle {
        public decimal MontoRecibido { get; set; }
        public string MonedaRecibida { get; set; }
        public decimal MontoEquivalente { get; set; }
        public string MonedaEquivalente { get; set; }
        public decimal Cotizacion { get; set; }
        public string Fecha { get; set; }
        /// <summary>True cuando el pago fue cancelado por una nota de crédito (MontoRecibido = 0).</summary>
        public bool CanceladoPorNotaCredito { get; set; }
    }

    public class PagoMethod
    {
        public static DataSet NuevoPago(decimal Monto, string ClienteID, string NroRecibo, string TransaccionId, int TipoPago, string FacturaID, 
                                        string NroFactura,decimal? MontoRecibido, int MontoRecibidoMonedaTipo, decimal? MontoEquivalente,int? MontoEquivalenteMonedaTipo, 
                                        decimal? MontoEquivalenteCotizacion)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@Monto", SqlDbType.Money, 0, Monto),
                        DBHelper.MakeParam("@ClienteID", SqlDbType.VarChar, 0, ClienteID),
                        DBHelper.MakeParam("@NroRecibo", SqlDbType.VarChar, 0, NroRecibo),
                        DBHelper.MakeParam("@TransaccionId", SqlDbType.VarChar, 0, TransaccionId),
                        DBHelper.MakeParam("@TipoPago", SqlDbType.Int, 0, TipoPago),
                        DBHelper.MakeParam("@VendedorId", SqlDbType.VarChar, 0, MATContext.CurrentVendedor.VendedorId.ToString()),
                        DBHelper.MakeParam("@FacturaID", SqlDbType.VarChar, 0, FacturaID),
                        DBHelper.MakeParam("@NroFactura", SqlDbType.VarChar, 0, NroFactura),
                        //DBHelper.MakeParam("@MontoRecibido", SqlDbType.Float, 0, MontoRecibido),
                        DBHelper.MakeParam("@MontoRecibidoMonedaTipo", SqlDbType.Int, 0, MontoRecibidoMonedaTipo),
                        DBHelper.MakeParam("@MontoEquivalente", SqlDbType.Money, 0, MontoEquivalente),
                        DBHelper.MakeParam("@MontoEquivalenteMonedaTipo", SqlDbType.Int, 0, MontoEquivalenteMonedaTipo),
                        DBHelper.MakeParam("@MontoEquivalenteCotizacion", SqlDbType.Money, 0, MontoEquivalenteCotizacion)
                       
                    };
            DataSet ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_RegistroPago_NuevoPago", dbParams);
            return ds;
        }

        public static List<PagoModel> GetPagosByFacturaID(Guid FacturaID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, FacturaID)
                    };
            DataSet ds = DBHelper.ExecuteDataSet("usp_MAT_Pago_GetPagosByFacturaID", dbParams);
            List<PagoModel> lPago = new List<PagoModel>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PagoModel item = new PagoModel();
                item.PagoID = dr["PagoID"].ToString(); ;
                item.FechaPago = Convert.ToDateTime(dr["FechaPago"]);
                item.NroRecibo = dr["NroRecibo"].ToString();
                item.Monto = Convert.ToDecimal(dr["Monto"]);
                item.TipoPagoDescripcion = dr["TipoPagoDescripcion"].ToString();
                item.TipoPago = Convert.ToInt32(dr["TipoPago"]);
                item.TransaccionID = dr["TransaccionID"].ToString();
                item.Vendedor = dr["Vendedor"].ToString();
                item.Moneda = dr["Moneda"].ToString();
                item.MonedaPaquete = dr["MonedaPaquete"].ToString();
                lPago.Add(item);
            }
                        

            return lPago;
        }

        public static List<PagoModel> GetPagosByFecha(DateTime Fecha)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@Fecha", SqlDbType.DateTime, 0, Fecha)
                    };
            DataSet ds = DBHelper.ExecuteDataSet("usp_MAT_Pago_GetPagosByFecha", dbParams);
            List<PagoModel> lPago = new List<PagoModel>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PagoModel item = new PagoModel();
                item.PagoID = dr["PagoID"].ToString(); ;
                item.FechaPago = Convert.ToDateTime(dr["FechaPago"]);
                item.NroRecibo = dr["NroRecibo"].ToString();
                item.Monto = Convert.ToDecimal(dr["Monto"]);
                item.TipoPagoDescripcion = dr["TipoPagoDescripcion"].ToString();
                item.TipoPago = Convert.ToInt32(dr["TipoPago"]);
                item.TransaccionID = dr["TransaccionID"].ToString();
                item.Vendedor = dr["Vendedor"].ToString();
                item.Cliente = dr["ClienteNombre"].ToString() + " " + dr["ClienteApellido"].ToString();
                lPago.Add(item);
            }

            return lPago;
        }

        public static void DeletePago(Guid PagoID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@PagoID", SqlDbType.UniqueIdentifier, 0, PagoID),
                        DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0, MATContext.CurrentVendedor.VendedorId ),
                    };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Pago_DeletePago", dbParams);
        }

        public static List<PagoModel> GetPagosByViaje(Guid ViajeID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, ViajeID)
                    };
            DataSet ds = DBHelper.ExecuteDataSet("usp_MAT_Pago_GetPagosByViaje", dbParams);
            List<PagoModel> lPago = new List<PagoModel>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                PagoModel item = new PagoModel();
                item.PagoID = dr["PagoID"].ToString(); ;
                item.sFechaPago = Convert.ToDateTime(dr["FechaPago"]).ToShortDateString() + " " + Convert.ToDateTime(dr["FechaPago"]).ToShortTimeString();
                item.NroRecibo = dr["NroRecibo"].ToString();
                item.Monto = Convert.ToDecimal(dr["Monto"]);
                item.TipoPagoDescripcion = dr["TipoPagoDescripcion"].ToString();
                item.Cliente = dr["Cliente"].ToString(); ;
                item.TransaccionID = dr["TransaccionID"].ToString();
                lPago.Add(item);
            }

            return lPago;
        }

        public static PagoDetalle GetPagoDetalleByPagoID(Guid PagoID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
            {
                DBHelper.MakeParam("@PagoID", SqlDbType.UniqueIdentifier, 0, PagoID)
            };
            DataSet ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_PagoDetalle_GetByPagoID", dbParams);
            PagoDetalle oPagoDetalle = new PagoDetalle
            {
                MonedaRecibida = "",
                MonedaEquivalente = "",
                Fecha = ""
            };
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                oPagoDetalle.MontoRecibido = dr["MontoRecibido"] != DBNull.Value && dr["MontoRecibido"] != null ? Convert.ToDecimal(dr["MontoRecibido"]) : 0;
                oPagoDetalle.MonedaRecibida = dr["MonedaRecibida"] != null && dr["MonedaRecibida"] != DBNull.Value ? dr["MonedaRecibida"].ToString() : "";
                oPagoDetalle.MontoEquivalente = dr["MontoEquivalente"] != DBNull.Value && dr["MontoEquivalente"] != null ? Convert.ToDecimal(dr["MontoEquivalente"]) : 0;
                oPagoDetalle.MonedaEquivalente = dr["MonedaEquivalente"] != null && dr["MonedaEquivalente"] != DBNull.Value ? dr["MonedaEquivalente"].ToString() : "";
                oPagoDetalle.Cotizacion = dr["Cotizacion"] != DBNull.Value && dr["Cotizacion"] != null ? Convert.ToDecimal(dr["Cotizacion"]) : 0;
                oPagoDetalle.Fecha = dr["Fecha"] != null && dr["Fecha"] != DBNull.Value ? dr["Fecha"].ToString() : "";
                oPagoDetalle.CanceladoPorNotaCredito = oPagoDetalle.MontoRecibido == 0;
            }
            else
            {
                oPagoDetalle.CanceladoPorNotaCredito = true;
            }
            return oPagoDetalle;
        }

        public static Decimal Pago_TotalPagosByFacturaID(Guid FacturaID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, FacturaID)
                    };
            DataSet ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_Pago_GetTotalPagoByFacturaID", dbParams);

            Decimal dReturn = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dReturn = Convert.ToDecimal(dr["TotalPagos"].ToString());
            }

            return dReturn;
        }

    }

    public class ReciboPagoViewModel
    {
        public string PagoID { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string NroRecibo { get; set; }
        public string TipoPagoDescripcion { get; set; }
        public string TransaccionID { get; set; }
        public string ClienteNombre { get; set; }
        public string Vendedor { get; set; }
        public string Moneda { get; set; }
        public string PaqueteDescripcion { get; set; }
        public string ViajeDescripcion { get; set; }
        public string NroFactura { get; set; }
        public PagoDetalle Detalle { get; set; }
    }

}