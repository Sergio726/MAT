using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MAT.Utilities;

namespace MAT.MVC.Infrastructure
{
    public static class SistemaParametroHelper
    {
        private static List<string> _cachedCodigos = null;
        private static DateTime _cacheTime = DateTime.MinValue;
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

        public static List<string> GetCodigosConfirmacion()
        {
            if (_cachedCodigos != null && (DateTime.Now - _cacheTime) < CacheDuration)
            {
                return _cachedCodigos;
            }

            _cachedCodigos = new List<string>();

            try
            {
                using (var reader = DBHelper.ExecuteDataReader("dbo.usp_MAT_SistemaParametro_GetAll", null))
                {
                    while (reader.Read())
                    {
                        var clave = reader["Clave"]?.ToString() ?? "";
                        if (clave.StartsWith("CodigoConfirmacion_"))
                        {
                            var valor = reader["Valor"]?.ToString();
                            var estaActivo = reader["EstaActivo"]?.ToString() == "1" || Convert.ToBoolean(reader["EstaActivo"]);
                            if (estaActivo && !string.IsNullOrEmpty(valor))
                            {
                                _cachedCodigos.Add(valor);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MATLogger.Log($"Error al obtener códigos de confirmación: {ex.Message}", 2);
            }

            _cacheTime = DateTime.Now;
            return _cachedCodigos;
        }

        public static bool ValidarCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return false;

            var codigos = GetCodigosConfirmacion();
            return codigos.Exists(c => c.Equals(codigo, StringComparison.Ordinal));
        }

        public static void ClearCache()
        {
            _cachedCodigos = null;
            _cacheTime = DateTime.MinValue;
        }
    }
}