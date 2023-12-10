using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.Enums;
namespace MAT.MVC.Models
{
    public class HistorialPreciosModel
    {
        public List<Entities.PrecioHabitacion> Singles { get; set; }
        public List<Entities.PrecioHabitacion> Dobles { get; set; }
        public List<Entities.PrecioHabitacion> Matrimoniales { get; set; }
        public List<Entities.PrecioHabitacion> Triples { get; set; }
        public List<Entities.PrecioHabitacion> Cuadruples { get; set; }

        public HistorialPreciosModel(List<Entities.PrecioHabitacion> Precios)
        {
            Singles = Precios.Where(pr => pr.TipoHabitacion == 1).OrderBy(pre => pre.FechaRegistro).ToList();
            Dobles = Precios.Where(pr => pr.TipoHabitacion == 2).OrderBy(pre => pre.FechaRegistro).ToList();
            Matrimoniales = Precios.Where(pr => pr.TipoHabitacion == 3).OrderBy(pre => pre.FechaRegistro).ToList();
            Triples = Precios.Where(pr => pr.TipoHabitacion == 4).OrderBy(pre => pre.FechaRegistro).ToList();
            Cuadruples = Precios.Where(pr => pr.TipoHabitacion == 5).OrderBy(pre => pre.FechaRegistro).ToList();
        }
    }
}