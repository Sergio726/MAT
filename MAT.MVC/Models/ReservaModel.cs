using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using MAT.Entities;
using MAT.Services;
using MAT.Enums;
using System.Web.Mvc;
using System.Collections.Specialized;
using MAT.MVC.Common;
using MAT.Utilities;
using System.Data.SqlClient;
using System.Data;
using MAT.Enums.SharedModels;

namespace MAT.MVC.Models
{
    //public class PostPasaje {
    //    public string adicionalesid { get; set; }
    //    public string pasajeid { get; set; }
    //    public string pasajeroid { get; set; }
    //    public string precioid { get; set; }
    //}
    public class ReservaModel
    {
        private ClienteService clienteService;
        //private CuentaService cuentaService;
        private PasajeroService pasajeroService;


        //public MAT.Entities.Cliente Cliente { get; set; }
        public IList<PasajeModel> Pasajes { get; set; }
        public Cuenta Cuenta { get; set; }
        public Pago Pago { get; set; }
        //public Factura Factura { get; set; }
        public eEstadoFactura EstadoFactura { get; set; }

        public ReservaModel(List<PasajeInputModel> pasajes)
        {
            try
            {
                pasajeroService = new PasajeroService();

                List<PasajeModel> _pasajes = new List<PasajeModel>();
                foreach (var item in pasajes)
                {
                    PasajeModel newpasaje = new PasajeModel(new Guid(item.pasajeid));
                    newpasaje.PrecioID = item.precioid;
                    newpasaje.AdicionalesID = item.adicionalesid;
                    newpasaje.Pasajero = pasajeroService.GetByPasajeroId(new Guid(item.pasajeroid));
                    newpasaje.Precio = Infrastructure.Data.PaqueteDataAccess.GetPrecioById(new Guid(item.precioid)).Monto;
                    double totaladicional = 0;
                    if (!string.IsNullOrEmpty(item.adicionalesid))
                    {
                        String[] _adicionalesid = item.adicionalesid.Split(';');
                        foreach (var _ad in _adicionalesid)
                        {
                            totaladicional += Infrastructure.Data.MaestrosDataAccess.GetAdicionalById(new Guid(_ad)).Monto;
                        }
                    }
                    newpasaje.Adicionales = totaladicional;
                    _pasajes.Add(newpasaje);
                }
                Pasajes = _pasajes;

            }
            catch (Exception ex)
            {
                MATLogger.Log(String.Format("{0} {1}", ex.Message, ex.StackTrace), 1);
                throw new Exception();
            }
        }
        public ReservaModel()
        {
            clienteService = new ClienteService();
            //ccService = new CuentaCorrienteService();
            pasajeroService = new PasajeroService();
        }

        #region Metodos Publicos
        public static double CalcularMontoTotal(List<Guid> precioIds, List<System.Guid> adicionalIds)
        {
            double MontoFactura = 0;
            try
            {
                double totalfactura = 0;
                double totalprecio = 0;
                double totaladicional = 0;

                using (SqlDataReader _readerP = PrecioMethod.GetByIds(precioIds))
                {
                    while (_readerP.Read())
                    {
                        totalprecio = totalprecio + Convert.ToDouble(_readerP["MontoTotal"]);
                    }
                }

                using (SqlDataReader _readerA = AdicionalMethod.GetByIds(adicionalIds))
                {
                    while (_readerA.Read())
                    {
                        totaladicional = totaladicional + Convert.ToDouble(_readerA["MontoTotal"]);
                    }
                }

                totalfactura = totalprecio + totaladicional;
                MontoFactura = totalfactura;
            }
            catch (Exception ex)
            {
                MATLogger.Log(String.Format("{0} {1}", ex.Message, ex.StackTrace), 1);
                throw new Exception();
            }
            return MontoFactura;
        }

        //public void EnlazarCliente(Guid clienteid)
        //{
        //    try
        //    {
        //        Cliente = clienteService.GetByClienteId(clienteid);
        //        Cuenta = cuentaService.GetByClienteId(clienteid).FirstOrDefault();
        //        CC = ccService.GetByClienteId(clienteid).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        MATLogger.Log(String.Format("{0} {1}", ex.Message, ex.StackTrace), 1);
        //        throw new Exception();
        //    }

        //}

        //public void ModificarEstados()
        //{
        //    try
        //    {
        //        foreach (PasajeModel pasaje in Pasajes)
        //        {
        //            pasaje.Pasaje.EstadoPasaje = (int)Factura.Estado;
        //            pasaje.Estado = (eEstadoPasaje)Factura.Estado;
        //            pasajeService.Update(pasaje.Pasaje);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MATLogger.Log(String.Format("{0} {1}", ex.Message, ex.StackTrace), 1);
        //        throw new Exception();
        //    }
        //}

        //public void GuardarImportes(string importe, string tipopago, string transaccionid, string condicion, string descuento, string recibo, string observaciones, double MontoFactura)
        //{
        //    try
        //    {
        //        double _descuento = !string.IsNullOrEmpty(descuento) ? Convert.ToDouble(descuento) : 0;
        //        double _nuevomonto = MontoFactura - _descuento;
        //        Factura _factura = new Factura();
        //        _factura.FacturaId = Guid.NewGuid();
        //        _factura.ClienteId = Cliente.ClienteId;
        //        if (MATContext.CurrentVendedor != null) _factura.VendedorId = MATContext.CurrentVendedor.VendedorId;
        //        _factura.Monto = _nuevomonto;
        //        _factura.DescuentoAplicado = _descuento;
        //        _factura.Fecha = DateTime.Now;
        //        if (!string.IsNullOrEmpty(observaciones)) _factura.Observaciones = observaciones;
        //        facturaService.Insert(_factura);
        //        Factura = _factura;

        //        MovimientoCuenta _impactofactura = new MovimientoCuenta();
        //        _impactofactura.MovimientoId = Guid.NewGuid();
        //        _impactofactura.CuentaId = Cuenta.CuentaId;
        //        _impactofactura.FacturaId = _factura.FacturaId;
        //        _impactofactura.FechaRegistro = _factura.Fecha;
        //        movimientoService.Insert(_impactofactura);

        //        CuentaCorriente _ccresto = new CuentaCorriente();
        //        _ccresto.CuentaCorrienteId = Guid.NewGuid();
        //        _ccresto.Monto = -Factura.Monto;
        //        _ccresto.Fecha = DateTime.Now;
        //        _ccresto.ClienteId = Cliente.ClienteId;
        //        ccService.Insert(_ccresto);

        //        if (condicion == "Efectivo")
        //        {
        //            eFormaPago formapago = (eFormaPago)Convert.ToInt32(tipopago);
        //            if (formapago == eFormaPago.Nota_de_Credito)
        //            {
        //                CuentaCorriente _ccdebito = new CuentaCorriente();
        //                _ccdebito.CuentaCorrienteId = Guid.NewGuid();
        //                _ccdebito.Monto = Convert.ToDouble(importe);
        //                _ccdebito.Fecha = DateTime.Now;
        //                _ccdebito.ClienteId = Cliente.ClienteId;
        //                ccService.Insert(_ccdebito);

        //                DebitoService debitoService = new DebitoService();
        //                Entities.Debito debito = new Debito();
        //                debito.DebitoId = Guid.NewGuid();
        //                debito.Fecha = DateTime.Now;
        //                debito.MontoDebito = Convert.ToDouble(importe);
        //                if (MATContext.CurrentVendedor != null) debito.VendedorId = MATContext.CurrentVendedor.VendedorId;
        //                debito.ClienteId = Cliente.ClienteId;
        //                debitoService.Insert(debito);

        //                #region Impacto Debito en Movimiento de Cuenta
        //                MovimientoCuentaService movService = new MovimientoCuentaService();
        //                MovimientoCuenta impactodebito = new MovimientoCuenta();
        //                impactodebito.MovimientoId = Guid.NewGuid();
        //                impactodebito.DebitoId = debito.DebitoId;
        //                impactodebito.FechaRegistro = debito.Fecha.Value;
        //                impactodebito.CuentaId = Cuenta.CuentaId;
        //                impactodebito.FacturaId = _factura.FacturaId;
        //                impactodebito.CuentaCorrienteId = _ccdebito.CuentaCorrienteId;
        //                movService.Insert(impactodebito);
        //                #endregion

        //                if (debito.MontoDebito < Factura.Monto)
        //                {
        //                    MovimientoCuenta _impactocc = new MovimientoCuenta();
        //                    _impactocc.MovimientoId = Guid.NewGuid();
        //                    _impactocc.CuentaId = Cuenta.CuentaId;
        //                    _impactocc.CuentaCorrienteId = _ccresto.CuentaCorrienteId;
        //                    _impactocc.FacturaId = _factura.FacturaId;
        //                    _impactocc.FechaRegistro = DateTime.Now;
        //                    movimientoService.Insert(_impactocc);
        //                    Factura.Estado = (int)eEstadoFactura.Señado;
        //                }
        //                else if (debito.MontoDebito == Factura.Monto)
        //                {
        //                    Factura.Estado = (int)eEstadoFactura.Pagado;                            
        //                }
        //                facturaService.Update(Factura);
        //            }
        //            else
        //            {
        //                CuentaCorriente _ccpago = new CuentaCorriente();
        //                _ccpago.CuentaCorrienteId = Guid.NewGuid();
        //                _ccpago.Monto = Convert.ToDouble(importe);
        //                _ccpago.Fecha = DateTime.Now;
        //                _ccpago.ClienteId = Cliente.ClienteId;
        //                ccService.Insert(_ccpago);

        //                Pago _pago = new Pago();
        //                _pago.PagoId = Guid.NewGuid();
        //                _pago.Monto = Convert.ToDouble(importe);
        //                _pago.ClienteId = Cliente.ClienteId;
        //                _pago.FechaPago = DateTime.Now;
        //                _pago.NroRecibo = recibo;
        //                _pago.EstadoRendicion = (int)eEstadoRendicion.Pendiente;
        //                _pago.CuentaCorrienteId = _ccpago.CuentaCorrienteId;
        //                _pago.TipoPago = Convert.ToInt32(tipopago);
        //                if (formapago == eFormaPago.Credito || formapago == eFormaPago.Mercado_Pago || formapago == eFormaPago.Transferencia)
        //                {
        //                    _pago.TransaccionId = transaccionid;
        //                }
        //                _pago.VendedorId = MATContext.CurrentVendedor.VendedorId;
        //                pagoService.Insert(_pago);
        //                Pago = _pago;

        //                MovimientoCuenta _impactopago = new MovimientoCuenta();
        //                _impactopago.MovimientoId = Guid.NewGuid();
        //                _impactopago.CuentaId = Cuenta.CuentaId;
        //                _impactopago.PagoId = _pago.PagoId;
        //                _impactopago.CuentaCorrienteId = _ccpago.CuentaCorrienteId; //Enlace de pago con cuenta corriente
        //                _impactopago.FacturaId = _factura.FacturaId;
        //                _impactopago.FechaRegistro = DateTime.Now;
        //                movimientoService.Insert(_impactopago);
        //                List<Pasaje> _pasajes = pasajeService.GetByFacturaId(Factura.FacturaId).ToList();
        //                if (Pago.Monto < Factura.Monto)
        //                {
        //                    MovimientoCuenta _impactocc = new MovimientoCuenta();
        //                    _impactocc.MovimientoId = Guid.NewGuid();
        //                    _impactocc.CuentaId = Cuenta.CuentaId;
        //                    _impactocc.CuentaCorrienteId = _ccresto.CuentaCorrienteId;
        //                    _impactocc.FacturaId = _factura.FacturaId;
        //                    _impactocc.FechaRegistro = DateTime.Now;
        //                    movimientoService.Insert(_impactocc);
        //                    Factura.Estado = (int)eEstadoFactura.Señado;
        //                }
        //                else if (Pago.Monto == Factura.Monto)
        //                {
        //                    Factura.Estado = (int)eEstadoFactura.Pagado;
        //                }
        //                facturaService.Update(Factura);
        //            }

        //        }
        //        else if (condicion == "Cuenta Corriente")
        //        {
        //            CuentaCorriente _cc = new CuentaCorriente();
        //            _cc.CuentaCorrienteId = Guid.NewGuid();
        //            _cc.Monto = -MontoFactura;
        //            _cc.Fecha = DateTime.Now;
        //            _cc.ClienteId = Cliente.ClienteId;
        //            ccService.Insert(_cc);

        //            MovimientoCuenta _impactocc = new MovimientoCuenta();
        //            _impactocc.MovimientoId = Guid.NewGuid();
        //            _impactocc.CuentaId = Cuenta.CuentaId;
        //            _impactocc.CuentaCorrienteId = _cc.CuentaCorrienteId;
        //            _impactocc.FacturaId = _factura.FacturaId;
        //            _impactocc.FechaRegistro = DateTime.Now;
        //            movimientoService.Insert(_impactocc);

        //            Factura.Estado = (int)eEstadoFactura.Prereserva;
        //            facturaService.Update(Factura);
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        MATLogger.Log(String.Format("{0} {1}", ex.Message, ex.StackTrace), 1);
        //        throw new Exception();
        //    }
        //}

        //public void EnlazarDatosPasajes()
        //{
        //    try
        //    {
        //        foreach (PasajeModel pasaje in Pasajes)
        //        {
        //            pasaje.Pasaje.FacturaId = Factura.FacturaId;
        //            pasaje.Pasaje.PasajeroId = pasaje.Pasajero.PasajeroId;
        //            pasaje.Pasaje.PrecioId = new Guid(pasaje.PrecioID);
        //            if (!string.IsNullOrEmpty(pasaje.AdicionalesID))
        //            {
        //                String[] _adicionalesid = pasaje.AdicionalesID.Split(';');
        //                foreach (var _ad in _adicionalesid)
        //                {
        //                    PasajeAdicionalService pasajeadicionalService = new PasajeAdicionalService();
        //                    Entities.PasajeAdicional pasajeadicional = new PasajeAdicional()
        //                    {
        //                        PasajeAdicionalId = Guid.NewGuid(),
        //                        AdicionalId = new Guid(_ad),
        //                        PasajeId = pasaje.Pasaje.PasajeId
        //                    };
        //                    pasajeadicionalService.Insert(pasajeadicional);
        //                }
        //            }
        //            if (Factura.Estado== (int)eEstadoFactura.Pagado)
        //            {
        //                //- Generación de Vouchers
        //                VoucherService voucherService = new VoucherService();
        //                Voucher voucher = new Voucher();
        //                voucher.VoucherId = Guid.NewGuid();
        //                voucher.FechaEmision = DateTime.Now;
        //                if (MATContext.CurrentVendedor!=null) voucher.VendedorId = MATContext.CurrentVendedor.VendedorId;
        //                voucherService.Insert(voucher);
        //                //-

        //                pasaje.Pasaje.VoucherId = voucher.VoucherId;
        //                pasaje.Pasaje.EstadoPasaje = (int)eEstadoPasaje.Pagado;
        //            }
                        

        //            pasajeService.Update(pasaje.Pasaje);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MATLogger.Log(String.Format("{0} {1}", ex.Message, ex.StackTrace), 1);
        //        throw new Exception();
        //    }
        //}
        #endregion

    }

    public class ReservaStandard
    {
        public Guid PasajeId { get; set; }
        public Guid PasajeroId { get; set; }
        public Guid ButacaId { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaCompra { get; set; }
        public Guid ViajeId { get; set; }
        public Guid FacturaId { get; set; }
        public Int32 EstadoPasaje { get; set; }
        public Guid VoucherId { get; set; }
        public Guid PrecioId { get; set; }
        public string ButacaNro { get; set; }
        public int ButacaPiso { get; set; }
        public string ButacaFila { get; set; }
        public string ButacaPosicion { get; set; }
        public string ButacaCodigoButaca { get; set; }
        public int? ButacaTipo { get; set; }
        public Guid TransporteID {get;set;}
        public string TransporteNroCoche { get; set; }
        public Guid PaqueteID { get; set; }
        public string PasajeroNombre { get; set; }
        public string PasajeroApellido { get; set; }
        public int MonedaTipo { get; set; }
        public string TransporteTipo { get; set; }
        public string PrecioCama { get; set; }
        public string PrecioSemicama { get; set; }
        public string PrecioCalculado { get; set; }
    }

    public class DistribucionCoche
    {
        public int ButacaNro { get; set; }
        public string ButacaPosicion { get; set; }
        public string ButacaCodigo { get; set; }
        public string PasajeID { get; set; }
        public int EstadoPasaje { get; set; }
        public string PasajeroID { get; set; }
        public string PasajeroApellido { get; set; }
        public string PasajeroNombre { get; set; }
    }

    public class DistribucionCocheSeatViewModel
    {
        public DistribucionCoche Item { get; set; }
        public string SeatClass { get; set; }
        public string PosicionClass { get; set; }
    }

    public class PreReserva {
        public string FacturaID { get; set; }
        public string ClienteID { get; set; }
        public string FullName { get; set; }
        public DateTime FechaPreReserva { get; set; }
        public DateTime VencimientoPreReserva { get; set; }
        public string NroButaca { get; set; }
    }

    public class PFC {
        public string PasajeID { get; set; }
        public string FacturaID { get; set; }
        public string ClienteID { get; set; }
    }

    public class ReservaMethod {
        /// <summary>
        /// Clase CSS de estado de butaca (misma lógica que Reserva/Index).
        /// </summary>
        public static string GetCssClassEstadoButaca(int estado)
        {
            switch (estado)
            {
                case 1: return "disponible";
                case 2:
                case 3: return "señado";
                case 4: return "reservado";
                case 5: return "prereserva";
                case 6: return "reservahotel";
                case 7: return "anulado";
                case 8: return "reservapasajehotel";
                case 9: return "prereservahotel";
                default: return "disponible";
            }
        }

        public static string GetDescripcionEstadoButaca(int estado)
        {
            if (estado == 9)
            {
                return "Pre-reserva + Hotel";
            }

            if (!Enum.IsDefined(typeof(eEstadoPasaje), estado))
            {
                return estado > 0 ? estado.ToString() : "Disponible";
            }

            var name = Enum.GetName(typeof(eEstadoPasaje), estado);
            var field = typeof(eEstadoPasaje).GetField(name);
            if (field == null)
            {
                return name;
            }

            var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attr != null ? attr.Description : name;
        }

        public static List<PreReserva> GetPreReservaVencidas(string ViajeID)
        {
            List<PreReserva> ListPreReserva = new List<PreReserva>();

              SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, ViajeID),
                    };
                using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Reserva_GetReservasVencidas", dbParams))
                {
                    while (_reader.Read())
                    {
                        PreReserva PreReserva = new PreReserva();
                        PreReserva.FacturaID = _reader["FacturaID"].ToString();
                        PreReserva.ClienteID = _reader["ClienteID"].ToString();
                        PreReserva.FullName = _reader["FullName"].ToString();
                        if (_reader["FechaPreReserva"].ToString() != "")
                        {
                            PreReserva.FechaPreReserva = Convert.ToDateTime(_reader["FechaPreReserva"]);
                        }

                        if (_reader["VencimientoPreReserva"].ToString() != "")
                        {
                            PreReserva.VencimientoPreReserva = Convert.ToDateTime(_reader["VencimientoPreReserva"]);
                        }
                        PreReserva.NroButaca = _reader["NroButaca"].ToString();
                        ListPreReserva.Add(PreReserva);
                    }
                }
                return ListPreReserva;
        }

        public static string RenovarPreReserva(string FacturaID, int Dias = 10)
        {
            string sResult = "";

            try
            {
                if (FacturaID != "" && FacturaID != null)
                {
                    SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@FacturaID", SqlDbType.VarChar, 0, FacturaID),
                        DBHelper.MakeParam("@VendedorID", SqlDbType.VarChar, 0, MATContext.CurrentVendedor.VendedorId.ToString()),
                        DBHelper.MakeParam("@CantDias", SqlDbType.Int, 0, Dias),
                    };
                    using (SqlDataReader _reader = DBHelper.ExecuteDataReader("usp_MAT_Reserva_ExtenderPreReserva", dbParams))
                    {
                        if (_reader.Read())
                        {
                            sResult = _reader["Result"].ToString();
                        }
                    }
                }

            }
            catch (Exception e)
            {
                sResult = e.Message;

            }
            return sResult;
        }

        public static List<ReservaStandard> GetListOfPasajesByViajeID(string ViajeID)
        {
            List<ReservaStandard> Model = new List<ReservaStandard>();
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.VarChar, 0, ViajeID),
                    };
            using (SqlDataReader _reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_Reserva_GetPasajeByViajeID", dbParams))
            {
                while (_reader.Read())
            {
                ReservaStandard Item = new ReservaStandard();

                if (_reader["pasajeid"].ToString() != "")
                {
                    Item.PasajeId = new Guid(_reader["pasajeid"].ToString());
                }
                if (_reader["pasajeroid"].ToString() != "")
                {
                    Item.PasajeroId = new Guid(_reader["pasajeroid"].ToString());
                }
                if (_reader["butacaid"].ToString() != "")
                {
                    Item.ButacaId = new Guid(_reader["butacaid"].ToString());
                }
                if (_reader["fechareserva"].ToString() != "")
                {
                    Item.FechaReserva = Convert.ToDateTime(_reader["fechareserva"].ToString());
                }
                if (_reader["fechacompra"].ToString() != "")
                {
                    Item.FechaCompra = Convert.ToDateTime(_reader["fechacompra"].ToString());
                }
                if (_reader["viajeid"].ToString() != "")
                {
                    Item.ViajeId = new Guid(_reader["viajeid"].ToString());
                }
                if (_reader["facturaid"].ToString() != "")
                {
                    Item.FacturaId = new Guid(_reader["facturaid"].ToString());
                }
                if (_reader["estadopasaje"].ToString() != "")
                {
                    Item.EstadoPasaje = Convert.ToInt32(_reader["estadopasaje"]);
                }
                if (_reader["voucherid"].ToString() != "")
                {
                    Item.VoucherId = new Guid(_reader["voucherid"].ToString());
                }
                //if (_reader["precioid"].ToString() != "")
                //{
                //    Item.PrecioId = new Guid(_reader["precioid"].ToString());
                //}
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
                if (_reader["TransporteID"].ToString() != "")
                {
                    Item.TransporteID = new Guid(_reader["TransporteID"].ToString());
                }
                if (_reader["PaqueteID"].ToString() != "")
                {
                    Item.PaqueteID = new Guid(_reader["PaqueteID"].ToString());
                }
                if (_reader["PasajeroNombre"].ToString() != "")
                {
                    Item.PasajeroNombre = _reader["PasajeroNombre"].ToString();
                }
                if (_reader["PasajeroApellido"].ToString() != "")
                {
                    Item.PasajeroApellido = _reader["PasajeroApellido"].ToString();
                }
                if (_reader["TransporteNroCoche"].ToString() != "")
                {
                    Item.TransporteNroCoche = _reader["TransporteNroCoche"].ToString();
                }
                if (_reader["TransporteTipo"].ToString() != "")
                {
                    Item.TransporteTipo = _reader["TransporteTipo"].ToString();
                }
                Item.MonedaTipo = Convert.ToInt32(_reader["MonedaTipo"]);
                Model.Add(Item);
                }
            }

            return Model;

        }

        public static void UpdatePasajeAdicionalesVoucher(string PasajeID, string FacturaID, string PasajeroID, string AdicionalesIDs, int EstadoFactura,string VendedorID)
        {
            SqlParameter[] _dbParams = new SqlParameter[]
                        {                    
                            DBHelper.MakeParam("@PasajeID", SqlDbType.VarChar, 0, PasajeID),
                            DBHelper.MakeParam("@FacturaID", SqlDbType.VarChar, 0, FacturaID), //FacturaID
                            DBHelper.MakeParam("@PasajeroID", SqlDbType.VarChar, 0, PasajeroID),
                            DBHelper.MakeParam("@AdicionalesIDs", SqlDbType.VarChar, 0, AdicionalesIDs),
                            DBHelper.MakeParam("@EstadoFactura", SqlDbType.Int, 0, EstadoFactura), //EstadoFactura
                            DBHelper.MakeParam("@VendedorID", SqlDbType.VarChar, 0, VendedorID)
                        };
            DBHelper.ExecuteNonQuery("usp_MAT_Reserva_UpdatePasajeAdicionalesVoucher", _dbParams);
        }

        public static void AddDetalleFactura(string FacturaID, string AdicionalesIDs, string PrecioID)
        {
            List<ReservaStandard> Model = new List<ReservaStandard>();
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@FacturaID", SqlDbType.VarChar, 0, FacturaID),
                        DBHelper.MakeParam("@AdicionalesIDs", SqlDbType.VarChar, 0, AdicionalesIDs),
                        DBHelper.MakeParam("@PrecioID", SqlDbType.VarChar, 0, PrecioID)

                    };
            DBHelper.ExecuteNonQuery("usp_MAT_Reserva_DetalleFactura", dbParams); 
        }

        public static DataSet RegistrarFactura(string ClienteID, string VendedorID, string Observaciones,string Condicion, int MonedaTipo)
        {
            List<ReservaStandard> Model = new List<ReservaStandard>();
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ClienteID", SqlDbType.VarChar, 0, ClienteID),
                        DBHelper.MakeParam("@VendedorID", SqlDbType.VarChar, 0, VendedorID),
                        DBHelper.MakeParam("@Observaciones", SqlDbType.VarChar, 0, Observaciones),
                        DBHelper.MakeParam("@Condicion", SqlDbType.VarChar, 0, Condicion),
                        DBHelper.MakeParam("@MonedaTipo", SqlDbType.Int, 0, MonedaTipo)
                    };
            return DBHelper.ExecuteDataSet("usp_MAT_Reserva_NuevaReserva", dbParams); 
        }

        public static decimal GetSaldoFactura(Guid FacturaID)
        {
            List<ReservaStandard> Model = new List<ReservaStandard>();
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@FacturaID", SqlDbType.UniqueIdentifier, 0, FacturaID)
                    };
            decimal d = Convert.ToDecimal(DBHelper.ExecuteScalar("usp_MAT_SaldoFactura", dbParams));
            return d;
        }

        public static void CambioButacas(string AdicionalesIDs, Guid OldPasaje, Guid NewPasaje)
        {
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@AdicionalesIDs", SqlDbType.VarChar, 0, AdicionalesIDs),
                        DBHelper.MakeParam("@OldPasaje", SqlDbType.UniqueIdentifier, 0, OldPasaje),
                        DBHelper.MakeParam("@NewPasaje", SqlDbType.UniqueIdentifier, 0, NewPasaje)
                    };
            DBHelper.ExecuteNonQuery("dbo.usp_MAT_Reserva_CambioButacas", dbParams);

        }

        public static DataSet GetSenasByViajeID(Guid ViajeID)
        {
            List<ReservaStandard> Model = new List<ReservaStandard>();
            SqlParameter[] dbParams = new SqlParameter[]
                    {                    
                        DBHelper.MakeParam("@ViajeID", SqlDbType.UniqueIdentifier, 0, ViajeID)
                    };
            return DBHelper.ExecuteDataSet("dbo.usp_Reserva_GetSenasByViajeID", dbParams);
        }

    }
}