using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityAndSeek.Server
{
    public static class Debug
    {
        public static void Log(LogLevel level, string message)
        {
            Console.WriteLine(string.Format("[{0}/{1}] {2}", DateTime.Now, level.ToString(), message));
        }

        public static void LogDebug(string message)
        {
            Log(LogLevel.Debug, message);
        }

        public static void LogInfo(string message)
        {
            Log(LogLevel.Info, message);
        }

        public static void LogWarning(string message)
        {
            Log(LogLevel.Warning, message);
        }

        public static void LogError(string message)
        {
            Log(LogLevel.Error, message);
        }

        public enum LogLevel
        {
            Debug,
            Info,
            Warning,
            Error
        }
    }
}
