using System;
using System.Linq;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Normaliza teléfonos argentinos al formato que espera WhatsApp (<c>549</c> + área + número, sin el 0 ni el 15)
    /// y arma enlaces <c>https://wa.me/...</c>.
    /// </summary>
    /// <remarks>
    /// Formatos cargados en <c>Persona.Celular</c> / <c>Persona.Telefono</c> (relevamiento 2026-10-29):
    /// <list type="bullet">
    /// <item><c>3874123456</c>, <c>0387-4123456</c>, <c>0387-15-4123456</c>, <c>+54 9 387 4123456</c>.</item>
    /// <item><c>15-4123456</c> o <c>4123456</c> sin código de área: se asume el área por defecto (Salta, 387).</item>
    /// </list>
    /// Un número de 10 dígitos puede ser fijo; no hay forma de distinguirlo, WhatsApp avisa si no tiene cuenta.
    /// </remarks>
    public static class WhatsAppHelper
    {
        public const string CodigoAreaDefault = "387";

        /// <summary>
        /// Devuelve el número listo para <c>wa.me</c> (solo dígitos, con código de país) o <c>null</c> si no se puede interpretar.
        /// </summary>
        public static string NormalizarTelefono(string telefono, string codigoAreaDefault = CodigoAreaDefault)
        {
            if (string.IsNullOrWhiteSpace(telefono)) return null;

            var texto = telefono.Trim();
            var d = new string(texto.Where(char.IsDigit).ToArray());
            if (d.Length == 0) return null;

            var internacional = texto.StartsWith("+") || d.StartsWith("00");
            if (d.StartsWith("00")) d = d.Substring(2);

            if (d.StartsWith("54") && (d.Length == 12 || d.Length == 13))
            {
                d = d.Substring(2);
                if (d.Length == 11 && d[0] == '9') d = d.Substring(1);
            }
            else if (internacional)
            {
                // Número de otro país: se usa tal cual si tiene un largo razonable.
                return d.Length >= 8 && d.Length <= 15 ? d : null;
            }

            if (d.StartsWith("0")) d = d.Substring(1);
            if (d.Length == 11 && d[0] == '9') d = d.Substring(1);

            if (d.Length == 12) d = QuitarQuince(d);
            else if (d.Length == 9 && d.StartsWith("15")) d = codigoAreaDefault + d.Substring(2);
            else if (d.Length == 7) d = codigoAreaDefault + d;

            if (d == null || d.Length != 10 || d[0] == '0') return null;
            return "549" + d;
        }

        /// <summary>
        /// Primer teléfono que se pueda normalizar (en orden: celular, teléfono), o <c>null</c>.
        /// </summary>
        public static string ElegirTelefono(params string[] telefonos)
        {
            if (telefonos == null) return null;
            return telefonos.Select(t => NormalizarTelefono(t)).FirstOrDefault(n => n != null);
        }

        /// <summary>
        /// Arma el enlace <c>https://wa.me/{numero}?text=...</c>; <c>null</c> si el teléfono no es válido.
        /// </summary>
        public static string ConstruirEnlace(string telefono, string mensaje)
        {
            var numero = NormalizarTelefono(telefono);
            if (numero == null) return null;

            var enlace = "https://wa.me/" + numero;
            if (!string.IsNullOrEmpty(mensaje)) enlace += "?text=" + Uri.EscapeDataString(mensaje);
            return enlace;
        }

        /// <summary>
        /// Quita el 15 que sigue al código de área (área de 2, 3 o 4 dígitos) en números de 12 dígitos.
        /// </summary>
        private static string QuitarQuince(string d)
        {
            var largosArea = d.StartsWith("11") ? new[] { 2 } : new[] { 3, 4, 2 };
            foreach (var largo in largosArea)
            {
                if (d.Substring(largo, 2) == "15") return d.Substring(0, largo) + d.Substring(largo + 2);
            }
            return null;
        }
    }
}
