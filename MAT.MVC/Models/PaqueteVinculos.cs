using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.Entities;
using MAT.Services;
namespace MAT.MVC.Models
{
    public class PaqueteVinculos
    {
        public Entities.Paquete Paquete { get; set; }
        public List<Entities.Servicio> Servicios { get; set; }
        public List<Entities.Excursion> Excursiones { get; set; }
        public List<Entities.Precio> Precios { get; set; }
        public List<Entities.Adicional> Adicionales { get; set; }

        private PaqueteService paqueteService;
        private ServicioService servicioService;
        private ExcursionService excursionService;
        private PrecioService precioService;
        private AdicionalService adicionalService;

        public PaqueteVinculos(Guid id)
        {
            paqueteService = new PaqueteService();
            servicioService = new ServicioService();
            excursionService = new ExcursionService();
            precioService = new PrecioService();
            adicionalService = new AdicionalService();

            Paquete = paqueteService.GetByPaqueteId(id);

            #region Servicios
            List<Entities.Servicio> _servicios = new List<Servicio>();
            List<Entities.PaqueteServicio> _paqueteservicios = new PaqueteServicioService().GetByPaqueteId(id).ToList();
            foreach (var ps in _paqueteservicios)
            {
                _servicios.Add(servicioService.GetByServicioId(ps.ServicioId.Value));
            }
            Servicios = _servicios;
            #endregion
            //#region Excursiones
            //List<Entities.Excursion> _excursiones = new List<Excursion>();
            //List<Entities.PaqueteExcursion> _paqueteexcursiones = new PaqueteExcursionService().GetByPaqueteId(id).ToList();
            //foreach (var pe in _paqueteexcursiones)
            //{
            //    _excursiones.Add(excursionService.GetByExcursionId(pe.ExcursionId));
            //}
            //Excursiones = _excursiones;
            //#endregion
            #region Precios
            List<Entities.Precio> _precios = new List<Precio>();
            List<Entities.PaquetePrecio> _paqueteprecios = new PaquetePrecioService().GetByPaqueteId(id).ToList();
            foreach (var pp in _paqueteprecios)
            {
                _precios.Add(precioService.GetByPrecioId(pp.PrecioId.Value));
            }
            Precios = _precios;
            #endregion
            #region Adicionales
            List<Entities.Adicional> _adicionales = new List<Adicional>();
            List<Entities.PaqueteAdicional> _paqueteadicionales = new PaqueteAdicionalService().GetByPaqueteId(id).ToList();
            foreach (var pa in _paqueteadicionales)
            {
                _adicionales.Add(adicionalService.GetByAdicionalId(pa.AdicionalId.Value));
            }
            Adicionales = _adicionales;
            #endregion
        }
    }
}