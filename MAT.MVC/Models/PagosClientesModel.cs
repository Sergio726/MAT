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

        private ClienteService clienteService;
        private PersonaClienteService personaclienteService;
        private PasajeroService pasajeroService;

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
            clienteService = new ClienteService();
            pasajeroService = new PasajeroService();
        }

        public FacturaPagosModel(Guid facturaid)
        {
            clienteService = new ClienteService();
            pasajeroService = new PasajeroService();
            personaclienteService = new PersonaClienteService();
            Factura = Infrastructure.Data.FacturaDataAccess.GetById(facturaid);
            Cliente = personaclienteService.GetAll().Where(pc => pc.ClienteId == Factura.ClienteId).FirstOrDefault();
            Saldo = CalcularSaldo();
            List<Pasaje> _pasajes = Infrastructure.Data.PasajeDataAccess.GetByFacturaId(facturaid);
            Viaje = Infrastructure.Data.ViajeDataAccess.GetById(_pasajes.FirstOrDefault().ViajeId.Value);
            Paquete = Infrastructure.Data.PaqueteDataAccess.GetPaqueteById(Viaje.PaqueteId.Value);
            List<PasajeModel> _pasajesmodel = new List<PasajeModel>();
            foreach (var item in _pasajes)
            {
                _pasajesmodel.Add(new PasajeModel(item.PasajeId));
            }
            Pasajes = _pasajesmodel;
        }

        private double CalcularSaldo()
        {
            // NetTiers F6: los pagos llegan en una sola consulta (sin N+1) y los
            // débitos se totalizan en el SP de saldo.
            double montofactura = Factura.Monto.Value;
            List<Entities.Pago> _pagosEntities = Infrastructure.Data.PagoDataAccess.GetPagosByFacturaId(Factura.FacturaId);
            double totalpagos = 0;
            foreach (var pago in _pagosEntities)
            {
                totalpagos += pago.Monto.Value;
            }
            Pagos = _pagosEntities;

            var info = Infrastructure.Data.FacturaDataAccess.GetSaldoInfo(Factura.FacturaId);
            double totaldebitos = info.TotalDebitos;

            return montofactura - totalpagos - totaldebitos;
        }
    }
}