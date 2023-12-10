using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.Entities;
using MAT.Services;
using MAT.Enums;
namespace MAT.MVC.Models
{
    public class PasajeroHistorialModel
    {
        private PasajeroService pasajeroService;
        //private ViajeService viajeService;
        //private PaqueteService paqueteService;
        private PasajeService pasajeService;


        public Pasajero Pasajero { get; set; }
        public List<PasajeModel> HistorialPasajes { get; set; }

        public PasajeroHistorialModel(Guid pasajeroid)
        {
            pasajeroService = new PasajeroService();
            pasajeService = new PasajeService();
            Pasajero = pasajeroService.GetByPasajeroId(pasajeroid);
            List<Pasaje> _pasajes = pasajeService.GetByPasajeroId(pasajeroid).ToList();
            List<PasajeModel> _pmodel = new List<PasajeModel>();
            foreach (Pasaje pasaje in _pasajes)
            {
                _pmodel.Add(new PasajeModel(pasaje.PasajeId));
            }
            HistorialPasajes = _pmodel;
        }
    }
}