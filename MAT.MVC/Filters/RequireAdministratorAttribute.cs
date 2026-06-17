using System;
using System.Text;
using System.Web.Mvc;
using MAT.MVC.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MAT.MVC.Filters
{
    /// <summary>
    /// Restringe acciones del panel Admin al rol Administrador.
    /// Redirige a Login (no autenticado) o Home (sin rol) en vistas HTML.
    /// En JSON/Excel responde con códigos HTTP y cuerpo acorde al tipo de acción.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class RequireAdministratorAttribute : ActionFilterAttribute
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Include
        };

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
                return;

            var http = filterContext.HttpContext;
            if (AdminAuthorizationHelper.IsAdministrator(http))
                return;

            var message = !AdminAuthorizationHelper.IsAuthenticated(http)
                ? "Debe iniciar sesión."
                : "No tiene permisos para acceder a esta sección.";
            var statusCode = !AdminAuthorizationHelper.IsAuthenticated(http) ? 401 : 403;
            var redirectToLogin = !AdminAuthorizationHelper.IsAuthenticated(http);

            var actionName = filterContext.ActionDescriptor.ActionName ?? "";

            if (string.Equals(actionName, "LogsRaw", StringComparison.OrdinalIgnoreCase))
            {
                filterContext.HttpContext.Response.StatusCode = statusCode;
                filterContext.Result = new ContentResult
                {
                    Content = message,
                    ContentType = "text/plain",
                    ContentEncoding = Encoding.UTF8
                };
                return;
            }

            if (IsExcelAction(actionName, filterContext))
            {
                filterContext.HttpContext.Response.StatusCode = statusCode;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.Result = new HttpStatusCodeResult(statusCode, message);
                return;
            }

            if (IsJsonAction(actionName, filterContext))
            {
                filterContext.HttpContext.Response.StatusCode = statusCode;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                var json = JsonConvert.SerializeObject(new { ok = false, message, data = (object)null }, JsonSettings);
                filterContext.Result = new ContentResult
                {
                    Content = json,
                    ContentType = "application/json",
                    ContentEncoding = Encoding.UTF8
                };
                return;
            }

            if (redirectToLogin)
            {
                var url = filterContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Login", returnUrl = url }));
                return;
            }

            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary(new { controller = "Home", action = "Index" }));
        }

        private static bool IsJsonAction(string actionName, ActionExecutingContext filterContext)
        {
            if (string.Equals(actionName, "AuditFactura", StringComparison.OrdinalIgnoreCase))
                return true;
            if (string.Equals(actionName, "Ventas", StringComparison.OrdinalIgnoreCase)
                || string.Equals(actionName, "Pagos", StringComparison.OrdinalIgnoreCase)
                || string.Equals(actionName, "Ranking", StringComparison.OrdinalIgnoreCase)
                || string.Equals(actionName, "BuscarViajes", StringComparison.OrdinalIgnoreCase))
                return true;
            if (actionName.EndsWith("Json", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        private static bool IsExcelAction(string actionName, ActionExecutingContext filterContext)
        {
            return actionName.EndsWith("Excel", StringComparison.OrdinalIgnoreCase);
        }
    }
}
