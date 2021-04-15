using System.Collections.Generic;
using BepInEx.Logging;
using Deli;
using MoonSharp.Interpreter;

namespace SosigScript
{
    public static class Executioner
    {
        public static IEnumerator<DynValue> ExecuteAsync(KeyValuePair<Mod, string> script)
        {
            var scriptLogger = new ManualLogSource($"SosigScript - {script.Key.Info.Name}");

            var loader = new Script
            {
                Options =
                {
                    DebugPrint = message => { scriptLogger.LogMessage(message); }
                }
            };

            yield return loader.DoString(script.Value);
        }
    }
}