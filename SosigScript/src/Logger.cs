using BepInEx.Logging;

namespace SosigScript
{
    internal static class Logger
    {
        internal static void Print(object message, LogLevel level = LogLevel.Info) => Plugin.Console.Log(level, message);
        

        internal static class Debug
        {
            internal static void Print(object message) => Plugin.Console.Log(LogLevel.Debug, message);
        }
    }
}