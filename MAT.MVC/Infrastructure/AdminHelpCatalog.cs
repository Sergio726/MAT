using System;
using System.Collections.Generic;
using System.Linq;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Ítem del catálogo de búsqueda / ayuda / navegación del panel Admin.
    /// </summary>
    public sealed class AdminHelpCatalogItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Category { get; set; }
        public string Icon { get; set; }
        public IList<string> Keywords { get; set; }
        public bool ShowInSidebar { get; set; }
        public string SidebarSection { get; set; }
        public int SidebarOrder { get; set; }
    }

    /// <summary>
    /// Catálogo centralizado de herramientas accesibles desde el panel Administrador.
    /// </summary>
    public static class AdminHelpCatalog
    {
        private static readonly string[] SidebarSectionOrder = { "reportes", "usuarios", "sistema", "dev" };

        public static IList<AdminHelpCatalogItem> GetItems(bool includeAdminDevTools)
        {
            var items = new List<AdminHelpCatalogItem>
            {
                SidebarNav("panel", "Panel", "Hub principal con acceso a todas las herramientas admin.", "/Admin/Index", "bi-grid-1x2", "reportes", 1,
                    "inicio", "hub", "panel", "administración"),

                SidebarReport("resumen-pagos-viaje", "Pagos por viaje", "Resumen de pagos agrupados por viaje seleccionado.", "/Admin/ResumenPagos", "bi-cash-stack", 2,
                    "pagos", "viaje", "resumen", "cobros"),
                SidebarReport("resumen-pagos-fecha", "Pagos por fecha", "Consultar pagos registrados en una fecha.", "/Admin/ResumenPagosPorFecha", "bi-calendar-check", 3,
                    "pagos", "fecha", "día", "cobros"),
                SidebarReport("auditoria-facturas", "Auditoría de facturas", "Historial de acciones sobre facturas por rango de fechas.", "/Admin/AuditoriaFacturas", "bi-clipboard-check", 4,
                    "auditoría", "facturas", "historial", "acciones"),
                SidebarReport("reportes-index", "Reportes operativos", "Índice de reportes de ventas, pagos y ranking.", "/Admin/Reportes", "bi-graph-up-arrow", 5,
                    "reportes", "operativos", "estadísticas"),
                Report("reporte-ventas", "Reporte de ventas", "Facturas, montos y saldos por fechas o por viaje.", "/Admin/Reportes/ReporteVentas", "bi-receipt-cutoff",
                    "ventas", "facturas", "excel", "estadísticas"),
                Report("reporte-pagos", "Reporte de pagos", "Movimientos de pago con filtros avanzados.", "/Admin/Reportes/ReportePagos", "bi-cash-coin",
                    "pagos", "movimientos", "excel", "cobros"),
                Report("reporte-ranking", "Ranking de compras", "Ranking de clientes y viajes por volumen.", "/Admin/Reportes/ReporteRanking", "bi-trophy",
                    "ranking", "compras", "clientes", "viajes"),

                SidebarUser("usuarios", "Listado de usuarios", "Ver cuentas, roles y estado de los usuarios.", "/Admin/Usuarios", "bi-people", 1,
                    "usuarios", "cuentas", "roles", "listado"),
                User("usuario-editar", "Editar roles de usuario", "Asignar o quitar roles desde el listado de usuarios.", "/Admin/Usuarios", "bi-person-gear",
                    "roles", "permisos", "editar", "administrador", "vendedor"),
                SidebarUser("registrar-vendedor", "Registrar vendedor", "Alta de cuenta y datos de vendedor sin cerrar sesión.", "/Admin/RegistrarVendedor", "bi-person-plus", 2,
                    "vendedor", "alta", "registro", "nuevo usuario"),
                SidebarUser("mi-cuenta", "Mi cuenta", "Cambiar la contraseña del usuario con el que inició sesión.", "/Admin/MiCuenta", "bi-key", 3,
                    "contraseña", "cuenta", "perfil", "clave"),

                SidebarSys("codigos-confirmacion", "Códigos de confirmación", "Gestionar códigos para eliminar ventas o pasajeros.", "/Admin/SistemaParametros", "bi-shield-lock", 1,
                    "códigos", "confirmación", "parámetros", "eliminar venta", "seguridad"),
                SidebarSys("error-log", "Error Log", "Errores en base de datos con correlation ID.", "/Admin/ErrorLog", "bi-bug", 2,
                    "error", "log", "diagnóstico", "correlation"),
                SidebarSys("logs-memoria", "Log en memoria", "Traza reciente del proceso en memoria.", "/Admin/Logs", "bi-terminal", 3,
                    "log", "memoria", "traza", "diagnóstico"),

                Nav("intranet-home", "Ir a la intranet (Home)", "Volver al panel principal de la agencia.", "/Home/Index", "bi-house-door",
                    "inicio", "intranet", "operación", "vendedores"),

                Intranet("presupuesto-index", "Presupuestos", "Listado y gestión de presupuestos telefónicos.", "/Presupuesto/Index", "bi-file-earmark-text",
                    "presupuesto", "presupuestos", "cotización", "seguimiento"),
                Intranet("presupuesto-seguimiento", "Seguimiento de presupuestos", "Seguimiento y estado de presupuestos en curso.", "/Presupuesto/Seguimiento", "bi-clipboard-data",
                    "presupuesto", "seguimiento", "estado"),
                Intranet("presupuesto-metricas", "Métricas de presupuestos", "Dashboard de métricas y conversión de presupuestos.", "/Presupuesto/Metricas", "bi-graph-up",
                    "presupuesto", "métricas", "conversión", "estadísticas"),

                Intranet("viaje-index", "Viajes", "Administración de viajes, reservas y disponibilidad.", "/Viaje/Index", "bi-bus-front",
                    "viaje", "viajes", "salida", "transporte"),
                Intranet("viajes-por-fecha", "Viajes por fecha", "Consultar viajes disponibles en una fecha.", "/Home/ViajesPorFecha", "bi-calendar-event",
                    "viaje", "fecha", "salida", "disponibilidad"),
                Intranet("todos-los-viajes", "Todos los viajes", "Listado completo de viajes del sistema.", "/Home/TodosLosViajesIndex", "bi-calendar2-week",
                    "viaje", "viajes", "listado", "todos"),

                Intranet("factura-fiscal-index", "Facturación fiscal", "Listado de facturas fiscales de compra y venta.", "/FacturaFiscal/Index", "bi-receipt-cutoff",
                    "factura", "fiscal", "compra", "venta", "arca"),
                Intranet("factura-fiscal-proveedores", "Proveedores fiscales", "Gestión de proveedores para facturación fiscal.", "/FacturaFiscal/Proveedores", "bi-building",
                    "proveedor", "proveedores", "fiscal", "factura"),

                Intranet("persona-cliente-index", "Clientes", "Listado y búsqueda de clientes de la agencia.", "/PersonaCliente/Index", "bi-people",
                    "cliente", "clientes", "persona", "cuenta"),

                Intranet("factura-operativa-index", "Búsqueda de facturas", "Consultar facturas operativas de la intranet.", "/Factura/Index", "bi-search",
                    "factura", "facturas", "operativa", "buscar")
            };

            if (includeAdminDevTools)
            {
                items.Add(SidebarDev("historial-pagos-dev", "Historial de pagos", "Historial completo de pagos del sistema.", "/Home/HistorialPagos", "bi-clock-history", 1,
                    "historial", "pagos", "dev", "adminddev"));
            }

            return items;
        }

        public static IList<AdminNavSection> GetSidebarSections(bool includeAdminDevTools)
        {
            var items = GetItems(includeAdminDevTools)
                .Where(i => i.ShowInSidebar)
                .OrderBy(i => Array.IndexOf(SidebarSectionOrder, (i.SidebarSection ?? "").ToLowerInvariant()))
                .ThenBy(i => i.SidebarOrder)
                .ToList();

            return items
                .GroupBy(i => i.SidebarSection, StringComparer.OrdinalIgnoreCase)
                .Select(g => new AdminNavSection
                {
                    Id = g.Key.ToLowerInvariant(),
                    Title = GetSidebarSectionTitle(g.Key),
                    Items = g.ToList()
                })
                .ToList();
        }

        public static AdminPageContext ResolvePage(string absolutePath, bool includeAdminDevTools)
        {
            if (string.IsNullOrWhiteSpace(absolutePath))
                return null;

            var path = NormalizePath(absolutePath);
            if (!path.StartsWith("/admin", StringComparison.OrdinalIgnoreCase)
                && !path.StartsWith("/home/historialpagos", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var items = GetItems(includeAdminDevTools);

            // Rutas especiales sin ítem directo en catálogo
            if (path.StartsWith("/admin/usuarioeditar", StringComparison.OrdinalIgnoreCase))
            {
                return new AdminPageContext
                {
                    Title = "Editar usuario",
                    SectionTitle = "Usuarios",
                    SectionUrl = "/Admin/Usuarios",
                    ShowBreadcrumb = true
                };
            }

            if (path.StartsWith("/admin/usuarioresetpassword", StringComparison.OrdinalIgnoreCase))
            {
                return new AdminPageContext
                {
                    Title = "Restablecer contraseña",
                    SectionTitle = "Usuarios",
                    SectionUrl = "/Admin/Usuarios",
                    ShowBreadcrumb = true
                };
            }

            // Coincidencia exacta
            var exact = items.FirstOrDefault(i => NormalizePath(i.Url).Equals(path, StringComparison.OrdinalIgnoreCase));
            if (exact != null)
            {
                return BuildPageContext(exact, path);
            }

            // Prefijo más largo (subrutas Reportes, etc.)
            var prefix = items
                .Where(i => !string.IsNullOrWhiteSpace(i.Url))
                .Select(i => new { Item = i, Normalized = NormalizePath(i.Url) })
                .Where(x => path.StartsWith(x.Normalized + "/", StringComparison.OrdinalIgnoreCase)
                    || (x.Normalized.Equals("/admin/reportes", StringComparison.OrdinalIgnoreCase)
                        && path.StartsWith("/admin/reportes", StringComparison.OrdinalIgnoreCase)))
                .OrderByDescending(x => x.Normalized.Length)
                .Select(x => x.Item)
                .FirstOrDefault();

            if (prefix != null)
            {
                return BuildPageContext(prefix, path);
            }

            if (path.Equals("/admin", StringComparison.OrdinalIgnoreCase))
            {
                return BuildPageContext(items.First(i => i.Id == "panel"), path);
            }

            return null;
        }

        public static string GetUserInitials(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return "U";

            var parts = userName.Split(new[] { ' ', '.', '_', '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
                return (parts[0].Substring(0, 1) + parts[1].Substring(0, 1)).ToUpperInvariant();

            return userName.Length >= 2
                ? userName.Substring(0, 2).ToUpperInvariant()
                : userName.Substring(0, 1).ToUpperInvariant();
        }

        private static AdminPageContext BuildPageContext(AdminHelpCatalogItem item, string path)
        {
            var isIndex = path.Equals("/admin", StringComparison.OrdinalIgnoreCase)
                || path.Equals("/admin/index", StringComparison.OrdinalIgnoreCase);

            return new AdminPageContext
            {
                Title = item.Title,
                SectionTitle = GetSidebarSectionTitle(item.SidebarSection) ?? item.Category,
                SectionUrl = GetSectionUrl(item),
                ShowBreadcrumb = !isIndex
            };
        }

        private static string GetSectionUrl(AdminHelpCatalogItem item)
        {
            if (string.IsNullOrWhiteSpace(item.SidebarSection))
                return null;

            switch (item.SidebarSection.ToLowerInvariant())
            {
                case "reportes": return "/Admin/Index";
                case "usuarios": return "/Admin/Usuarios";
                case "sistema": return "/Admin/ErrorLog";
                case "dev": return "/Admin/Index";
                default: return "/Admin/Index";
            }
        }

        private static string GetSidebarSectionTitle(string sectionId)
        {
            if (string.IsNullOrWhiteSpace(sectionId))
                return null;

            switch (sectionId.ToLowerInvariant())
            {
                case "reportes": return "Reportes";
                case "usuarios": return "Usuarios";
                case "sistema": return "Sistema";
                case "dev": return "Dev";
                default: return sectionId;
            }
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

        private static AdminHelpCatalogItem SidebarNav(string id, string title, string desc, string url, string icon, string sidebarSection, int order, params string[] keywords)
        {
            return SidebarItem(id, title, desc, url, "Navegación", icon, sidebarSection, order, keywords);
        }

        private static AdminHelpCatalogItem SidebarReport(string id, string title, string desc, string url, string icon, int order, params string[] keywords)
        {
            return SidebarItem(id, title, desc, url, "Reportes", icon, "reportes", order, keywords);
        }

        private static AdminHelpCatalogItem SidebarUser(string id, string title, string desc, string url, string icon, int order, params string[] keywords)
        {
            return SidebarItem(id, title, desc, url, "Usuarios", icon, "usuarios", order, keywords);
        }

        private static AdminHelpCatalogItem SidebarSys(string id, string title, string desc, string url, string icon, int order, params string[] keywords)
        {
            return SidebarItem(id, title, desc, url, "Sistema", icon, "sistema", order, keywords);
        }

        private static AdminHelpCatalogItem SidebarDev(string id, string title, string desc, string url, string icon, int order, params string[] keywords)
        {
            return SidebarItem(id, title, desc, url, "Dev", icon, "dev", order, keywords);
        }

        private static AdminHelpCatalogItem SidebarItem(string id, string title, string desc, string url, string category, string icon, string sidebarSection, int order, params string[] keywords)
        {
            var item = Item(id, title, desc, url, category, icon, keywords);
            item.ShowInSidebar = true;
            item.SidebarSection = sidebarSection;
            item.SidebarOrder = order;
            return item;
        }

        private static AdminHelpCatalogItem Nav(string id, string title, string desc, string url, string icon, params string[] keywords)
        {
            return Item(id, title, desc, url, "Navegación", icon, keywords);
        }

        private static AdminHelpCatalogItem Report(string id, string title, string desc, string url, string icon, params string[] keywords)
        {
            return Item(id, title, desc, url, "Reportes", icon, keywords);
        }

        private static AdminHelpCatalogItem User(string id, string title, string desc, string url, string icon, params string[] keywords)
        {
            return Item(id, title, desc, url, "Usuarios", icon, keywords);
        }

        private static AdminHelpCatalogItem Sys(string id, string title, string desc, string url, string icon, params string[] keywords)
        {
            return Item(id, title, desc, url, "Sistema", icon, keywords);
        }

        private static AdminHelpCatalogItem Intranet(string id, string title, string desc, string url, string icon, params string[] keywords)
        {
            return Item(id, title, desc, url, "Intranet", icon, keywords);
        }

        private static AdminHelpCatalogItem Item(string id, string title, string desc, string url, string category, string icon, params string[] keywords)
        {
            return new AdminHelpCatalogItem
            {
                Id = id,
                Title = title,
                Description = desc,
                Url = url,
                Category = category,
                Icon = icon,
                Keywords = keywords,
                ShowInSidebar = false,
                SidebarSection = null,
                SidebarOrder = 0
            };
        }
    }
}
