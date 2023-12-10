using MAT.Entities;
using MAT.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class SeleccionarPasajeroModel
    {
        private PrecioService precioService;
        private AdicionalService adicionalService;

        public List<Entities.Precio> Precios { get; set; }
        public List<Entities.Adicional> Adicionales { get; set; }


        public SeleccionarPasajeroModel(Guid paqueteid)
        {
            precioService = new PrecioService();
            adicionalService = new AdicionalService();

            List<Entities.PaquetePrecio> _paqueteprecios = new PaquetePrecioService().GetByPaqueteId(paqueteid).ToList();
            List<Entities.PaqueteAdicional> _paqueteadicionales = new PaqueteAdicionalService().GetByPaqueteId(paqueteid).ToList();
            List<Entities.Precio> _precios = new List<Precio>();
            foreach (var item in _paqueteprecios)
            {
                _precios.Add(precioService.GetByPrecioId(item.PrecioId.Value));
            }
            Precios = _precios;
            List<Entities.Adicional> _adicionales = new List<Adicional>();
            foreach (var item in _paqueteadicionales)
            {
                _adicionales.Add(adicionalService.GetByAdicionalId(item.AdicionalId.Value));
            }
            Adicionales = _adicionales;
        }
    }
}