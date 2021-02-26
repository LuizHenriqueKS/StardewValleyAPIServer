using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public class MonitorLog
    {
        public static IMonitor Monitor;

        public static void Log(string message, LogLevel level = LogLevel.Trace) {
            try
            {
                Monitor.Log(message, level);
            } catch (FileNotFoundException ex)
            {}
        }

        public static void Info(string message)
        {
            Log(message, LogLevel.Info);
        }
        public static void Error(string message)
        {
            Log(message, LogLevel.Error);
        }
    }
}
