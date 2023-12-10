using MAT.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.MVC.Models;

namespace MAT.MVC.Models
{
    public class PlanillaHotelPrintModel
    {
        public Entities.Habitacion Habitacion { get; set; }
        public Entities.PlanillaHabitacionItem HabitacionItem { get; set; }
        public Entities.PrecioHabitacion PrecioHabitacion { get; set; }
        public double PrecioMenor { get; set; }
        public string TipoHabitacion { get; set; }
        public List<Entities.Persona> Pasajeros { get; set; }
        public string Hotel { get; set; }
        public double Subtotal { get; set; }
        public int Menores { get; set; }
        public int Dias { get; set; }

        public PlanillaHotelPrintModel(Guid habitacionid, Guid planillaid, string hotel, Guid viajeid)
        {
            Services.PlanillaHabitacionItemService planillahabitacionitemService = new Services.PlanillaHabitacionItemService();
            Entities.PlanillaHabitacionItem habitacionitem = planillahabitacionitemService.GetByPlanillaId(planillaid).Where(hi => hi.HabitacionId == habitacionid).FirstOrDefault();
            Subtotal = habitacionitem != null ? habitacionitem.Subtotal : 0;
            Dias = habitacionitem != null ? habitacionitem.Cantidad : 0;
            HabitacionItem = habitacionitem != null ? habitacionitem : null;
            int _menores = 0;
            Hotel = hotel;
            Habitacion = new Services.HabitacionService().GetByHabitacionId(habitacionid);
//            TipoHabitacion = ((eTipoHabitacion)Habitacion.Tipo.Value).ToString();
            TipoHabitacion = HabitacionTipoMethod.GetAllHabitacionTipoByTipoId(Habitacion.Tipo).Descripcion;          
            List<Entities.ReservaHabitacion> _reservas = new Services.ReservaHabitacionService().GetByHabitacionId(habitacionid).Where(re => re.ViajeId == viajeid).ToList();
            List<Entities.Persona> _pasajeros = new List<Entities.Persona>();
            foreach (var item in _reservas)
            {
                Entities.Persona pasajero = new Services.PersonaService().GetByPersonaId(item.PasajeroId.Value);
                if (EsMenor(pasajero)) _menores += 1;
                _pasajeros.Add(pasajero);
            }
            Menores = _menores;
            Pasajeros = _pasajeros;
            //PrecioHabitacion = new Services.PrecioHabitacionService().GetAll().Where(ph => ph.HotelId == Habitacion.HotelId && ph.TipoHabitacion == Habitacion.Tipo && ph.Activo).FirstOrDefault();
            PrecioHabitacion = new Services.PrecioHabitacionService().GetAll().Where(ph => ph.HotelId == Habitacion.HotelId && ph.TipoHabitacion == Habitacion.Tipo && ph.Activo).FirstOrDefault();
            PrecioMenor = new Services.PrecioHabitacionService().GetAll().Where(ph => ph.HotelId == Habitacion.HotelId && ph.TipoHabitacion == 0 && ph.Activo).FirstOrDefault().Precio;
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