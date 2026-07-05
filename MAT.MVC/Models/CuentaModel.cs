using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.Entities;

namespace MAT.MVC.Models
{
    public class CuentaModel
    {
        //private ClienteService clienteService;
        ////private CuentaCorrienteService ccService;
        //private CuentaService cuentaService;
        //private PagoService pagoService;
        //private FacturaService facturaService;
        //private MovimientoCuentaService movimientoService;
        //private NotaService notaService;
        //private VendedorService vendedorService;
        //private PersonaService personaService;

        //public MAT.Entities.Cliente Cliente { get; set; }
        //public MAT.Entities.CuentaCorriente CC { get; set; }
        public Cuenta Cuenta { get; set; }
        public List<Pago> Pagos { get; set; }
        //public List<Factura> Facturas { get; set; }
        //public List<Nota> Notas { get; set; }
        public List<Resumen> ResumenCuenta { get; set; }
        //public List<MovimientoCuenta> Movimientos { get; set; }
        public Vendedor Vendedor { get; set; }
        public Persona Persona { get; set; }

        public double TotalDebe { get; set; }
        public double TotalHaber { get; set; }
        public double Unificado { get; set; }
        //public CuentaModel()
        //{
        //    clienteService = new ClienteService();
        //    ccService = new CuentaCorrienteService();
        //    pagoService = new PagoService();
        //    facturaService = new FacturaService();
        //    movimientoService = new MovimientoCuentaService();
        //    notaService = new NotaService();
        //}

        //public CuentaModel(Guid clienteid)
        //{
        //    clienteService = new ClienteService();
        //    ccService = new CuentaCorrienteService();
        //    pagoService = new PagoService();
        //    facturaService = new FacturaService();
        //    movimientoService = new MovimientoCuentaService();
        //    notaService = new NotaService();
        //    cuentaService = new CuentaService();
        //    vendedorService = new VendedorService();
        //    personaService = new PersonaService();

        //    Cliente = clienteService.GetByClienteId(clienteid);
        //    Persona = personaService.GetByPersonaId(clienteid);
        //    Cuenta = cuentaService.GetByClienteId(clienteid).FirstOrDefault();
        //    CC = ccService.GetByClienteId(clienteid).FirstOrDefault();
        //    Facturas = facturaService.GetByClienteId(clienteid).ToList();
        //    Pagos = pagoService.GetByClienteId(clienteid).ToList();
        //    Notas = notaService.GetByClienteId(clienteid).ToList();
        //    Movimientos = movimientoService.GetByCuentaId(Cuenta.CuentaId).ToList();
        //}

        //public void CrearResumen()
        //{
        //    List<Resumen> _resumencuenta = new List<Resumen>();
        //    double unificado = 0;
        //    double _totaldebe = 0;
        //    double _totalhaber = 0;
        //    foreach (MovimientoCuenta item in Movimientos.OrderByDescending(mov => mov.FechaRegistro)) //!(mov.CuentaCorrienteId.HasValue && !mov.PagoId.HasValue)
        //    {               
        //        Persona _vendedor;
        //        Resumen resumen = new Resumen()
        //        {
        //         Fecha = item.FechaRegistro.Value,
        //         Comprobante = item.MovimientoId.ToString()
        //        };
        //        Resumen compensacion = new Resumen()
        //        {
        //            Fecha = item.FechaRegistro.Value,
        //            Comprobante = item.MovimientoId.ToString()
        //        };
        //        if (!item.PagoId.HasValue && !item.NotaId.HasValue && !item.DebitoId.HasValue && !item.CuentaCorrienteId.HasValue)
        //        {
        //            Factura _factura = facturaService.GetByFacturaId(item.FacturaId);
        //            resumen.Debe = _factura.Monto.Value;
        //            _vendedor = personaService.GetByPersonaId(_factura.VendedorId);
        //            resumen.Vendedor = string.Format("{0} {1}", _vendedor.Nombre, _vendedor.Apellido);
        //            _totaldebe += resumen.Debe;
        //            resumen.Haber = 0;
        //            resumen.Descripcion = "Factura";
        //            unificado = unificado - resumen.Debe;
        //        }
        //        else if (item.PagoId.HasValue)
        //        {
        //            Pago _pago = pagoService.GetByPagoId(item.PagoId.Value);
        //            resumen.Haber = _pago.Monto.Value;
        //            resumen.TipoPago = _pago.TipoPago;
        //            _vendedor = personaService.GetByPersonaId(_pago.VendedorId.Value);
        //            resumen.Vendedor = string.Format("{0} {1}", _vendedor.Nombre, _vendedor.Apellido);
        //            _totalhaber += resumen.Haber;
        //            resumen.Debe = 0;
        //            resumen.Descripcion = "Pago";
        //            unificado = unificado + resumen.Haber;
        //        }
        //        else if (item.NotaId.HasValue)
        //        {
        //            Nota _nota = notaService.GetByNotaId(item.NotaId.Value);
        //            NotaCreditoModel notamodel = new NotaCreditoModel(Cliente.ClienteId, item.FacturaId);
        //            _vendedor = personaService.GetByPersonaId(_nota.VendedorId.Value);
        //            if (notamodel.Factura.Monto.Value > notamodel.PagoTotal)
        //            {
        //                compensacion.Haber = notamodel.Factura.Monto.Value - notamodel.PagoTotal;
        //                compensacion.Debe = 0;
        //                compensacion.Descripcion = "Compensación Nota";
        //                compensacion.Vendedor = string.Format("{0} {1}", _vendedor.Nombre, _vendedor.Apellido);
        //                _totalhaber += compensacion.Haber;
        //                //_resumencuenta.Add(compensacion);
        //            }
        //            resumen.Haber = _nota.MontoNota.Value;                    
        //            resumen.Vendedor = string.Format("{0} {1}", _vendedor.Nombre, _vendedor.Apellido);
        //            _totalhaber += resumen.Haber;
        //            resumen.Debe = 0;
        //            resumen.Descripcion = "Nota de Crédito";
        //            unificado = unificado + resumen.Haber + compensacion.Haber;
        //        }
        //        else if (item.DebitoId.HasValue)
        //        {
        //            Debito _debito = new DebitoService().GetByDebitoId(item.DebitoId.Value);
        //            resumen.Debe = _debito.MontoDebito.Value;
        //            if (_debito.VendedorId.HasValue)
        //            {
        //                _vendedor = personaService.GetByPersonaId(_debito.VendedorId.Value);
        //                resumen.Vendedor = string.Format("{0} {1}", _vendedor.Nombre, _vendedor.Apellido);
        //            } 
        //            _totaldebe += resumen.Debe;
        //            resumen.Haber = 0;
        //            resumen.Descripcion = "Utilización de Crédito";
        //            unificado = unificado + resumen.Debe;
        //        }
        //        resumen.Unificado = unificado;
        //        if (!string.IsNullOrEmpty(resumen.Descripcion)) _resumencuenta.Add(resumen);
        //        if (item.NotaId.HasValue && !string.IsNullOrEmpty(compensacion.Descripcion)) _resumencuenta.Add(compensacion);
        //    }
        //    TotalDebe = _totaldebe;
        //    TotalHaber = _totalhaber;
        //    Unificado = _totalhaber - _totaldebe;
        //    ResumenCuenta = _resumencuenta;
        //}
    }

    public class Resumen
    {
        public DateTime Fecha { get; set; }
        public string Comprobante { get; set; }
        public string Descripcion { get; set; }
        public double Debe { get; set; }
        public double Haber { get; set; }
        public double Unificado { get; set; }
        public string Vendedor { get; set; }
        public int? TipoPago { get; set; }
    }

    public class Comprobante
    {
        public Pago Pago { get; set; }
        //public Factura Factura { get; set; }
        //public Nota Nota { get; set; }
        //public MovimientoCuenta Movimiento { get; set; }
        public String TipoComprobante { get; set; }
        public string Vendedor { get; set; }
        //public Comprobante(Guid movimientoid)
        //{
        //    MovimientoCuentaService movimientoService = new MovimientoCuentaService();
        //    PersonaService personaService = new PersonaService();
        //    Persona _vendedor;
        //    Movimiento = movimientoService.GetByMovimientoId(movimientoid);
        //    if (Movimiento.PagoId.HasValue)
        //    {
        //        Pago = new PagoService().GetByPagoId(Movimiento.PagoId.Value);
        //        _vendedor = personaService.GetByPersonaId(Pago.VendedorId.Value);
        //        Vendedor = string.Format("{0} {1}", _vendedor.Nombre, _vendedor.Apellido);
        //        TipoComprobante = "Recibo";
        //    } 
        //    if (Movimiento.FacturaId != null && !Movimiento.PagoId.HasValue){ 
        //        Factura = new FacturaService().GetByFacturaId(Movimiento.FacturaId);
        //        _vendedor = personaService.GetByPersonaId(Factura.VendedorId);
        //        Vendedor = string.Format("{0} {1}", _vendedor.Nombre, _vendedor.Apellido);
        //        TipoComprobante = "Factura";
        //    }
        //    if (Movimiento.NotaId.HasValue)
        //    {
        //        Nota = new NotaService().GetByNotaId(Movimiento.NotaId.Value);
        //        _vendedor = personaService.GetByPersonaId(Nota.VendedorId.Value);
        //        Vendedor = string.Format("{0} {1}", _vendedor.Nombre, _vendedor.Apellido);
        //        TipoComprobante = "Nota de Crédito";
        //    }
            
            
        //}
    }

}