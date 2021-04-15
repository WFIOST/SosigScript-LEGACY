using System;
using BepInEx.Logging;

namespace SosigScript
{
    public static class Logger
    { public static void Print(object message, LogLevel level = LogLevel.Info) => Plugin.Console.Log(level, message); }
}