using System.Linq;
using SosigScript.Common;
using Deli.Setup;
using MoonSharp.Interpreter;

namespace SosigScript
{
    public class SosigScript : DeliBehaviour
    {
        private static ScriptLoader _scriptLoader = new();
        
        private void Awake()
        {
            Logger.LogInfo("Started SosigScript!");

            Logger.LogInfo("Loaded mods: ");
            
            foreach (var script in _scriptLoader.Scripts)
            {
                Logger.LogInfo(script.Key);
            }

            Logger.LogInfo("Running scripts...");
            
            foreach 
            (
                var result 
                in _scriptLoader
                    .Scripts
                    .Select
                    (
                        script => 
                            Script.RunString
                            (
                                script.Value
                            )
                    )
            )
            {
                Logger.LogInfo(result.String);
            }

        }
        
        
        
    }
}