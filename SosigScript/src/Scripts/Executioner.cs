using System;
using System.Collections.Generic;
using BepInEx.Logging;
using Deli;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using MoonSharp.Interpreter.Serialization.Json;
using static SosigScript.Common.Logger;

namespace SosigScript.ScriptLoader
{
    /// <summary>
    /// Executes scripts from memory
    /// <remarks>Scripts are loaded in memory via ScriptLoader until the instance of the class is destroyed.</remarks>
    /// </summary>
    public class Executioner
    {
        private ManualLogSource _logger;
        private Script _script;
        
        /// <summary>
        /// Executes specified script
        /// </summary>
        /// <param name="script">KeyValuePair of Mod (Class), String (the raw script itself)</param>
        /// <returns>Return value of the script (also in the ReturnValues dictionary)</returns>
        public Executioner(KeyValuePair<Mod, string> script)
        {
            Debug.Print($"Executing mod {script.Key.Info.Name}");
            
            _logger = BepInEx.Logging.Logger.CreateLogSource($"SosigScript - {script.Key.Info.Name}");

            _script = SosigScript.Instance.ScriptLoader;
            
            Debug.Print("Assigning the DebugPrint to the LogSource");
            _script.Options.DebugPrint = message => _logger.LogInfo(message);

            Debug.Print("Running the script");
            var result = _script.DoString(script.Value);
        }

        /// <summary>
        /// Executes the specified function
        /// </summary>
        /// <remarks>
        /// This function is unsafe and literally relies on try-catch
        /// TODO: Find better way to implement the checks for this
        /// </remarks>
        /// <param name="function">Function to execute, must be in the script's globals</param>
        /// <param name="arguments">Arguments to pass to the function</param>
        /// <param name="isUpdate">Defines if the function is the "update" function or similar, stopping error logging if the function is not there</param>
        /// <returns>the return value of the function, if it was successfully executed, if not, returns null</returns>
        public DynValue? ExecuteFunction(string function, object? arguments, bool isUpdate = false)
        {
            DynValue? ret = null;
            
            try
            {
                //Check if the arguments are null to avoid nullref exception
                ret = arguments is not null ? _script.Call(_script.Globals[function], arguments) : _script.Call(_script.Globals[function]);
            }
            catch (Exception e)
            {
                if(!isUpdate)
                    _logger.LogError($"Could not call function {function}!\nAre you sure that it is registered in the script's globals?");
            }

            return ret;
        }


        public IEnumerator<DynValue> ExecuteCoroutine(string function, object? arguments)
        {
            return null; //TODO: Finish Coroutines
        }
    }
}