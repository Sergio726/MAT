using MAT.MVC.Infrastructure;
using MAT.Utilities;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MAT.MVC.Filters
{
    /// <summary>
    /// Manejo global de excepciones para MVC:
    /// - Log con correlationId
    /// - Respuesta JSON estándar para AJAX
    /// - Vista de error amigable para requests normales
    /// </summary>
    public class GlobalExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null || filterContext.ExceptionHandled) return;

            var ex = filterContext.Exception;
            var correlationId = RequestContext.GetOrCreateCorrelationId();

            // Log (sin romper el request)
            try
            {
                MATLogger.Log($"[{correlationId}] Unhandled exception: {ex.GetType().Name} - {ex.Message}", 1);
                MATLogger.Log($"[{correlationId}] {ex.StackTrace}", 1);
            }
            catch { /* no-op */ }

            DbErrorLogger.Log(
                correlationId,
                ex,
                url:      filterContext.HttpContext?.Request?.Url?.ToString(),
                usuario:  filterContext.HttpContext?.User?.Identity?.Name);

            var isAjax = filterContext.HttpContext?.Request?.IsAjaxRequest() == true
                         || string.Equals(filterContext.HttpContext?.Request?.Headers?["X-Requested-With"], "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);

            var isDev = string.Equals(System.Configuration.ConfigurationManager.AppSettings["SystemDEV"], "true", StringComparison.OrdinalIgnoreCase);

            if (isAjax)
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        ok = false,
                        message = isDev ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente.",
                        correlationId
                    }
                };
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                filterContext.ExceptionHandled = true;
                return;
            }

            // Request normal: enviar a una página amigable (no exponer stacktrace salvo DEV)
            filterContext.Controller.TempData["Error"] = isDev
                ? $"[{correlationId}] {ex.Message}"
                : $"Ocurrió un error inesperado. ID: {correlationId}";

            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml",
                ViewData = new ViewDataDictionary(ex)
            };

            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            filterContext.ExceptionHandled = true;
        }
    }
}

