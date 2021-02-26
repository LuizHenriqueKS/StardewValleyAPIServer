using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIServer.core
{
    public class Log
    {
        public static bool WriteDebugLog;

        public static void Error(String message)
        {
            Console.Error.WriteLine(message);
            MError(message);
        }
        public static void Info(String message)
        {
            Console.WriteLine(message);
            MInfo(message);
        }

        public static void Debug(String message)
        {
            if (Log.WriteDebugLog)
            {
                Console.WriteLine(message);
                MInfo(message);
            }
        }

        public static void MInfo(string message)
        {
            try
            {
                MonitorLog.Info(message);
            }
            catch (FileNotFoundException)
            {
            }
            catch (TypeLoadException)
            {

            }

        }

        public static void MError(string message)
        {
            try
            {
                MonitorLog.Error(message);
            }
            catch (FileNotFoundException)
            {
            }
            catch (TypeLoadException)
            {

            }
        }

    }
}
