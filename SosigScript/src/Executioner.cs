using System.Collections.Generic;
using BepInEx.Logging;
using Deli;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;

namespace SosigScript
{
    public class Executioner
    {
        public List<DynValue> ReturnValues = new List<DynValue>();

        public IEnumerator<DynValue> Execute(KeyValuePair<Mod, string> script)
        {
            var scriptLogger = new ManualLogSource($"SosigScript - {script.Key.Info.Name}");

            var scriptLoader = SosigScript.SosigScriptInstance.ScriptLoader;
            
            scriptLoader.Options.DebugPrint = message => scriptLogger.LogInfo(message);

            DynValue result;
            
            yield return result = scriptLoader.DoString(script.Value);
            
            ReturnValues.Add(result);
        }
    }
}