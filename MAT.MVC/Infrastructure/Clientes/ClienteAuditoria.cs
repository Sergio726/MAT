using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using MAT.MVC.Models.Clientes;

namespace MAT.MVC.Infrastructure.Clientes
{
    /// <summary>
    /// Arma el detalle de una modificación para <c>ClienteAudit</c>: una línea por campo cambiado,
    /// con el nombre que ve el usuario ("Celular: 387 4111111 → 387 4222222") (módulo Clientes F6).
    /// </summary>
    public static class ClienteAuditoria
    {
        public const string Alta = "Alta";
        public const string Modificacion = "Modificacion";

        /// <summary>Campos que no se registran: ids técnicos y datos solo de pantalla.</summary>
        private static readonly HashSet<string> Excluidos = new HashSet<string>
        {
            "PersonaId", "PaisId", "DepartamentoId", "ReturnUrl", "EsAlta"
        };

        private static readonly PropertyInfo[] Campos = typeof(ClienteFormModel)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.CanWrite && !Excluidos.Contains(p.Name))
            .ToArray();

        /// <param name="texto">Convierte un valor en texto legible (por ejemplo el número de localidad en su nombre);
        /// recibe el nombre de la propiedad y el valor. Si devuelve null se usa el valor tal cual.</param>
        /// <returns>Las líneas separadas por salto de línea; vacío si no cambió nada.</returns>
        public static string Diferencias(ClienteFormModel antes, ClienteFormModel despues, Func<string, object, string> texto = null)
        {
            if (antes == null || despues == null) return string.Empty;
            var lineas = new List<string>();
            foreach (var campo in Campos)
            {
                var valorAntes = Normalizar(campo.GetValue(antes));
                var valorDespues = Normalizar(campo.GetValue(despues));
                if (string.Equals(valorAntes, valorDespues, StringComparison.Ordinal)) continue;

                lineas.Add(Rotulo(campo) + ": " +
                    Mostrar(campo.Name, campo.GetValue(antes), valorAntes, texto) + " → " +
                    Mostrar(campo.Name, campo.GetValue(despues), valorDespues, texto));
            }
            return string.Join("\n", lineas);
        }

        private static string Normalizar(object valor)
        {
            if (valor == null) return null;
            if (valor is bool) return (bool)valor ? "Sí" : "No";
            var s = Convert.ToString(valor, System.Globalization.CultureInfo.InvariantCulture).Trim();
            return s.Length == 0 ? null : s;
        }

        private static string Mostrar(string propiedad, object original, string normalizado, Func<string, object, string> texto)
        {
            if (normalizado == null) return "(vacío)";
            var legible = texto != null ? texto(propiedad, original) : null;
            return string.IsNullOrWhiteSpace(legible) ? normalizado : legible;
        }

        private static string Rotulo(PropertyInfo campo)
        {
            var display = campo.GetCustomAttribute<DisplayAttribute>();
            return display != null && !string.IsNullOrEmpty(display.Name) ? display.Name : campo.Name;
        }
    }
}
