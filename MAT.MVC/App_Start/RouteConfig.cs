using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MAT.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Reportes administrativos — ver ReportesController (hub sin segmento extra)
            routes.MapRoute(
                name: "AdminReportesHub",
                url: "Admin/Reportes",
                defaults: new { controller = "Reportes", action = "Index" }
            );
            routes.MapRoute(
                name: "AdminReportes",
                url: "Admin/Reportes/{action}",
                defaults: new { controller = "Reportes", action = "Index" },
                constraints: new { action = "Index|ReporteVentas|ReportePagos|ReporteRanking|Ventas|Pagos|Ranking|VentasExcel|PagosExcel|RankingExcel" }
            );

            // Alias REST (compat. MAT Web) → mismas acciones que /Admin/Reportes/...
            var reportesNs = new[] { "MAT.MVC.Controllers.Admin" };
            routes.MapRoute("ReportesAliasVentasExcel", "reportes/ventas/excel", new { controller = "Reportes", action = "VentasExcel" }, null, reportesNs);
            routes.MapRoute("ReportesAliasVentas", "reportes/ventas", new { controller = "Reportes", action = "Ventas" }, null, reportesNs);
            routes.MapRoute("ReportesAliasPagosExcel", "reportes/pagos/excel", new { controller = "Reportes", action = "PagosExcel" }, null, reportesNs);
            routes.MapRoute("ReportesAliasPagos", "reportes/pagos", new { controller = "Reportes", action = "Pagos" }, null, reportesNs);
            routes.MapRoute("ReportesAliasRankingExcel", "reportes/ranking-compras/excel", new { controller = "Reportes", action = "RankingExcel" }, null, reportesNs);
            routes.MapRoute("ReportesAliasRanking", "reportes/ranking-compras", new { controller = "Reportes", action = "Ranking" }, null, reportesNs);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}