using System.Collections.Generic;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Ítem de acceso rápido en el panel principal.
    /// </summary>
    public sealed class QuickAccessItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Variant { get; set; }
    }

    /// <summary>
    /// Catálogo estático de accesos frecuentes y módulos secundarios del home.
    /// </summary>
    public static class QuickAccessCatalog
    {
        public static IList<QuickAccessItem> GetFrequentActions()
        {
            return new List<QuickAccessItem>
            {
                Item("Seguimiento presupuestos", "Seguimiento y cotizaciones telefónicas", "/Presupuesto/Seguimiento", "bi-clipboard-data", "success"),
                Item("Listado de viajes", "Reservas y disponibilidad", "/Viaje/Index", "bi-airplane", "brand"),
                Item("Viajes por fecha", "Consultar salidas por día", "/Home/ViajesPorFecha", "bi-calendar-event", "warning"),
                Item("Nuevo cliente", "Alta de persona cliente", "/PersonaCliente/Create", "bi-person-plus", "info"),
                Item("Buscar facturas", "Consulta y gestión de facturas", "/Factura/Index", "bi-receipt", "accent"),
                Item("Notas de crédito", "Devoluciones y notas", "/PersonaCliente/NotaCreditoList", "bi-file-earmark-minus", "dark")
            };
        }

        public static IList<QuickAccessItem> GetAllModules(bool includeHistorialPagos)
        {
            var items = new List<QuickAccessItem>
            {
                Item("Listado presupuestos", "Todos los presupuestos", "/Presupuesto/Index", "bi-file-earmark-text", "success"),
                Item("Todos los viajes", "Catálogo completo de viajes", "/Home/TodosLosViajesIndex", "bi-suitcase", "brand"),
                Item("Paquetes", "Configuración de paquetes turísticos", "/Paquete/Index", "bi-box-seam", "info"),
                Item("Listado clientes", "Personas clientes registradas", "/PersonaCliente/Index", "bi-people", "brand"),
                Item("Métricas presupuestos", "Indicadores del módulo", "/Presupuesto/Metricas", "bi-graph-up", "accent")
            };

            if (includeHistorialPagos)
            {
                items.Add(Item("Historial de pagos", "Consulta completa de pagos", "/Home/HistorialPagos", "bi-clock-history", "dark"));
            }

            return items;
        }

        private static QuickAccessItem Item(string title, string description, string url, string icon, string variant)
        {
            return new QuickAccessItem
            {
                Title = title,
                Description = description,
                Url = url,
                Icon = icon,
                Variant = variant
            };
        }
    }
}
