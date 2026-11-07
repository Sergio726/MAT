using System;

namespace MAT.MVC.Models.Home
{
    /// <summary>
    /// Tendencia de un indicador del inicio contra el mismo tramo del mes anterior
    /// (bloque E del plan del panel principal). Para los tres indicadores, subir es bueno.
    /// </summary>
    public sealed class TendenciaKpi
    {
        private static readonly string[] Meses = { "ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic" };

        /// <summary>"sube", "baja" o "igual" (sirve de modificador CSS).</summary>
        public string Direccion { get; private set; }

        /// <summary>Texto corto visible: "+12 %", "−8 %", "+3" (si antes había 0) o "Igual".</summary>
        public string Texto { get; private set; }

        /// <summary>Período comparado, por ejemplo "1–6 oct".</summary>
        public string Periodo { get; private set; }

        /// <summary>Frase completa para lectores de pantalla y tooltip.</summary>
        public string Lectura { get; private set; }

        public static TendenciaKpi Calcular(double actual, double anterior, string periodo)
        {
            var t = new TendenciaKpi { Periodo = periodo };
            if (anterior <= 0)
            {
                if (actual <= 0)
                {
                    t.Direccion = "igual";
                    t.Texto = "Igual";
                    t.Lectura = "Igual que " + periodo + " (sin movimiento)";
                }
                else
                {
                    // Sin base no hay porcentaje: se muestra la diferencia absoluta
                    t.Direccion = "sube";
                    t.Texto = "+" + actual.ToString("N0");
                    t.Lectura = "Sube de 0 a " + actual.ToString("N0") + " respecto de " + periodo;
                }
                return t;
            }

            var porcentaje = (int)Math.Round((actual - anterior) * 100.0 / anterior, MidpointRounding.AwayFromZero);
            if (porcentaje == 0)
            {
                t.Direccion = "igual";
                t.Texto = "Igual";
                t.Lectura = "Igual que " + periodo;
            }
            else if (porcentaje > 0)
            {
                t.Direccion = "sube";
                t.Texto = "+" + porcentaje + " %";
                t.Lectura = "Sube " + porcentaje + " % respecto de " + periodo;
            }
            else
            {
                t.Direccion = "baja";
                t.Texto = "−" + (-porcentaje) + " %";
                t.Lectura = "Baja " + (-porcentaje) + " % respecto de " + periodo;
            }
            return t;
        }

        /// <summary>
        /// Mismo tramo del mes anterior: del 1 al día de hoy. Si el mes anterior es más corto
        /// (31 de marzo → febrero), se corta en su último día.
        /// </summary>
        public static void PeriodoAnterior(DateTime hoy, out DateTime desde, out DateTime hasta)
        {
            desde = new DateTime(hoy.Year, hoy.Month, 1).AddMonths(-1);
            var dia = Math.Min(hoy.Day, DateTime.DaysInMonth(desde.Year, desde.Month));
            hasta = desde.AddDays(dia - 1);
        }

        /// <summary>"1–6 oct", o "1 oct" si el tramo es de un solo día.</summary>
        public static string EtiquetaPeriodo(DateTime desde, DateTime hasta)
        {
            var mes = Meses[desde.Month - 1];
            return hasta.Day == desde.Day ? desde.Day + " " + mes : desde.Day + "–" + hasta.Day + " " + mes;
        }
    }
}
