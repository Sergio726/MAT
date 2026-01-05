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
        public int? MonedaTipo { get; set; } // 1 = Pesos, 3 = Dólar


        public SeleccionarPasajeroModel(Guid paqueteid)
        {
            // Inicializar listas por defecto
            Precios = new List<Entities.Precio>();
            Adicionales = new List<Entities.Adicional>();
            MonedaTipo = 1; // Default a pesos si no se puede obtener

            try
            {
                precioService = new PrecioService();
                adicionalService = new AdicionalService();

                // Obtener información del Paquete para la moneda
                try
                {
                    PaqueteService paqueteService = new PaqueteService();
                    Entities.Paquete paquete = paqueteService.GetByPaqueteId(paqueteid);
                    if (paquete != null && paquete.Moneda.HasValue)
                    {
                        MonedaTipo = paquete.Moneda.Value;
                    }
                }
                catch
                {
                    // Si hay error obteniendo el paquete, usar default (pesos)
                    MonedaTipo = 1;
                }

                List<Entities.PaquetePrecio> _paqueteprecios = new PaquetePrecioService().GetByPaqueteId(paqueteid).ToList();
                List<Entities.PaqueteAdicional> _paqueteadicionales = new PaqueteAdicionalService().GetByPaqueteId(paqueteid).ToList();
                List<Entities.Precio> _precios = new List<Precio>();
                foreach (var item in _paqueteprecios)
                {
                    if (item.PrecioId.HasValue)
                    {
                        var precio = precioService.GetByPrecioId(item.PrecioId.Value);
                        if (precio != null)
                        {
                            _precios.Add(precio);
                        }
                    }
                }
                Precios = _precios;
                List<Entities.Adicional> _adicionales = new List<Adicional>();
                foreach (var item in _paqueteadicionales)
                {
                    if (item.AdicionalId.HasValue)
                    {
                        var adicional = adicionalService.GetByAdicionalId(item.AdicionalId.Value);
                        if (adicional != null)
                        {
                            _adicionales.Add(adicional);
                        }
                    }
                }
                Adicionales = _adicionales;
            }
            catch
            {
                // Si hay cualquier error, mantener las listas vacías pero inicializadas
                Precios = Precios ?? new List<Entities.Precio>();
                Adicionales = Adicionales ?? new List<Entities.Adicional>();
            }
        }
    }
}