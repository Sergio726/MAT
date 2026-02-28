using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAT.Utilities
{
    public static class MATLogger
    {
        readonly static int SAVE_PERIOD = 10 * 1000;
        readonly static int SAVE_COUNTER = 200;
        readonly static int MIN_IMPORTANCE = 0;
        readonly static int MAX_IN_MEMORY = 2000;

        readonly static List<string> _list_log = new List<string>();
        readonly static object _locker = new object();
        static int _counter = 0;
        static DateTime _last_save = DateTime.Now;

        private static string GetLogDirectory()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                return Path.Combine(baseDir, "App_Data", "logs");
            }
            catch
            {
                return null;
            }
        }

        public static void NewFile()
        {
            SaveToFile();
            lock (_locker)
            {
                _counter = 0;
            }
        }

        public static void Log(string LogMessage, int Importance)
        {
            if (Importance < MIN_IMPORTANCE) return;
            var entry = String.Format("{0:yyyy-MM-dd HH:mm:ss.ffff},{1},{2}", DateTime.Now, LogMessage, Importance);
            lock (_locker)
            {
                _list_log.Add(entry);
                _counter++;
            }
            TimeSpan timeDiff = DateTime.Now - _last_save;

            if (_counter > SAVE_COUNTER || timeDiff.TotalMilliseconds > SAVE_PERIOD)
                SaveToFile();
        }

        public static void SaveToFile()
        {
            List<string> toWrite;
            lock (_locker)
            {
                if (_list_log.Count == 0)
                {
                    _last_save = DateTime.Now;
                    return;
                }
                toWrite = new List<string>(_list_log);
                if (_list_log.Count > MAX_IN_MEMORY)
                    _list_log.RemoveRange(0, _list_log.Count - MAX_IN_MEMORY);
                _counter = 0;
                _last_save = DateTime.Now;
            }

            try
            {
                string logDir = GetLogDirectory();
                if (string.IsNullOrEmpty(logDir)) return;
                if (!Directory.Exists(logDir)) Directory.CreateDirectory(logDir);
                string logFile = Path.Combine(logDir, $"mat-log-{DateTime.Now:yyyy-MM-dd}.txt");
                File.AppendAllLines(logFile, toWrite, Encoding.UTF8);
            }
            catch { /* no-op: logging nunca debe romper la app */ }
        }

        /// <summary>Devuelve las últimas N entradas del log en memoria.</summary>
        public static List<string> GetRecentLogs(int count = 500)
        {
            lock (_locker)
            {
                int skip = Math.Max(0, _list_log.Count - count);
                return _list_log.Skip(skip).ToList();
            }
        }


        public static void ReadLog(string logfile)
        {
            // Deshabilitado: No se leen archivos físicos de log
            // Los logs ahora se mantienen solo en memoria
            Console.WriteLine("ReadLog: La lectura de archivos de log está deshabilitada. Los logs se mantienen solo en memoria.");
        }

        public static string FormatMessageToHtml(string message)
        {
            message = message.Replace(Environment.NewLine, "<br>").Replace("\n", "<br>");

            return message;
        }
        public static string FormatExceptionToHtml(Exception e, string customMessage = "")
        {
            StringBuilder messageBuilder = new StringBuilder();
            messageBuilder.Append("<div>");            
            messageBuilder.AppendLine("Detail Error");
            if (customMessage != "")
            {
                messageBuilder.AppendLine("Custom message:");
                messageBuilder.Append("<div style=\"margin-left: 20px\">");
                messageBuilder.Append(customMessage);
                messageBuilder.Append("</div>");                
            }
            messageBuilder.AppendLine("Message:");
            messageBuilder.Append("<div style=\"margin-left: 20px\">");
            messageBuilder.Append(e.Message);
            messageBuilder.Append("</div>");

            messageBuilder.AppendLine("Stack Trace:");
            messageBuilder.Append("<div style=\"margin-left: 20px\">");
            messageBuilder.Append(e.StackTrace);
            messageBuilder.Append("</div>");

            messageBuilder.Append("</div>");
            string message = messageBuilder.ToString();
            message = message.Replace(Environment.NewLine, "<br>").Replace("\n", "<br>");

            return message;
        }

        private static string ReplaceNewLine(string line)
        {
            line = line.Replace(Environment.NewLine, "<br>").Replace("\n", "<br>");
            return line;
        }
    }
}
