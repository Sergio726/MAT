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
        readonly static int SAVE_PERIOD = 10 * 1000;// period=10 seconds
        readonly static int SAVE_COUNTER = 1000;// save after 1000 messages
        readonly static int MIN_IMPORTANCE = 0;// log only messages with importance value >=MIN_IMPORTANCE

        readonly static string DIR_LOG_FILES = @"c:\Reproisa\";

        static string _filename = DIR_LOG_FILES + @"Log." + DateTime.Now.ToString("yyMMdd.HHmm") + @".txt";

        readonly static List<string> _list_log = new List<string>();
        readonly static object _locker = new object();
        static int _counter = 0;
        static DateTime _last_save = DateTime.Now;

        public static void NewFile()
        {//new file is created because filename changed
            SaveToFile();
            lock (_locker)
            {

                _filename = DIR_LOG_FILES + @"Log." + DateTime.Now.ToString("yyMMdd.HHmm") + @".txt";
                _counter = 0;
            }
        }
        public static void Log(string LogMessage, int Importance)
        {
            if (Importance < MIN_IMPORTANCE) return;
            lock (_locker)
            {
                _list_log.Add(String.Format("{0:HH:mm:ss.ffff},{1},{2}", DateTime.Now, LogMessage, Importance));
                _counter++;
            }
            TimeSpan timeDiff = DateTime.Now - _last_save;

            if (_counter > SAVE_COUNTER || timeDiff.TotalMilliseconds > SAVE_PERIOD)
                SaveToFile();
        }

        public static void SaveToFile()
        {
            lock (_locker)
                if (_list_log.Count == 0)
                {
                    _last_save = _last_save = DateTime.Now;
                    return;
                }
            lock (_locker)
            {
                if (!File.Exists(_filename))
                {
                    File.CreateText(_filename);
                }
                using (StreamWriter logfile = File.AppendText(_filename))
                {

                    foreach (string s in _list_log) logfile.WriteLine(s);
                    logfile.Flush();
                    logfile.Close();
                }

                _list_log.Clear();
                _counter = 0;
                _last_save = DateTime.Now;
            }
        }


        public static void ReadLog(string logfile)
        {
            using (StreamReader r = File.OpenText(logfile))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
                r.Close();
            }
        }
    }
}
