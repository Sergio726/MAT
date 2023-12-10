using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MAT.Enums;
namespace MAT.MVC.Models
{
    public class ResumenPlanillaModel
    {

        public string TipoHabitacion { get; set; }
        public int IntTipoHabitacion { get; set; }
        public int CantidadPersonas { get; set; }
        public int CantidadMenores { get; set; }
        public double Subtotal { get; set; }
        public int Dias { get; set; }
        //public ResumenPlanillaModel(List<PlanillaHotelModel> planilla, eTipoHabitacion tipo)
        public ResumenPlanillaModel(List<PlanillaHotelModel> planilla, int tipo)
        {
            List<PlanillaHotelModel> filtrado = planilla.Where(pl => pl.Habitacion.Tipo == (int)tipo).ToList();
            TipoHabitacion = tipo.ToString();
            IntTipoHabitacion = (int)tipo;
            int _cantidadpersonas = 0;
            int _cantidadmenores = 0;
            double _subtotal = 0;
            foreach (var item in filtrado)
            {
                _cantidadpersonas += item.Pasajeros.Count;
                _cantidadmenores += item.Menores;
                _subtotal += item.Subtotal;
            }
            CantidadPersonas = _cantidadpersonas;
            CantidadMenores = _cantidadmenores;
            Subtotal = _subtotal;
        }
    }
}