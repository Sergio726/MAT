using MAT.Entities;
using MAT.MVC.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class SeleccionarPasajeroModel
    {
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
                // Obtener información del Paquete para la moneda
                try
                {
                    Entities.Paquete paquete = PaqueteDataAccess.GetPaqueteById(paqueteid);
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

                List<Entities.PaquetePrecio> _paqueteprecios = PaqueteDataAccess.GetPaquetePreciosByPaqueteId(paqueteid);
                List<Entities.PaqueteAdicional> _paqueteadicionales = PaqueteDataAccess.GetPaqueteAdicionalesByPaqueteId(paqueteid);
                List<Entities.Precio> _precios = new List<Precio>();
                foreach (var item in _paqueteprecios)
                {
                    if (item.PrecioId.HasValue)
                    {
                        var precio = PaqueteDataAccess.GetPrecioById(item.PrecioId.Value);
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
                        var adicional = MaestrosDataAccess.GetAdicionalById(item.AdicionalId.Value);
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
