using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Caching;
using AutoMapper;
using MAT.MVC.Integration;
using MAT.MVC.Models;
using MAT.Utilities;
using WebMatrix.WebData;
using MatRequestContext = MAT.MVC.Infrastructure.RequestContext;

namespace MAT.MVC
{
    // Nota: para obtener instrucciones sobre cómo habilitar el modo clásico de IIS6 o IIS7, 
    // visite http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private const string CacheKeyMarcarExpirados = "Presupuesto_MarcarExpirados_LastRun";

        protected void Application_Start()
        {
            // Inicialización de AutoMapper
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<MappingProfile>(); // Agrega tu perfil
            });

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Marcar presupuestos expirados al iniciar la aplicación
            try
            {
                var count = PresupuestoMethod.MarcarExpirados();
                if (count > 0)
                {
                    MATLogger.Log($"Presupuesto: {count} presupuesto(s) marcado(s) como expirado(s) al iniciar la aplicación.", 0);
                }
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Presupuesto: Error al marcar expirados en Application_Start: {ex.Message}", 1);
            }
        }

        protected void Application_BeginRequest()
        {
            // CorrelationId + timer para logging/performance
            MatRequestContext.GetOrCreateCorrelationId();
            MatRequestContext.StartRequestStopwatch();

            // Marcar presupuestos expirados (máximo una vez por hora)
            try
            {
                var cache = HttpRuntime.Cache;
                var lastRun = cache[CacheKeyMarcarExpirados] as DateTime?;
                if (lastRun == null || (DateTime.UtcNow - lastRun.Value).TotalHours >= 1)
                {
                    cache.Insert(CacheKeyMarcarExpirados, DateTime.UtcNow, null, DateTime.UtcNow.AddHours(1), Cache.NoSlidingExpiration);
                    var count = PresupuestoMethod.MarcarExpirados();
                    if (count > 0)
                    {
                        MATLogger.Log($"Presupuesto: {count} presupuesto(s) marcado(s) como expirado(s).", 0);
                    }
                }
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Presupuesto: Error al marcar expirados: {ex.Message}", 1);
            }
        }

        protected void Application_EndRequest()
        {
            var sw = MatRequestContext.GetRequestStopwatch();
            if (sw == null) return;

            sw.Stop();
            var thresholdMs = 2000;
            try
            {
                var setting = System.Configuration.ConfigurationManager.AppSettings["SlowRequestMs"];
                if (int.TryParse(setting, out var ms) && ms > 0) thresholdMs = ms;
            }
            catch { /* ignore */ }

            if (sw.ElapsedMilliseconds >= thresholdMs)
            {
                try
                {
                    var correlationId = MatRequestContext.GetOrCreateCorrelationId();
                    var url = HttpContext.Current?.Request?.RawUrl;
                    var method = HttpContext.Current?.Request?.HttpMethod;
                    var status = HttpContext.Current?.Response?.StatusCode;
                    MATLogger.Log($"[{correlationId}] SLOW EndRequest {sw.ElapsedMilliseconds}ms {method} {url} -> {status}", 1);
                }
                catch { /* ignore */ }
            }
        }

        protected void Application_Error()
        {
            // Captura de excepciones no controladas fuera del pipeline MVC
            try
            {
                var ex = Server.GetLastError();
                if (ex == null) return;

                var correlationId = MatRequestContext.GetOrCreateCorrelationId();
                MATLogger.Log($"[{correlationId}] Application_Error: {ex.GetType().Name} - {ex.Message}", 1);
                MATLogger.Log($"[{correlationId}] {ex.StackTrace}", 1);
            }
            catch { /* no-op */ }
        }

        protected void Application_End()
        {
            MATLogger.SaveToFile();
        }

    }
}