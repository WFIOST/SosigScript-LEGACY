using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using BepInEx.Logging;
using Deli;
using Deli.Runtime;
using Deli.Setup;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Platforms;

using static SosigScript.Logger;

#region ERROR DISABLES
// ReSharper disable UnusedMember.local
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable EmptyConstructor
#pragma warning disable 8618
#endregion

namespace SosigScript
{
    public class SosigScript : DeliBehaviour
    {
        /// <summary>
        /// Instance of SosigScript, used in Stdlib.Meta
        /// </summary>
        public static SosigScript Instance { get; private set; }
        /// <summary>
        /// Global manual log source
        /// </summary>
        internal ManualLogSource Console { get; private set; }
        /// <summary>
        /// Library Loader instance
        /// </summary>
        public static LibraryLoader Libraries = new();
        /// <summary>
        /// Scriptloader, used in the Executioner, libraries and LibraryLoader
        /// </summary>
        public Script ScriptLoader { get; private set; }

        /// <summary>
        /// Script executioner
        /// </summary>
        public List<Executioner> ActiveScripts { get; internal set; }

        public SosigScript()
        {
            //Set Console log
            Console = BepInEx.Logging.Logger.CreateLogSource("SosigScript");
            
            Logger.LogInfo("Initialising SosigScript");
            //Start up MoonSharp so scripts load faster
            Logger.LogDebug("Warming up scripts");
            Script.WarmUp();
            //Give it standard platform settings
            Logger.LogDebug("Setting Platform accessors");
            Script.GlobalOptions.Platform = new StandardPlatformAccessor();
            //Make a new manual log source specifically for checking if MoonSharp is initialised
            Logger.LogDebug("Setting DebugPrint logsource");
            Script.DefaultOptions.DebugPrint = message => { BepInEx.Logging.Logger.CreateLogSource("SosigScript (INITIALISATION)").LogInfo(message); };
            //Run a print command!
            Script.RunString("print('SosigScript initialised! Hello from Lua!')");
            Logger.LogDebug("Subscribing to the AssetLoader events");
            Stages.Setup    += RegisterLibraries;
            Stages.Runtime  += RegisterScripts;
            //We set the "Soft Sandbox" so user has more options in their scripts
            ScriptLoader = new Script(CoreModules.Preset_SoftSandbox);

            ActiveScripts = new List<Executioner>();
            Instance = this;
        }

        private void Awake()
        {
            foreach (var script in ActiveScripts.Where(script => script is not null))
            {
                script.ExecuteFunction("Awake", null);
            }
        }

        private void Update()
        {
            foreach (var script in ActiveScripts.Where(script => script is not null))
            {
                script.ExecuteFunction("Update", null, true);
            }
        }

        private void RegisterScripts(RuntimeStage stage)
        {
            Debug.Print("Loading Scripts");
            stage.RuntimeAssetLoaders[Source, "script"] = new ScriptLoader().LoadScripts;
        }

        private void RegisterLibraries(SetupStage stage)
        {
            Debug.Print("Loading Libraries");
            stage.SetupAssetLoaders[Source, "library"] = Libraries.LoadAssembly;
        }
    }
}