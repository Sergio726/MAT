using MAT.MVC.Infrastructure;
using MAT.Utilities;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;

namespace MAT.MVC.Filters
{
    /// <summary>
    /// Guardrails por request:
    /// - CorrelationId
    /// - Stopwatch de performance
    /// - Timeout DB por request (se consume desde DBHelper via HttpContext.Items)
    /// </summary>
    public class RequestGuardFilter : ActionFilterAttribute
    {
        private const string DbTimeoutKey = "MAT.DbCommandTimeoutSeconds";
        private readonly int _dbTimeoutSeconds;
        private readonly int _slowRequestMs;

        public RequestGuardFilter(int dbTimeoutSeconds = 120, int slowRequestMs = 2000)
        {
            _dbTimeoutSeconds = dbTimeoutSeconds;
            _slowRequestMs = slowRequestMs;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var correlationId = RequestContext.GetOrCreateCorrelationId();
            RequestContext.StartRequestStopwatch();

            // Timeout DB por request (solo afecta cuando hay HttpContext)
            filterContext.HttpContext.Items[DbTimeoutKey] = _dbTimeoutSeconds;
            filterContext.HttpContext.Items["MAT.CorrelationId"] = correlationId;

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var sw = RequestContext.GetRequestStopwatch();
            if (sw != null)
            {
                sw.Stop();
                if (sw.ElapsedMilliseconds >= _slowRequestMs)
                {
                    try
                    {
                        var correlationId = RequestContext.GetOrCreateCorrelationId();
                        var route = filterContext.RouteData?.Values;
                        var controller = route?["controller"]?.ToString();
                        var action = route?["action"]?.ToString();
                        var url = filterContext.HttpContext?.Request?.RawUrl;

                        MATLogger.Log($"[{correlationId}] SLOW request {sw.ElapsedMilliseconds}ms: {controller}/{action} {url}", 1);
                    }
                    catch { /* no-op */ }
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}

