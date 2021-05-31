using System.Collections.Generic;
using BepInEx.Logging;
using Deli;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;

using static SosigScript.Logger;

namespace SosigScript
{
    public class Executioner
    {
        /// <summary>
        /// Executes specified script
        /// </summary>
        /// <param name="script">KeyValuePair of Mod (Class), String (the raw script itself)</param>
        /// <returns>Return value of the script (also in the ReturnValues dictionary)</returns>
        public IEnumerator<DynValue> Execute(KeyValuePair<Mod, string> script)
        {
            Debug.Print($"Executing mod {script.Key.Info.Name}");
            
            var scriptLogger = BepInEx.Logging.Logger.CreateLogSource($"SosigScript - {script.Key.Info.Name}");

            var scriptLoader = SosigScript.Instance.ScriptLoader;
            
            Debug.Print("Assigning the DebugPrint to the LogScource");
            scriptLoader.Options.DebugPrint = message => scriptLogger.LogInfo(message);

          
            
            Debug.Print("Running the script");
            DynValue result;
            yield return result = scriptLoader.DoString(script.Value);

            Print($"Script {script.Key.Info.Name} exited with return {result.ToString()}");
        }
    }
}