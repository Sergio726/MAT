using MAT.Entities;
using MAT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class FacturaPagosModel
    {

        private FacturaService facturaService;
        private PasajeService pasajeService;
        private ClienteService clienteService;
        private PersonaClienteService personaclienteService;
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
        public List<Entities.Pago> Pagos { get; set; }
        public FacturaPagosModel()
        {
            facturaService = new FacturaService();
            pasajeService = new PasajeService();
            clienteService = new ClienteService();
            pasajeroService = new PasajeroService();
            paqueteService = new PaqueteService();
            viajeService = new ViajeService();
        }

        public FacturaPagosModel(Guid facturaid)
        {
            facturaService = new FacturaService();
            pasajeService = new PasajeService();
            clienteService = new ClienteService();
            pasajeroService = new PasajeroService();
            paqueteService = new PaqueteService();
            viajeService = new ViajeService();
            personaclienteService = new PersonaClienteService();
            Factura = facturaService.GetByFacturaId(facturaid);
            Cliente = personaclienteService.GetAll().Where(pc => pc.ClienteId == Factura.ClienteId).FirstOrDefault();
            Saldo = CalcularSaldo();
            List<Pasaje> _pasajes = pasajeService.GetByFacturaId(facturaid).ToList();
            Viaje = viajeService.GetByViajeId(_pasajes.FirstOrDefault().ViajeId.Value);
            Paquete = paqueteService.GetByPaqueteId(Viaje.PaqueteId.Value);
            List<PasajeModel> _pasajesmodel = new List<PasajeModel>();
            foreach (var item in _pasajes)
            {
                _pasajesmodel.Add(new PasajeModel(item.PasajeId));
            }
            Pasajes = _pasajesmodel;
        }

        private double CalcularSaldo()
        {
            double montofactura = Factura.Monto.Value;
            var _pagos = new MovimientoCuentaService().GetByFacturaId(Factura.FacturaId).Where(p => p.PagoId.HasValue).ToList();
            List<Entities.Pago> _pagosEntities = new List<Pago>();
            double totalpagos = 0;
            foreach (var item in _pagos)
            {
                Pago pago = new PagoService().GetByPagoId(item.PagoId.Value);
                _pagosEntities.Add(pago);
                totalpagos += pago.Monto.Value;
            }
            Pagos = _pagosEntities;
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
}