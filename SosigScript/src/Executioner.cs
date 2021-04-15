using System.Collections;
using System.Collections.Generic;
using BepInEx.Logging;
using MoonSharp.Interpreter;

using static SosigScript.Logger;

namespace SosigScript
{
    public static class Executioner
    {
        public static IEnumerator<DynValue> ExecuteAsync(KeyValuePair<string, string> script)
        {

            var scriptLogger = new ManualLogSource($"SosigScript - {script.Key}");

            var loader = new Script
            {
                Options =
                {
                    DebugPrint = message => { scriptLogger.LogMessage(message); }
                },
            };

            yield return loader.DoString(script.Value);
        }
    }
}