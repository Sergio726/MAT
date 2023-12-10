using MAT.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MAT.MVC.Models
{
    public class ResumenPlanillaPrintModel
    {
        public string TipoHabitacion { get; set; }
        public int IntTipoHabitacion { get; set; }
        public int CantidadPersonas { get; set; }
        public int CantidadMenores { get; set; }
        public double Subtotal { get; set; }
        public int Dias { get; set; }
        public string HotelName { get; set; }
        //public ResumenPlanillaPrintModel(List<PlanillaHotelPrintModel> planilla, eTipoHabitacion tipo)
        public ResumenPlanillaPrintModel(List<PlanillaHotelPrintModel> planilla, int tipo)
        {
            List<PlanillaHotelPrintModel> filtrado = planilla.Where(pl => pl.Habitacion.Tipo == (int)tipo).ToList();
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
            Dias = filtrado.Count > 0 ? filtrado.FirstOrDefault().Dias : 0;
            HotelName = filtrado.Count > 0 ? filtrado.FirstOrDefault().Hotel : "";
            Subtotal = _subtotal;
        }
    }
}