using MAT.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security;
using WebMatrix.WebData;
using MAT.Services;
using MAT.Entities;
using MAT.MVC.Filters;

namespace MAT.MVC.Common
{

    [InitializeSimpleMembership]
    public class MATContext
    {
        private static Entities.Planilla _planilla;
        private static List<Entities.Planilla> _coleccionplanillas;
        private static List<Entities.PlanillaServicioItem> _serviciosseleccionados;
        private static List<Entities.PlanillaHabitacionItem> _habitacionesplanilla;
        private static ReservaModel _reserva;
        private static RegistroPagoModel _registropago;
        private static RegistroPagoModel _registropagototal;
        private static int _currentuserid;
        public static NotaCreditoModel Nota { get; set; }
        public static ReservaModel Reserva
        {
            get
            {
                return _reserva;
            }
            set
            {
                _reserva = value;
            }
        }
        public static RegistroPagoModel RegistroPago
        {
            get
            {
                return _registropago;
            }
            set
            {
                _registropago = value;
            }
        }
        public static RegistroPagoModel RegistroPagoTotal
        {
            get
            {
                return _registropagototal;
            }
            set
            {
                _registropagototal = value;
            }
        }

        public static List<Entities.PlanillaServicioItem>  ServiciosSeleccionados
        {
            get
            {
                return _serviciosseleccionados;
            }
            set
            {
                _serviciosseleccionados = value;
            }
        }

        public static List<Entities.PlanillaHabitacionItem> HabitacionesPlanilla
        {
            get
            {
                return _habitacionesplanilla;
            }
            set
            {
                _habitacionesplanilla = value;
            }
        }



        public static List<Entities.Planilla> ColeccionPlanillas
        {
            get { return _coleccionplanillas; }
            set { _coleccionplanillas = value; }
        }


        public static Entities.Planilla Planilla
        {
            get { return _planilla; }
            set { _planilla = value; }
        }
        

        public static double Saldo(Guid facturaid)
        {
            Factura _factura = new FacturaService().GetByFacturaId(facturaid);
            double montofactura = _factura.Monto.Value;
            var _pagos = new MovimientoCuentaService().GetByFacturaId(_factura.FacturaId).Where(p => p.PagoId.HasValue).ToList();
            double totalpagos = 0;
            foreach (var item in _pagos)
            {
                Pago pago = new PagoService().GetByPagoId(item.PagoId.Value);
                totalpagos += pago.Monto.Value;
            }
            return montofactura - totalpagos;
        }
        
       
        public static Vendedor CurrentVendedor
        {
            get
            {
                PersonaService personaService = new PersonaService();
                VendedorService vendedorService = new VendedorService();
                Persona currentpersona;
                int userId = 0; 
              
                if (WebSecurity.HasUserId)
                {
                    userId = WebSecurity.CurrentUserId;
                    
                }
                else
                {
                    userId = MATContext.CurrentUserId;
                }
                currentpersona = personaService.GetAll().Where(p => p.UserId == userId).FirstOrDefault();
                if (currentpersona == null)
                    return null;

                return vendedorService.GetByVendedorId(currentpersona.PersonaId);
            }
        }

        public static int CurrentUserId
        {
            get
            {
                return _currentuserid;
            }
            set
            {
                _currentuserid = value;
            }
        }
    }
}