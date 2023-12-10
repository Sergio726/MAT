using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.Entities;
using MAT.Services;
using MAT.Utilities;
using MAT.Enums;
using MAT.MVC.Common;

namespace MAT.MVC.Models
{
    public class RegistroPagoModel
    {
        public Factura Factura { get; set; }
        public Entities.Cliente Cliente { get; set; }
        public Cuenta Cuenta { get; set; }
        public Vendedor Vendedor { get; set; }
        public Pago Pago { get; set; }
        public eFormaPago FormaPago { get; set; }
                       

        public void ActualizarEstados()
        {
            ActualizarEstados(String.Empty);
        }

        public void ActualizarEstados(string nrofactura)
        {
            double _saldo = MATContext.Saldo(Factura.FacturaId);
            PasajeService pasajeService = new PasajeService();
            List<Pasaje> _pasajes = pasajeService.GetByFacturaId(Factura.FacturaId).ToList();
            if (_saldo <= 0)
            {
                Factura.Estado = (int)eEstadoFactura.Pagado;
                if (!String.IsNullOrEmpty(nrofactura)) Factura.NroFactura = nrofactura;
                foreach (Pasaje pasaje in _pasajes)
                {
                    //- Generación de Vouchers
                    VoucherService voucherService = new VoucherService();
                    Voucher voucher = new Voucher()
                    {
                        VoucherId = Guid.NewGuid(),
                        FechaEmision = DateTime.Now
                    };
                    voucherService.Insert(voucher);
                    //-

                    pasaje.VoucherId = voucher.VoucherId;
                    switch (pasaje.EstadoPasaje)
                    {
                        case (int)eEstadoPasaje.Señado:
                            pasaje.EstadoPasaje = (int)eEstadoPasaje.Pagado;
                            break;
                        case (int)eEstadoPasaje.PasajeHotelPrereserva:
                            pasaje.EstadoPasaje = (int)eEstadoPasaje.ReservaHotel;
                            break;
                        case (int)eEstadoPasaje.Prereserva:
                            pasaje.EstadoPasaje = (int)eEstadoPasaje.Pagado;
                            break;
                        default:
                            pasaje.EstadoPasaje = (int)eEstadoPasaje.Pagado;
                            break;
                    }
                    
                    pasajeService.Update(pasaje);
                }

            }
            else if (Factura.Estado == (int)eEstadoFactura.Prereserva)
            {
                Factura.Estado = (int)eEstadoFactura.Señado;

                foreach (Pasaje pasaje in _pasajes)
                {
                    pasaje.EstadoPasaje = (int)eEstadoPasaje.Señado;
                    pasajeService.Update(pasaje);
                }
            }
            FacturaService facturaService = new FacturaService();
            facturaService.Save(Factura);
        }

        private bool EsPagoTotal()
        {
            return false;
        }
    }
}