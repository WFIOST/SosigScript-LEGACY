using BepInEx.Logging;
using Deli;
using Deli.Runtime;
using Deli.Setup;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Platforms;

#region ERROR DIABLES
// ReSharper disable UnusedMember.local
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable EmptyConstructor
#endregion

namespace SosigScript
{
    public class Plugin : DeliBehaviour
    {
        public Plugin()
        {
            Logger.LogInfo("Initialising SosigScript");
            //Start up MoonSharp so scripts load faster
            Script.WarmUp();
            //Give it standard platform settings
            Script.GlobalOptions.Platform = new StandardPlatformAccessor();
            //Make a new manual log source specifically for checking if MoonSharp is initialised
            Script.DefaultOptions.DebugPrint = message => { new ManualLogSource("SosigScript").LogInfo(message); };
            //Run a print command!
            Script.RunString("print('SosigScript initialised! Hello from Lua!')");
        }

        internal static ManualLogSource Console { get; set; } = new("SosigScript");

        private void Awake()
        {
            Logger.LogInfo("Started SosigScript!");
        }

        private void Register(RuntimeStage stage)
        {
            stage.RuntimeAssetLoaders[Source, "SosigScript"] = new ScriptLoader().LoadScripts;
        }
    }
}