using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MAT.Enums;
using MAT.MVC.Models.Seguimiento;
using MAT.Utilities;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Categorías de seguimiento de clientes, resultados de contacto y armado del saludo de cumpleaños.
    /// Reglas acordadas en <c>docs/DECISIONES_ABIERTAS.md</c> (Seguimiento de clientes, 2026-10-29);
    /// el cálculo de la categoría vive en <c>fn_MAT_Seguimiento_Clientes</c>.
    /// </summary>
    public static class SeguimientoCatalog
    {
        private static readonly List<SeguimientoCategoriaDto> Categorias = new List<SeguimientoCategoriaDto>
        {
            Categoria("VIP", "VIP", "5 o más viajes y viajó en los últimos 12 meses", "Saludo especial y primero en enterarse de las salidas", "bi-gem"),
            Categoria("EnRiesgo", "En riesgo", "3 o más viajes y entre 12 y 24 meses sin viajar", "Llamada de recuperación con una propuesta", "bi-exclamation-triangle"),
            Categoria("Frecuente", "Frecuente", "2 a 4 viajes y viajó en los últimos 12 meses", "Mantener el contacto y avisar de salidas que le gusten", "bi-arrow-repeat"),
            Categoria("Nuevo", "Nuevo", "1 viaje en los últimos 6 meses", "Preguntar cómo le fue y proponer el segundo viaje", "bi-stars"),
            Categoria("Ocasional", "Ocasional", "1 o 2 viajes y entre 6 y 24 meses sin viajar", "Recordarle las próximas salidas", "bi-calendar-heart"),
            Categoria("Dormido", "Dormido", "Más de 24 meses sin viajar", "Campaña general de reactivación", "bi-moon"),
            Categoria("SinViajes", "Sin viajes", "Cliente cargado que todavía no viajó", "Averiguar qué busca y ofrecer una salida", "bi-person-dash")
        };

        public static IList<SeguimientoCategoriaDto> GetCategorias()
        {
            return Categorias.Select(c => new SeguimientoCategoriaDto
            {
                Codigo = c.Codigo,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                AccionSugerida = c.AccionSugerida,
                Icono = c.Icono
            }).ToList();
        }

        public static bool EsCategoriaValida(string codigo)
        {
            return Categorias.Any(c => c.Codigo == codigo);
        }

        public static IList<KeyValuePair<int, string>> GetResultados()
        {
            return Enum.GetValues(typeof(eResultadoContacto))
                .Cast<eResultadoContacto>()
                .Select(r => new KeyValuePair<int, string>((int)r, r.GetDescription()))
                .ToList();
        }

        public static string NombreResultado(int? resultado)
        {
            if (!resultado.HasValue || !Enum.IsDefined(typeof(eResultadoContacto), resultado.Value)) return null;
            return ((eResultadoContacto)resultado.Value).GetDescription();
        }

        /// <summary>
        /// Completa etiquetas y enlace de WhatsApp. Con <paramref name="mensaje"/> null el enlace abre el chat vacío.
        /// </summary>
        public static void Completar(SeguimientoClienteDto cliente, string mensaje = null)
        {
            var categoria = Categorias.FirstOrDefault(c => c.Codigo == cliente.Categoria);
            cliente.CategoriaNombre = categoria?.Nombre ?? cliente.Categoria;
            cliente.AccionSugerida = categoria?.AccionSugerida;
            cliente.UltimoContactoResultadoNombre = NombreResultado(cliente.UltimoContactoResultado);

            var telefono = WhatsAppHelper.ElegirTelefono(cliente.Celular, cliente.Telefono);
            cliente.TieneWhatsApp = telefono != null;
            cliente.WhatsAppUrl = telefono == null ? null : WhatsAppHelper.ConstruirEnlace(telefono, mensaje);
        }

        /// <summary>
        /// "¡Feliz cumpleaños, {Nombre}! 🎉 ... — {Vendedor}". Sin viajes previos se omite el agradecimiento.
        /// </summary>
        public static string ArmarSaludoCumpleanios(SeguimientoClienteDto cliente, string nombreVendedor)
        {
            var nombre = PrimerNombre(cliente.Nombre);
            var saludo = string.IsNullOrEmpty(nombre) ? "¡Feliz cumpleaños! 🎉" : "¡Feliz cumpleaños, " + nombre + "! 🎉";
            saludo += " De parte de todo el equipo de Marco Antonio Tours te deseamos un gran día.";
            if (cliente.ViajesTotales > 0) saludo += " Gracias por viajar con nosotros.";
            if (!string.IsNullOrWhiteSpace(nombreVendedor)) saludo += " — " + nombreVendedor.Trim();
            return saludo;
        }

        /// <summary>
        /// Primer nombre en formato título ("RITA NERINA" → "Rita").
        /// </summary>
        public static string PrimerNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return null;
            var primero = nombre.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
            return CultureInfo.GetCultureInfo("es-AR").TextInfo.ToTitleCase(primero.ToLowerInvariant());
        }

        /// <summary>
        /// Nombre y apellido en formato título para firmar mensajes.
        /// </summary>
        public static string NombreParaFirma(string nombre, string apellido)
        {
            var texto = string.Join(" ", new[] { PrimerNombre(nombre), apellido?.Trim() }.Where(s => !string.IsNullOrEmpty(s)));
            return string.IsNullOrEmpty(texto) ? null : CultureInfo.GetCultureInfo("es-AR").TextInfo.ToTitleCase(texto.ToLowerInvariant());
        }

        private static SeguimientoCategoriaDto Categoria(string codigo, string nombre, string descripcion, string accion, string icono)
        {
            return new SeguimientoCategoriaDto { Codigo = codigo, Nombre = nombre, Descripcion = descripcion, AccionSugerida = accion, Icono = icono };
        }
    }
}
