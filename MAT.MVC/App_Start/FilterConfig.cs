using System.Web;
using System.Web.Mvc;
using MAT.MVC.Filters;

namespace MAT.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Todo exige sesión salvo lo marcado con [AllowAnonymous] (2026-11-07). Antes cada acción dependía de su
            // propio [Authorize] y ~150 acciones en 25 controladores respondían sin sesión: listado de viajes,
            // detalle de factura, y también escrituras (ReservarPasajes, EliminarVenta, CancelarPago, DeleteViaje…).
            // Públicos a propósito: AccountController (Login, Register, RegistrarVendedor, LogOff), ErrorController
            // y ExternalController.PackagesGetAll (paquetes para la web), todos con [AllowAnonymous].
            // Una acción nueva que tenga que ser pública necesita [AllowAnonymous] explícito.
            filters.Add(new AuthorizeAttribute());

            // Guardrails y observabilidad
            filters.Add(new RequestGuardFilter(dbTimeoutSeconds: 120, slowRequestMs: 2000));

            // Manejo global de excepciones (HTML + AJAX/JSON)
            filters.Add(new GlobalExceptionFilter());

            // Fallback MVC (Error.cshtml) si no aplica nuestro filtro (por compatibilidad)
            filters.Add(new HandleErrorAttribute());
        }
    }
}