using MAT.MVC.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class PlanillaHotelModel
    {
        public Entities.Habitacion Habitacion { get; set; }
        public List<Entities.Persona> Pasajeros { get; set; }
        public string Hotel { get; set; }
        public double Subtotal { get; set; }
        public Entities.PrecioHabitacion PrecioHabitacion { get; set; }
        public double PrecioHabitacionMenor { get; set; }
        public int Menores { get; set; }
        public PlanillaHotelModel(Guid id, string hotel, Guid viajeid)
        {
            double _subtotal = 0;
            int _menores = 0;
            Hotel = hotel;
            Habitacion = MaestrosDataAccess.GetHabitacionById(id);
            PrecioHabitacion = PaqueteDataAccess.GetAllPrecioHabitaciones().Where(ph => ph.HotelId == Habitacion.HotelId.Value && ph.TipoHabitacion == Habitacion.Tipo && ph.Activo).FirstOrDefault();
            List<Entities.ReservaHabitacion> _reservas = ReservaHabitacionDataAccess.GetByHabitacionId(id).Where(re => re.ViajeId==viajeid).ToList();
            List<Entities.Persona> _pasajeros = new List<Entities.Persona>();
            foreach (var item in _reservas)
            {
                Entities.Persona pasajero = PersonaDataAccess.GetById(item.PasajeroId.Value);
                if (EsMenor(pasajero))
                {
                    double _preciomenor = PaqueteDataAccess.GetAllPrecioHabitaciones().Where(ph => ph.HotelId == Habitacion.HotelId.Value && ph.TipoHabitacion == 0 && ph.Activo).FirstOrDefault().Precio;
                    _subtotal += _preciomenor;
                    PrecioHabitacionMenor = _preciomenor;
                    _menores += 1;
                }
                else
                {
                    _subtotal += PrecioHabitacion.Precio;
                }
                _pasajeros.Add(pasajero);
            }
            Subtotal = _subtotal;
            Menores = _menores;
            Pasajeros = _pasajeros;
        }

        private bool EsMenor(Entities.Persona persona)
        {
            bool result = false;
            if ((DateTime.Now.Year - persona.FechaNacimiento.Value.Year) < 5)
            {
                result = true;
            }
            return result;
        }
    }
}