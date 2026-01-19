using System.Web;
using System.Web.Mvc;
using MAT.MVC.Filters;

namespace MAT.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Guardrails y observabilidad
            filters.Add(new RequestGuardFilter(dbTimeoutSeconds: 120, slowRequestMs: 2000));

            // Manejo global de excepciones (HTML + AJAX/JSON)
            filters.Add(new GlobalExceptionFilter());

            // Fallback MVC (Error.cshtml) si no aplica nuestro filtro (por compatibilidad)
            filters.Add(new HandleErrorAttribute());
        }
    }
}