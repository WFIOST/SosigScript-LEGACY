using BepInEx.Logging;

namespace SosigScript
{
    /// <summary>
    /// SosigScript logger, unused in script in favour of a scripts personal LogSource
    /// </summary>
    internal static class Logger
    {
        /// <summary>
        /// Prints a message to the console
        /// </summary>
        /// <param name="message">Message to print</param>
        /// <param name="level">LogLevel of the message</param>
        internal static void Print(object message, LogLevel level = LogLevel.Info) => SosigScript.Instance.Console.Log(level, message);

        internal static void Error(object message, LogLevel level = LogLevel.Error) => SosigScript.Instance.Console.Log(level, message);
        
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
            internal static void Print(object message) => SosigScript.Instance.Console.Log(LogLevel.Debug, message);
        }
    }
}