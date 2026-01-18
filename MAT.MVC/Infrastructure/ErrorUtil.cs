using MAT.Utilities;
using System;
using System.Configuration;

namespace MAT.MVC.Infrastructure
{
    public static class ErrorUtil
    {
        public static bool IsDev()
        {
            try
            {
                return string.Equals(ConfigurationManager.AppSettings["SystemDEV"], "true", StringComparison.OrdinalIgnoreCase);
            }
            catch { return false; }
        }

        /// <summary>
        /// Loguea la excepción con CorrelationId y devuelve un mensaje "safe" para UI/JSON.
        /// No incluye StackTrace para usuarios.
        /// </summary>
        public static string LogAndGetPublicMessage(Exception ex, string context)
        {
            var correlationId = RequestContext.GetOrCreateCorrelationId();
            try
            {
                MATLogger.Log($"[{correlationId}] {context} - {ex.GetType().Name}: {ex.Message}", 1);
                MATLogger.Log($"[{correlationId}] {ex.StackTrace}", 1);
            }
            catch { /* no-op */ }

            if (IsDev())
                return $"[{correlationId}] {ex.Message}";

            return $"Ocurrió un error. ID: {correlationId}";
        }
    }
}

