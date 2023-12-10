using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.Services;
using MAT.Entities;
using MAT.Utilities;
using System.Data.SqlClient;
using System.Data;
 
namespace MAT.MVC.Models
{
    public class FacturaModel
    {

        private FacturaService facturaService;
        private PasajeService pasajeService;
        private ClienteService clienteService;
        
        private PasajeroService pasajeroService;
        private PaqueteService paqueteService;
        private ViajeService viajeService;

        public Factura Factura { get; set; }
        public Pasajero Pasajero { get; set; }
        public Paquete Paquete { get; set; }
        public Viaje Viaje { get; set; }
        public MAT.Entities.PersonaCliente Cliente { get; set; }
        public List<PasajeModel> Pasajes { get; set; }
        public double Saldo { get; set; }        
        public FacturaModel()
        {
            facturaService = new FacturaService();
            pasajeService = new PasajeService();
            clienteService = new ClienteService();
            pasajeroService = new PasajeroService();
            paqueteService = new PaqueteService();
            viajeService = new ViajeService();
        }


        private double CalcularSaldo()
        {
            double montofactura = Factura.Monto.Value;
            var _pagos = new MovimientoCuentaService().GetByFacturaId(Factura.FacturaId).Where(p => p.PagoId.HasValue).ToList();
            double totalpagos = 0;
            foreach (var item in _pagos)
            {
                Pago pago = new PagoService().GetByPagoId(item.PagoId.Value);
                totalpagos += pago.Monto.Value;
            }

            #region Debitos
            var _debitos = new MovimientoCuentaService().GetByFacturaId(Factura.FacturaId).Where(p => p.DebitoId.HasValue).ToList();
            double totaldebitos = 0;
            foreach (var item in _debitos)
            {
                Debito debito = new DebitoService().GetByDebitoId(item.DebitoId.Value);
                totaldebitos += debito.MontoDebito.Value;
            }
            #endregion

            return montofactura - totalpagos - totaldebitos;
        }
    }

    public class FacturaStandard {
        public Guid FacturaID { get; set; }
        public string NroFactura { get; set; }
        public double Monto { get; set; }
        public double Saldo { get; set; }
        public int MonedaTipo { get; set; }
        public DateTime Fecha { get; set; }
        public string sFecha { get; set; }
        public int Tipo { get; set; }
        public int Estado { get; set; }
        public string EstadoDescripcion { get; set; }
        public Guid ClienteID { get; set; }
        public Guid VendedorID { get; set; }
        public double DescuentoAplicado { get; set; }
        public string Observaciones { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteApellido { get; set; }
        public string VendedorNombre { get; set; }
        public string VendedorApellido { get; set; }
        public string PaqueteDescripcion { get; set; }
        public string ViajeDescripcion { get; set; }
        public string ClienteFullName { get; set; }
    }

    public class FacturaDetalle {
        public Guid PasajeID { get; set; }
        public Guid PasajeroID { get; set; }
        public Guid ButacaID { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaCompra { get; set; }
        public Guid ViajeID { get; set; }
        public Guid FacturaID { get; set; }
        public int EstadoPasaje { get; set; }
        public Guid VoucherID { get; set; }
        //public Guid PrecioID { get; set; }
        public int ButacaNro { get; set; }
        public int ButacaPiso { get; set; }
        public string ButacaFila { get; set; }
        public string ButacaPosicion { get; set; }
        public string ButacaCodigoButaca { get; set; }
        public Guid PaqueteID { get; set; }
        public string PasajeroNombre { get; set; }
        public string PasajeroApellido { get; set; }
        public Guid HabitacionID { get; set; }
        public string PaqueteDescripcion { get; set; }
    }

    public class DBOFacturaDetalle {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Detalle { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
    }

    public class FacturaViaje {
        public string FacturaID { get; set; }
        public DateTime ViajeSalida { get; set; }
        public string PaqueteNombre { get; set; }
        public DateTime FacturaFecha { get; set; }
    }

    public class FacturaMetod {
        public static FacturaStandard FacturaStandardByID(Guid FacturaID)
        {
            FacturaStandard Factura = new FacturaStandard();
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@FacturaId", SqlDbType.UniqueIdentifier, 0, FacturaID),
                };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Factura_GetFacturaClienteByFacturaID", dbParams);

            while (_reader.Read())
            {
                ReservaStandard Item = new ReservaStandard();

                if (_reader["FacturaID"].ToString() != "")
                {
                    Factura.FacturaID = new Guid(_reader["FacturaID"].ToString());
                }
                if (_reader["NroFactura"].ToString() != "")
                {
                    Factura.NroFactura = _reader["NroFactura"].ToString();
                }
                if (_reader["Monto"].ToString() != "")
                {
                    Factura.Monto = Convert.ToDouble(_reader["Monto"]);
                }
                if (_reader["Fecha"].ToString() != "")
                {
                    Factura.Fecha = Convert.ToDateTime(_reader["Fecha"].ToString());
                }
                if (_reader["Tipo"].ToString() != "")
                {
                    Factura.Tipo = Convert.ToInt32(_reader["Tipo"]);
                }
                if (_reader["Estado"].ToString() != "")
                {
                    Factura.Estado = Convert.ToInt32(_reader["Estado"]);
                }
                if (_reader["ClienteID"].ToString() != "")
                {
                    Factura.ClienteID = new Guid(_reader["ClienteID"].ToString());
                }
                if (_reader["VendedorID"].ToString() != "")
                {
                    Factura.VendedorID = new Guid(_reader["VendedorID"].ToString());
                }

                //if (_reader["DescuentoAplicado"].ToString() != "")
                //{
                //    Factura.DescuentoAplicado = Convert.ToDouble(_reader["DescuentoAplicado"]);
                //}
                Factura.Observaciones = _reader["Observaciones"].ToString();
                Factura.ClienteNombre = _reader["Nombre"].ToString();
                Factura.ClienteApellido = _reader["Apellido"].ToString();
                Factura.VendedorNombre = _reader["VendedorNombre"].ToString();
                Factura.VendedorApellido = _reader["VendedorApellido"].ToString();
                if (_reader["Saldo"].ToString() != "")
                {
                    Factura.Saldo = Convert.ToDouble(_reader["Saldo"]);
                }
                
            }
            _reader.NextResult();
            if (_reader.Read())
            {
                Factura.PaqueteDescripcion = _reader["PaqueteDescripcion"].ToString();
                Factura.MonedaTipo = Convert.ToInt32(_reader["MonedaTipo"]);
            }
            return Factura;
        }

        public static List<FacturaDetalle> FacturaDetalleByID(Guid FacturaID)
        {
            List<FacturaDetalle> DetalleFactura = new List<FacturaDetalle>();
            
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@FacturaId", SqlDbType.UniqueIdentifier, 0, FacturaID),
                };
            SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_Factura_DetalleFacturaByFacturaID", dbParams);

            while (_reader.Read())
            {
                FacturaDetalle Item = new FacturaDetalle();
                if (_reader["pasajeid"].ToString() != "")
                {
                    Item.PasajeID = new Guid(_reader["pasajeid"].ToString());
                }
                if (_reader["pasajeroid"].ToString() != "")
                {
                    Item.PasajeroID = new Guid(_reader["pasajeroid"].ToString());
                }
                if (_reader["butacaid"].ToString() != "")
                {
                    Item.ButacaID = new Guid(_reader["butacaid"].ToString());
                }
                if (_reader["fechareserva"].ToString() != "")
                {
                    Item.FechaReserva = Convert.ToDateTime(_reader["fechareserva"]);
                }
                if (_reader["fechacompra"].ToString() != "")
                {
                    Item.FechaCompra = Convert.ToDateTime(_reader["fechacompra"]);
                }
                if (_reader["viajeid"].ToString() != "")
                {
                    Item.ViajeID = new Guid(_reader["viajeid"].ToString());
                }
                if (_reader["facturaid"].ToString() != "")
                {
                    Item.FacturaID = new Guid(_reader["facturaid"].ToString());
                }
                if (_reader["estadopasaje"].ToString() != "")
                {
                    Item.EstadoPasaje = Convert.ToInt32(_reader["estadopasaje"]);
                }
                if (_reader["voucherid"].ToString() != "")
                {
                    Item.VoucherID = new Guid(_reader["voucherid"].ToString());
                }
                //if (_reader["precioid"].ToString() != "")
                //{
                //    Item.PrecioID = new Guid(_reader["precioid"].ToString());
                //}
                if (_reader["ButacaNro"].ToString() != "")
                {
                    Item.ButacaNro = Convert.ToInt32(_reader["ButacaNro"]);
                }
                if (_reader["ButacaPiso"].ToString() != "")
                {
                    Item.ButacaPiso = Convert.ToInt32(_reader["ButacaPiso"]);
                }
                if (_reader["ButacaFila"].ToString() != "")
                {
                    Item.ButacaFila = _reader["ButacaFila"].ToString();
                }
                if (_reader["ButacaPosicion"].ToString() != "")
                {
                    Item.ButacaPosicion = _reader["ButacaPosicion"].ToString();
                }
                if (_reader["ButacaCodigoButaca"].ToString() != "")
                {
                    Item.ButacaCodigoButaca = _reader["ButacaCodigoButaca"].ToString();
                }
                if (_reader["PaqueteID"].ToString() != "")
                {
                    Item.PaqueteID = new Guid(_reader["PaqueteID"].ToString());
                }
                Item.PasajeroNombre = _reader["PasajeroNombre"].ToString();
                Item.PasajeroApellido = _reader["PasajeroApellido"].ToString();
                if (_reader["HabitacionID"].ToString() != "")
                {
                    Item.HabitacionID = new Guid(_reader["HabitacionID"].ToString());
                }

                DetalleFactura.Add(Item);
            }

            return DetalleFactura;
        }

        public static List<FacturaStandard> ListFacturaByClienteID(Guid ClienteID)
        {
            List<FacturaStandard> ListFactura = new List<FacturaStandard>();
              SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, ClienteID),
                };
              SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Factura_GetFacturaByClienteID", dbParams);

            while (_reader.Read())
            {
                FacturaStandard Factura = new FacturaStandard();
                if (_reader["facturaid"].ToString() != "")
                {
                    Factura.FacturaID = new Guid(_reader["facturaid"].ToString());
                }
                 if (_reader["nrofactura"].ToString() != "")
                {
                    Factura.NroFactura = _reader["nrofactura"].ToString();
                }
                 if (_reader["monto"].ToString() != "")
                {
                    Factura.Monto = Convert.ToDouble(_reader["monto"].ToString());
                }
                 if (_reader["fecha"].ToString() != "")
                {
                    Factura.Fecha = Convert.ToDateTime(_reader["fecha"].ToString());
                }
                 if (_reader["EstadoFactura"].ToString() != "")
                {
                    Factura.EstadoDescripcion = _reader["EstadoFactura"].ToString();
                }
                 if (_reader["estado"].ToString() != "")
                {
                    Factura.Estado = Convert.ToInt32(_reader["estado"].ToString());
                }
                 if (_reader["clienteid"].ToString() != "")
                {
                    Factura.ClienteID = new Guid(_reader["clienteid"].ToString());
                }
                 if (_reader["PersonaNombre"].ToString() != "")
                {
                    Factura.ClienteNombre = _reader["PersonaNombre"].ToString();
                }
                 if (_reader["PersonaApellido"].ToString() != "")
                {
                    Factura.ClienteApellido = _reader["PersonaApellido"].ToString();
                }
                 if (_reader["PaqueteDescripcion"].ToString() != "")
                {
                    Factura.PaqueteDescripcion = _reader["PaqueteDescripcion"].ToString();
                }
                 ListFactura.Add(Factura);
            }

            return ListFactura;
        }

        public static bool UpdateAndCheckStates(Guid MovimientoID)
        {
            bool bResult = false;
            try
            {
                List<FacturaStandard> ListFactura = new List<FacturaStandard>();
                SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@MovimientoID", SqlDbType.UniqueIdentifier, 0, MovimientoID),
                };
                SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Factura_ActualizarEstadosByMovimientoID", dbParams);

                if (_reader.Read())
                {
                    if (_reader["Result"].ToString() == "Done.")
                    {
                        bResult = true;
                    }
                }

                return bResult;
            }
            catch 
            {
                bResult = false;
                return bResult;
            }
            
        }

        public static bool DeletePago(Guid MovimientoID, Guid VendedorID)
        {
            bool bResult = false;
            try
            {
                List<FacturaStandard> ListFactura = new List<FacturaStandard>();
                SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@MovimientoID", SqlDbType.UniqueIdentifier, 0, MovimientoID),
                    DBHelper.MakeParam("@VendedorID", SqlDbType.UniqueIdentifier, 0, VendedorID)
                };
                SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Factura_BorrarUnPago", dbParams);

                if (_reader.Read())
                {
                    if (_reader["Result"].ToString() == "Done.")
                    {
                        bResult = true;
                    }
                }

                return bResult;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                bResult = false;
                return bResult;
            }

        }

        public static void AgregarDescuento_Recargo(string FacturaID, string Detalle, decimal Monto, bool IsDescuento)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@FacturaID", SqlDbType.VarChar, 0, FacturaID),
                        DBHelper.MakeParam("@Detalle", SqlDbType.VarChar, 0, Detalle),
                        DBHelper.MakeParam("@Monto", SqlDbType.Float, 0, Monto),
                        DBHelper.MakeParam("@IsDescuento", SqlDbType.Bit, 0, IsDescuento)
                        
                    };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_DetalleFactura_AgregarDescuentoRecargo", dbParams);

        }

        public static List<DBOFacturaDetalle> GetDetalleByFacturaID(Guid FacturaID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, FacturaID),
                };
            DataSet ds = DBHelper.ExecuteDataSet("usp_MAT_Factura_GetDetalleByFacturaID", dbParams);

            List<DBOFacturaDetalle> Detalle = new List<DBOFacturaDetalle>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DBOFacturaDetalle item = new DBOFacturaDetalle();
                item.Id = Convert.ToInt32(dr["Id"]);
                item.Fecha = Convert.ToDateTime(dr["Fecha"]);
                item.Detalle = dr["Detalle"].ToString();
                item.Cantidad = Convert.ToInt32(dr["Cantidad"]);
                item.Precio = Convert.ToDouble(dr["Precio"]);
                Detalle.Add(item);
            }


            return Detalle.OrderBy(l => l.Detalle).ToList();
        }

        public static List<FacturaViaje> GetListFacturaByViajeByClienteID(Guid ClienteID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@ClienteID", SqlDbType.UniqueIdentifier, 0, ClienteID),
                };
            DataSet ds = DBHelper.ExecuteDataSet("usp_MAT_Factura_GetListFacturaIDByViajeByClienteID", dbParams);

            List<FacturaViaje> list = new List<FacturaViaje>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                FacturaViaje row = new FacturaViaje();
                row.FacturaID = dr["FacturaID"].ToString();
                row.ViajeSalida = Convert.ToDateTime(dr["ViajeSalida"]);
                row.PaqueteNombre = dr["PaqueteNombre"].ToString();
                row.FacturaFecha = Convert.ToDateTime(dr["FacturaFecha"]);
                list.Add(row);
            }

            return list;
        }

        public static List<FacturaStandard> FacturaSearch(string sPersonaID, string sViajeID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@ClienteID", SqlDbType.VarChar, 0,  sPersonaID),
                    DBHelper.MakeParam("@ViejeId", SqlDbType.VarChar, 0, sViajeID),
                };
            DataSet ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_Factura_Search", dbParams);

            List<FacturaStandard> ListFactura = new List<FacturaStandard>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                FacturaStandard row = new FacturaStandard();
                row.FacturaID = new Guid(dr["FacturaID"].ToString());
                row.NroFactura = dr["NroFactura"].ToString();
                row.Monto = Convert.ToDouble(dr["Monto"]);
                row.Fecha = Convert.ToDateTime(dr["FechaFactura"]);
                row.EstadoDescripcion = dr["EstadoFactura"].ToString();
                row.PaqueteDescripcion = dr["Paquete"].ToString();
                row.ViajeDescripcion = dr["Viaje"].ToString();
                row.ClienteFullName = dr["ClienteNomre"].ToString();
                ListFactura.Add(row);

            }

            return ListFactura;
        }

        public static List<string> GetMoreDetailsByFacturaID(string sFacturaID)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                {                    
                    DBHelper.MakeParam("@FacturaID", SqlDbType.VarChar, 0,  sFacturaID)
                };
            DataSet ds = DBHelper.ExecuteDataSet("dbo.usp_MAT_Factura_GetPasajerosByFacturaID", dbParams);

            List<string> ListPasajeros = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string sPasajero = dr["PasajeroFullName"].ToString();
                ListPasajeros.Add(sPasajero);
                

            }
                
            return ListPasajeros;
        }
    }
}