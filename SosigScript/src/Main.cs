using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
#endregion

namespace SosigScript
{
    public class Plugin : DeliBehaviour
    {
        internal static ManualLogSource Console { get; private set; } = new("SosigScript");

        public static LibraryLoader Libraries = new LibraryLoader();
        
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

        private void Awake()
        {
            Print("Loading libraries");
            Libraries.LoadAllAssemblyTypes();
            Print($"Loaded {Libraries.LoadedAssemblies.ToList().Count} Libraries!");
        }

        /// <summary>
        /// Here we register the assetloaders for SosigScript
        /// Currently there is 2 loaders:
        /// 1. Lua script ("SosigScript")
        /// 2. SosigScript library ("SosigScript.Library")
        /// </summary>
        private void Register(RuntimeStage stage)
        {
            Type cum, shit, piss;
            
            stage.RuntimeAssetLoaders[Source, "Script"] = new ScriptLoader().LoadScripts;
    
            stage.RuntimeAssetLoaders[Source, "Library"] = Libraries.LoadAssembly;
        }
    }
}