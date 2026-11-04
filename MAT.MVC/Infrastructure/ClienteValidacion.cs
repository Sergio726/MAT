using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Reglas del cliente que comparten el formulario (atributos de <c>Infrastructure/Validation</c>) y cualquier
    /// otro alta: fecha de nacimiento real, teléfono o celular completo, documento según su tipo.
    /// Los mensajes son los que ve el vendedor, en el navegador y en el servidor.
    /// </summary>
    public static class ClienteValidacion
    {
        public const int MinimoDigitosTelefono = 7;
        public const int AnioMinimoNacimiento = 1910;
        public const string FormatoFecha = "dd/MM/yyyy";

        public const string MensajeFechaFormato = "La fecha de nacimiento no es válida. Usá el formato dd/mm/aaaa.";
        public const string MensajeFechaFutura = "La fecha de nacimiento no puede ser futura.";
        public const string MensajeFechaMinima = "La fecha de nacimiento no puede ser anterior a 1910.";
        public const string MensajeTelefonoIncompleto = "El número está incompleto: cargalo con código de área (al menos 7 números).";
        public const string MensajeTelefonoOCelular = "Cargá al menos un teléfono o celular.";

        private static readonly TimeZoneInfo ZonaArgentina = BuscarZonaArgentina();

        /// <summary>
        /// Fecha de hoy en Argentina (los servidores pueden estar en UTC).
        /// </summary>
        public static DateTime HoyArgentina()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, ZonaArgentina).Date;
        }

        /// <summary>
        /// Interpreta "dd/mm/aaaa" (también con un solo dígito en día o mes). No acepta fechas inexistentes como 31/02.
        /// </summary>
        public static bool TryParseFecha(string texto, out DateTime fecha)
        {
            return DateTime.TryParseExact((texto ?? string.Empty).Trim(), new[] { "dd/MM/yyyy", "d/M/yyyy" },
                CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha);
        }

        /// <summary>
        /// Error de la fecha de nacimiento escrita, o null si es válida. Un valor vacío lo resuelve [Required].
        /// </summary>
        public static string ValidarFechaNacimiento(string texto, DateTime hoy)
        {
            if (string.IsNullOrWhiteSpace(texto)) return null;
            DateTime fecha;
            if (!TryParseFecha(texto, out fecha)) return MensajeFechaFormato;
            return ValidarFechaNacimiento(fecha, hoy);
        }

        public static string ValidarFechaNacimiento(DateTime fecha, DateTime hoy)
        {
            if (fecha.Date > hoy) return MensajeFechaFutura;
            if (fecha.Year < AnioMinimoNacimiento) return MensajeFechaMinima;
            return null;
        }

        /// <summary>
        /// Un teléfono cargado tiene que estar completo; vacío está permitido (lo exige <see cref="TieneAlgunTelefono"/>).
        /// </summary>
        public static bool TelefonoCompletoOVacio(string telefono)
        {
            var digitos = CantidadDigitos(telefono);
            return digitos == 0 ? string.IsNullOrWhiteSpace(telefono) : digitos >= MinimoDigitosTelefono;
        }

        public static bool TieneAlgunTelefono(string telefono, string celular)
        {
            return CantidadDigitos(telefono) >= MinimoDigitosTelefono || CantidadDigitos(celular) >= MinimoDigitosTelefono;
        }

        /// <summary>
        /// Error del número de documento según el tipo (eTipoDocumento), o null si es válido.
        /// </summary>
        public static string ValidarDocumento(int? tipoDocumento, string numero)
        {
            if (string.IsNullOrWhiteSpace(numero)) return null;
            var limpio = numero.Trim();

            if (tipoDocumento == 2) // Pasaporte
            {
                var alfanumerico = new string(limpio.Where(char.IsLetterOrDigit).ToArray());
                return alfanumerico.Length >= 5 && alfanumerico.Length <= 20 && limpio.All(c => char.IsLetterOrDigit(c) || c == '-' || c == ' ')
                    ? null
                    : "El pasaporte debe tener entre 5 y 20 letras o números.";
            }

            if (limpio.Any(c => !char.IsDigit(c) && c != '.' && c != ' ' && c != '-'))
                return "El documento solo puede tener números.";

            var digitos = new string(limpio.Where(char.IsDigit).ToArray()).TrimStart('0');
            if (tipoDocumento == 1 || tipoDocumento == null) // DNI
                return digitos.Length >= 7 && digitos.Length <= 8 ? null : "El DNI debe tener 7 u 8 números.";

            return digitos.Length >= 6 && digitos.Length <= 10 ? null : "El documento debe tener entre 6 y 10 números.";
        }

        /// <summary>
        /// Reglas de contacto en bloque (usado por pantallas que no tienen el formulario de cliente).
        /// </summary>
        public static List<string> ValidarDatosContacto(DateTime? fechaNacimiento, string telefono, string celular, DateTime hoy)
        {
            var errores = new List<string>();

            if (!fechaNacimiento.HasValue)
                errores.Add("Ingrese la fecha de nacimiento (dd/mm/aaaa).");
            else
            {
                var errorFecha = ValidarFechaNacimiento(fechaNacimiento.Value, hoy);
                if (errorFecha != null) errores.Add(errorFecha);
            }

            if (!TieneAlgunTelefono(telefono, celular))
                errores.Add("Ingrese un teléfono o celular completo (no solo el código de área).");

            return errores;
        }

        public static int CantidadDigitos(string valor)
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
