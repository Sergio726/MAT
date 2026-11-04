using System;
using System.Collections.Generic;
using System.Linq;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Reglas comunes del alta y la edición de cliente: fecha de nacimiento obligatoria y real,
    /// teléfono o celular con número completo. Antes solo se validaba en el navegador y la edición
    /// exigía el fijo, por eso se cargaba "387" para poder guardar.
    /// </summary>
    public static class ClienteValidacion
    {
        public const int MinimoDigitosTelefono = 7;
        public const int AnioMinimoNacimiento = 1910;

        private static readonly TimeZoneInfo ZonaArgentina = BuscarZonaArgentina();

        /// <summary>
        /// Fecha de hoy en Argentina (los servidores pueden estar en UTC).
        /// </summary>
        public static DateTime HoyArgentina()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ZonaArgentina).Date;
        }

        public static List<string> ValidarDatosContacto(DateTime? fechaNacimiento, string telefono, string celular, DateTime hoy)
        {
            var errores = new List<string>();

            if (!fechaNacimiento.HasValue)
                errores.Add("Ingrese la fecha de nacimiento (dd/mm/aaaa).");
            else if (fechaNacimiento.Value.Year < AnioMinimoNacimiento || fechaNacimiento.Value.Date > hoy)
                errores.Add("La fecha de nacimiento no es válida.");

            if (CantidadDigitos(telefono) < MinimoDigitosTelefono && CantidadDigitos(celular) < MinimoDigitosTelefono)
                errores.Add("Ingrese un teléfono o celular completo (no solo el código de área).");

            return errores;
        }

        private static int CantidadDigitos(string valor)
        {
            return string.IsNullOrEmpty(valor) ? 0 : valor.Count(char.IsDigit);
        }

        private static TimeZoneInfo BuscarZonaArgentina()
        {
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
            }
            catch (TimeZoneNotFoundException)
            {
                // Argentina no tiene horario de verano: UTC-3 fijo.
                return TimeZoneInfo.CreateCustomTimeZone("AR", TimeSpan.FromHours(-3), "Argentina", "Argentina");
            }
        }
    }
}
