using System;
using System.Diagnostics;
using System.Web;

namespace MAT.MVC.Infrastructure
{
    public static class RequestContext
    {
        private const string CorrelationIdKey = "MAT.CorrelationId";
        private const string StopwatchKey = "MAT.RequestStopwatch";

        public static string GetOrCreateCorrelationId()
        {
            var ctx = HttpContext.Current;
            if (ctx == null) return Guid.NewGuid().ToString("N");

            if (ctx.Items[CorrelationIdKey] is string existing && !string.IsNullOrWhiteSpace(existing))
                return existing;

            var id = Guid.NewGuid().ToString("N");
            ctx.Items[CorrelationIdKey] = id;
            return id;
        }

        public static Stopwatch StartRequestStopwatch()
        {
            var ctx = HttpContext.Current;
            var sw = Stopwatch.StartNew();
            if (ctx != null)
            {
                ctx.Items[StopwatchKey] = sw;
            }
            return sw;
        }

        public static Stopwatch GetRequestStopwatch()
        {
            var ctx = HttpContext.Current;
            return ctx?.Items[StopwatchKey] as Stopwatch;
        }
    }
}
