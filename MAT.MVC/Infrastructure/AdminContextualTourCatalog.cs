using System;
using System.Collections.Generic;
using System.Linq;

namespace MAT.MVC.Infrastructure
{
    public sealed class AdminContextualTourStep
    {
        public string Selector { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public sealed class AdminContextualTourDefinition
    {
        public string PageId { get; set; }
        public string PathMatch { get; set; }
        public bool ExactMatch { get; set; }
        public IList<AdminContextualTourStep> Steps { get; set; }
    }

    /// <summary>
    /// Tours cortos por pantalla del panel Admin (persistencia en localStorage del cliente).
    /// </summary>
    public static class AdminContextualTourCatalog
    {
        public static IList<AdminContextualTourDefinition> GetDefinitions()
        {
            return new List<AdminContextualTourDefinition>
            {
                Def("reportes-hub", "/Admin/Reportes", exact: true,
                    Step("#admin-ctx-tour-reportes-header", "Reportes operativos",
                        "Desde aquí accede a los tres reportes analíticos del panel: ventas, pagos y ranking."),
                    Step("#admin-ctx-tour-reportes-cards", "Elegir reporte",
                        "Seleccione una tarjeta para abrir el reporte. Cada uno tiene filtros por fecha o por viaje.")),

                Def("usuarios", "/Admin/Usuarios", exact: true,
                    Step("#admin-ctx-tour-usuarios-header", "Gestión de usuarios",
                        "Administre cuentas del sistema: roles, estado y contraseñas."),
                    Step("#admin-ctx-tour-usuarios-filtro", "Filtrar por estado",
                        "Use el filtro para ver solo usuarios activos, deshabilitados o bloqueados."),
                    Step("#admin-ctx-tour-usuarios-tabla", "Listado y acciones",
                        "Desde la tabla puede editar roles, restablecer contraseña o deshabilitar cuentas.")),

                Def("sistema-parametros", "/Admin/SistemaParametros", exact: true,
                    Step("#admin-ctx-tour-parametros-header", "Códigos de confirmación",
                        "Configure los códigos que el personal debe ingresar para operaciones sensibles."),
                    Step("#admin-ctx-tour-parametros-tabla", "Tabla de parámetros",
                        "Cree, edite o active/desactive códigos. Los valores no se muestran en claro por seguridad.")),

                Def("error-log", "/Admin/ErrorLog", exact: true,
                    Step("#admin-ctx-tour-errorlog-filtros", "Filtros de búsqueda",
                        "Filtre por correlation ID o fecha para acotar los errores registrados."),
                    Step("#admin-ctx-tour-errorlog-vista", "Tabla y diagnóstico",
                        "Alterne entre la vista de tabla detallada y el resumen de diagnóstico por tipo de error."))
            };
        }

        public static AdminContextualTourDefinition Resolve(string absolutePath)
        {
            if (string.IsNullOrWhiteSpace(absolutePath))
                return null;

            var path = NormalizePath(absolutePath);
            var defs = GetDefinitions();

            var exact = defs.FirstOrDefault(d => d.ExactMatch && NormalizePath(d.PathMatch).Equals(path, StringComparison.OrdinalIgnoreCase));
            if (exact != null)
                return exact;

            return defs
                .Where(d => !d.ExactMatch && path.StartsWith(NormalizePath(d.PathMatch), StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(d => d.PathMatch.Length)
                .FirstOrDefault();
        }

        private static AdminContextualTourDefinition Def(string pageId, string pathMatch, bool exact, params AdminContextualTourStep[] steps)
        {
            return new AdminContextualTourDefinition
            {
                PageId = pageId,
                PathMatch = pathMatch,
                ExactMatch = exact,
                Steps = steps
            };
        }

        private static AdminContextualTourStep Step(string selector, string title, string text)
        {
            return new AdminContextualTourStep
            {
                Selector = selector,
                Title = title,
                Text = text
            };
        }

        private static string NormalizePath(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return "/";

            var path = url.Split('?')[0].Trim();
            if (!path.StartsWith("/"))
                path = "/" + path;

            return path.TrimEnd('/').ToLowerInvariant();
        }
    }
}
