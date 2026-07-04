using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.Entities;
using MAT.Services;
using MAT.Enums;
using MAT.MVC.Infrastructure.Data;

namespace MAT.MVC.Models
{
    public class PasajeModel
    {
        public Pasaje Pasaje { get; set; }
        public Pasajero Pasajero { get; set; }
        public PersonaPasajero PersonaPasajero { get; set; }
        public Butaca Butaca { get; set; }
        public Transporte Bus { get; set; }
        public MAT.Entities.Habitacion Habitacion { get; set; }
        public Hotel Hotel { get; set; }
        public eEstadoPasaje Estado { get; set; }
        public PaqueteModel Paquete { get; set; }
        public double Precio { get; set; }
        public string PrecioID { get; set; }
        public double Adicionales { get; set; }
        public string AdicionalesID { get; set; }
        public PasajeModel() {
        }

        public PasajeModel(Guid pasajeid)
        {
            Pasaje = PasajeDataAccess.GetById(pasajeid);
            Estado = (eEstadoPasaje)Pasaje.EstadoPasaje;
            List<Entities.ReservaHabitacion> reservasPasaje = ReservaHabitacionDataAccess.GetByPasajeId(pasajeid);
            if (reservasPasaje.Count > 0)
            {
                List<Entities.ReservaHabitacion> reservas = reservasPasaje.Where(re => re.ViajeId.Value == Pasaje.ViajeId.Value).ToList();
                Habitacion = MaestrosDataAccess.GetHabitacionById(reservas.FirstOrDefault().HabitacionId.Value);
            }
            if (Pasaje.PasajeroId.HasValue)
            {
                Pasajero = PasajeroDataAccess.GetById(Pasaje.PasajeroId.Value);
                PersonaPasajero = PersonaPasajeroDataAccess.GetByPersonaId(Pasaje.PasajeroId.Value);
            }
            if (Pasaje.ButacaId.HasValue) Butaca = MaestrosDataAccess.GetButacaById(Pasaje.ButacaId.Value);            
            if (Butaca != null && Butaca.TransporteId.HasValue) Bus = MaestrosDataAccess.GetTransporteById(Butaca.TransporteId.Value);            
            //if (Pasaje.HabitacionId.HasValue) Habitacion = hServ.GetByHabitacionId(Pasaje.HabitacionId.Value);            
            //if (Pasaje.HabitacionId.HasValue && Habitacion.HotelId.HasValue) Hotel = hotelServ.GetByHotelId(Habitacion.HotelId.Value);
            if (Pasaje.ViajeId.HasValue) Paquete = new PaqueteModel(Pasaje.ViajeId.Value);
        }

        public void ReservarPasaje(Guid pasajeroid)
        {
            Pasaje pasaje = PasajeDataAccess.GetById(Pasaje.PasajeId);
            pasaje.PasajeroId = pasajeroid;
            PasajeDataAccess.Update(pasaje);
            Estado = eEstadoPasaje.Reservado;
        }

    }

}