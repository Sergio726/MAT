using MAT.Services;
using MAT.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MAT.MVC.Models
{
    public class VoucherModel
    {
        private PasajeService pasajeService;
        private PersonaPasajeroService pasajeroService;
        private FacturaService facturaService;
        private ServicioService servicioService;
        private ExcursionService excursionService;
        private PaqueteServicioService paqueteservicioService;
        private PaqueteExcursionService paqueteexcursionService;
        private PaqueteService paqueteService;
        private ViajeService viajeService;
        private VoucherService voucherService;

        public String Servicios { get; set; }
        public String Excursiones { get; set; }
        public Entities.PersonaPasajero Pasajero { get; set; }
        public Entities.Voucher Voucher { get; set; }
        public Entities.Paquete Paquete { get; set; }
        public Entities.Pasaje Pasaje { get; set; }
        public Entities.Viaje Viaje { get; set; }
        public Entities.Localidad Localidad { get; set; }
        public Entities.Localidad Destino { get; set; }
        public Entities.Butaca Butaca { get; set; }
        public List<ReservaVoucherModel>  Reserva { get; set; }
        public VoucherModel(Guid pasajeid)
        {
            pasajeService = new PasajeService();
            pasajeroService = new PersonaPasajeroService();
            facturaService = new FacturaService();
            servicioService = new ServicioService();
            excursionService = new ExcursionService();
            paqueteservicioService = new PaqueteServicioService();
            paqueteexcursionService = new PaqueteExcursionService();
            paqueteService = new PaqueteService();
            viajeService = new ViajeService();
            voucherService = new VoucherService();

            Pasaje = pasajeService.GetByPasajeId(pasajeid);
            Butaca = new ButacaService().GetByButacaId(Pasaje.ButacaId.Value);
            Pasajero = pasajeroService.GetAll().Where(psj => psj.PasajeroId == Pasaje.PasajeroId.Value).FirstOrDefault();
            Viaje = viajeService.GetByViajeId(Pasaje.ViajeId.Value);
            Paquete = paqueteService.GetByPaqueteId(Viaje.PaqueteId.Value);
            Destino = GeoDataAccess.GetLocalidadById(Paquete.DestinoId);
            List<Entities.PaqueteServicio> _serviciospaquete = paqueteservicioService.GetByPaqueteId(Paquete.PaqueteId).ToList();
            StringBuilder _servicios = new StringBuilder();
            foreach (var item in _serviciospaquete)
            {
                _servicios.Append(servicioService.GetByServicioId(item.ServicioId.Value).Descripcion).Append(", ");
            }
            Servicios = _servicios.ToString();

            List<Entities.PaqueteExcursion> _excursionesPaquete = paqueteexcursionService.GetByPaqueteId(Paquete.PaqueteId).ToList();
            StringBuilder _excursiones = new StringBuilder();
            foreach (var item in _excursionesPaquete)
            {
                _excursiones.Append(excursionService.GetByExcursionId(item.ExcursionId).Descripcion).Append(", ");
            }
            Excursiones = _excursiones.ToString();

            //Reserva = new ReservaVoucherModel(pasajeid);
            Reserva = GetReservas.GetListReservaHabitacion(pasajeid);

            
            Voucher = voucherService.GetByVoucherId(Pasaje.VoucherId.Value);
            if (Pasajero != null && Pasajero.LocalidadId.HasValue) Localidad = GeoDataAccess.GetLocalidadById(Pasajero.LocalidadId.Value);
        }
    }

    public class GetReservas
    {
        public static List<ReservaVoucherModel> GetListReservaHabitacion(Guid pasajeid)
        {
            List<ReservaVoucherModel> ListaReserva = new List<ReservaVoucherModel>();

            Services.PasajeService pasajeService = new PasajeService();
            Entities.Pasaje pasaje = pasajeService.GetByPasajeId(pasajeid);
            Services.ReservaHabitacionService reservaService = new ReservaHabitacionService();
            List<Entities.ReservaHabitacion> reserva = reservaService.GetByPasajeId(pasajeid).OrderBy(L =>L.Desde).ToList();

            foreach (var item in reserva)
            {

                ReservaVoucherModel Reserva = new ReservaVoucherModel();

                Entities.Habitacion habitacion = item.HabitacionId.HasValue ? new HabitacionService().GetByHabitacionId(item.HabitacionId.Value) : null;
                HotelService hotelService = new HotelService();
                Entities.Hotel hotel = hotelService.GetByHotelId(habitacion.HotelId.Value);
                Reserva.NombreHotel = hotel.Nombre;
                Reserva.Telefono = hotel.Telefono;
                Reserva.Direccion = hotel.Direccion;
                Reserva.NroHabitacion = habitacion.NroHabitacion.Value.ToString();
                Reserva.TipoHabitacion = ((MAT.Enums.eTipoHabitacion)(habitacion.Tipo.Value)).ToString();
                Reserva.DiasReserva = Reserva.GetFechasReservadas(habitacion.HabitacionId, pasaje.ViajeId.Value);

                ListaReserva.Add(Reserva);
            }

            return ListaReserva;
            
        }
    }
       

    public class ReservaVoucherModel
    {
        public string NombreHotel { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string ServiciosHotel { get; set; }
        public string NroHabitacion { get; set; }
        public string TipoHabitacion { get; set; }
        public string DiasReserva { get; set; }

        //public ReservaVoucherModel(Guid pasajeid)
        //{
        //    Services.PasajeService pasajeService = new PasajeService();
        //    Entities.Pasaje pasaje = pasajeService.GetByPasajeId(pasajeid);
        //    Services.ReservaHabitacionService reservaService = new ReservaHabitacionService();
        //    Entities.ReservaHabitacion reserva = reservaService.GetByPasajeId(pasajeid).FirstOrDefault();
        //    Entities.Habitacion habitacion = reserva.HabitacionId.HasValue ? new HabitacionService().GetByHabitacionId(reserva.HabitacionId.Value) : null;
        //    HotelService hotelService = new HotelService();
        //    Entities.Hotel hotel = hotelService.GetByHotelId(habitacion.HotelId.Value);
        //    NombreHotel = hotel.Nombre;
        //    Telefono = hotel.Telefono;
        //    Direccion = hotel.Direccion;
        //    NroHabitacion = habitacion.NroHabitacion.Value.ToString();
        //    TipoHabitacion = ((MAT.Enums.eTipoHabitacion)(habitacion.Tipo.Value)).ToString();
        //    DiasReserva = GetFechasReservadas(habitacion.HabitacionId, pasaje.ViajeId.Value);
        //}

        public string GetFechasReservadas(Guid habitacionid, Guid viajeid)
        {
            ReservaHabitacionService reservaService = new ReservaHabitacionService();
            List<Entities.ReservaHabitacion> reservas = reservaService.GetByHabitacionId(habitacionid).Where(rs => rs.ViajeId==viajeid).ToList();
            Entities.ReservaHabitacion reserva = reservaService.GetByHabitacionId(habitacionid).Where(rs => rs.ViajeId == viajeid).FirstOrDefault();
            StringBuilder arrayfechas = new StringBuilder();
            string mes = "";
            mes = string.Format("{0: 'de' MMMM}", reserva.Desde.Value);
            DateTime fecha_desde = reserva.Desde.Value;
            DateTime fecha_hasta = reserva.Hasta.Value;
            arrayfechas.Append(reserva.Desde.Value.Day);
            arrayfechas.Append(",");
            int rango = Convert.ToInt32((fecha_hasta - fecha_desde).TotalDays);
            for (int i = 0; i < rango; i++)
            {
                fecha_desde = fecha_desde.AddDays(1);
                arrayfechas.Append(fecha_desde.Day);
                arrayfechas.Append(",");
            }

            //foreach (var item in reservas)
            //{
            //    mes = string.Format("{0: 'de' MMMM}", item.Desde.Value);
            //    DateTime fecha_desde = item.Desde.Value;
            //    DateTime fecha_hasta = item.Hasta.Value;
            //    arrayfechas.Append(item.Desde.Value.Day);
            //    arrayfechas.Append(",");
            //    int rango = Convert.ToInt32((fecha_hasta - fecha_desde).TotalDays);
            //    for (int i = 0; i < rango; i++)
            //    {
            //        fecha_desde = fecha_desde.AddDays(1);
            //        arrayfechas.Append(fecha_desde.Day);
            //        arrayfechas.Append(",");
            //    }
            //    break;
            //}
            arrayfechas.Remove(arrayfechas.Length - 1, 1);
            arrayfechas.Append(" " + mes);
            return arrayfechas.ToString();
        }
    }
}