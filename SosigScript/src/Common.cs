using BepInEx.Logging;

namespace SosigScript
{
    public static class Common
    {
        /// <summary>
        /// Location of the BepInEx Plugins directory (BepInEx/Plugins)
        /// </summary>
        public const string PLUGINS_DIR = "BepInEx/Plugins";
        
        /// <summary>
        /// SosigScript logger, unused in script in favour of a scripts personal LogSource
        /// </summary>
        public static class Logger
        {
            /// <summary>
            /// Prints a message to the console
            /// </summary>
            /// <param name="message">Message to print</param>
            /// <param name="level">LogLevel of the message</param>
            internal static void Print(object message, LogLevel level = LogLevel.Info) => SosigScript.Console.Log(level, message);

            internal static void Error(object message, LogLevel level = LogLevel.Error) => SosigScript.Console.Log(level, message);
        
            /// <summary>
            /// Used for debugging
            /// </summary>
            internal static class Debug
            {
                /// <summary>
                /// Prints a debug message to the console
                /// </summary>
                /// <param name="message">Message to print
                ///
                /// </param>
                internal static void Print(object message) => SosigScript.Console.Log(LogLevel.Debug, message);
            }
        }
        
        public static class PluginInfo
        {
            public const string GUID    = "com.wfiost.sosigscript";
            public const string NAME    = "SosigScript";
            public const string VERSION = "1.0.0";
        }
    }
}