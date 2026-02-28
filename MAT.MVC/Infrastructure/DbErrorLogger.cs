using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MAT.MVC.Infrastructure
{
    /// <summary>
    /// Registra errores en la tabla [dbo].[ErrorLog] de la base de datos principal.
    /// Si la inserción en DB falla, escribe en el Windows Event Log como respaldo.
    /// Nunca lanza excepciones — no puede romper el flujo del request.
    /// </summary>
    public static class DbErrorLogger
    {
        private const string EventSourceName = "MAT.MVC";
        private const string SpInsert        = "usp_MAT_ErrorLog_Insert";
        private const string SpGetRecent     = "usp_MAT_ErrorLog_GetRecent";

        // ------------------------------------------------------------------ //
        //  API pública
        // ------------------------------------------------------------------ //

        public static void Log(
            string correlationId,
            string tipo,
            string mensaje,
            string stackTrace,
            string url      = null,
            string usuario  = null,
            int    importancia = 1)
        {
            try
            {
                InsertDb(correlationId, tipo, mensaje, stackTrace, url, usuario, importancia);
            }
            catch (Exception dbEx)
            {
                FallbackEventLog(correlationId, tipo, mensaje, stackTrace, dbEx);
            }
        }

        public static void Log(
            string correlationId,
            Exception ex,
            string url     = null,
            string usuario = null,
            int importancia = 1)
        {
            if (ex == null) return;
            Log(
                correlationId,
                ex.GetType().FullName,
                ex.Message,
                ex.ToString(),
                url,
                usuario,
                importancia);
        }

        public static DataTable GetRecent(
            int    top           = 100,
            string correlationId = null,
            DateTime? fechaDesde = null,
            int?   importancia   = null)
        {
            try
            {
                return QueryDb(top, correlationId, fechaDesde, importancia);
            }
            catch
            {
                return new DataTable();
            }
        }

        // ------------------------------------------------------------------ //
        //  Implementación privada
        // ------------------------------------------------------------------ //

        private static string ConnectionString =>
            ConfigurationManager.ConnectionStrings["MAT.Data.ConnectionString"]?.ConnectionString;

        private static void InsertDb(
            string correlationId,
            string tipo,
            string mensaje,
            string stackTrace,
            string url,
            string usuario,
            int importancia)
        {
            var cs = ConnectionString;
            if (string.IsNullOrEmpty(cs)) return;

            using (var cn = new SqlConnection(cs))
            using (var cmd = new SqlCommand(SpInsert, cn))
            {
                cmd.CommandType    = CommandType.StoredProcedure;
                cmd.CommandTimeout = 5;

                cmd.Parameters.Add(new SqlParameter("@CorrelationId", SqlDbType.VarChar,  50)   { Value = DbVal(correlationId) });
                cmd.Parameters.Add(new SqlParameter("@Tipo",          SqlDbType.VarChar,  200)  { Value = DbVal(tipo) });
                cmd.Parameters.Add(new SqlParameter("@Mensaje",       SqlDbType.NVarChar, -1)   { Value = DbVal(mensaje) });
                cmd.Parameters.Add(new SqlParameter("@StackTrace",    SqlDbType.NVarChar, -1)   { Value = DbVal(stackTrace) });
                cmd.Parameters.Add(new SqlParameter("@Url",           SqlDbType.NVarChar, 1000) { Value = DbVal(url) });
                cmd.Parameters.Add(new SqlParameter("@Usuario",       SqlDbType.NVarChar, 200)  { Value = DbVal(usuario) });
                cmd.Parameters.Add(new SqlParameter("@Importancia",   SqlDbType.Int)             { Value = importancia });

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private static DataTable QueryDb(
            int top,
            string correlationId,
            DateTime? fechaDesde,
            int? importancia)
        {
            var cs = ConnectionString;
            if (string.IsNullOrEmpty(cs)) return new DataTable();

            using (var cn = new SqlConnection(cs))
            using (var cmd = new SqlCommand(SpGetRecent, cn))
            {
                cmd.CommandType    = CommandType.StoredProcedure;
                cmd.CommandTimeout = 10;

                cmd.Parameters.Add(new SqlParameter("@Top",           SqlDbType.Int)             { Value = top });
                cmd.Parameters.Add(new SqlParameter("@CorrelationId", SqlDbType.VarChar, 50)    { Value = DbVal(correlationId) });
                cmd.Parameters.Add(new SqlParameter("@FechaDesde",    SqlDbType.DateTime)        { Value = fechaDesde.HasValue ? (object)fechaDesde.Value : DBNull.Value });
                cmd.Parameters.Add(new SqlParameter("@Importancia",   SqlDbType.Int)             { Value = importancia.HasValue  ? (object)importancia.Value  : DBNull.Value });

                cn.Open();
                using (var da = new SqlDataAdapter(cmd))
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        private static void FallbackEventLog(
            string correlationId,
            string tipo,
            string mensaje,
            string stackTrace,
            Exception dbEx)
        {
            try
            {
                if (!EventLog.SourceExists(EventSourceName))
                    EventLog.CreateEventSource(EventSourceName, "Application");

                string entry =
                    $"[MAT] CorrelationId={correlationId}\r\n" +
                    $"Tipo:       {tipo}\r\n" +
                    $"Mensaje:    {mensaje}\r\n" +
                    $"StackTrace: {stackTrace}\r\n" +
                    $"--- Error al guardar en BD: {dbEx.Message} ---";

                EventLog.WriteEntry(EventSourceName, entry.Substring(0, Math.Min(entry.Length, 31000)),
                    EventLogEntryType.Error);
            }
            catch
            {
                // Si el Event Log tampoco funciona (permisos insuficientes), no hay nada más que hacer.
            }
        }

        private static object DbVal(string value) =>
            string.IsNullOrEmpty(value) ? (object)DBNull.Value : value;
    }
}
