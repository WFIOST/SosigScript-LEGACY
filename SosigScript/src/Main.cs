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

        public static SosigScript SosigScriptInstance { get; private set; }
        internal static ManualLogSource Console { get; private set; } = new("SosigScript");

        public static LibraryLoader Libraries = new();

        public Script ScriptLoader { get; set; }

        public SosigScript()
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

            ScriptLoader = new Script();

            SosigScriptInstance = this;
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
            stage.RuntimeAssetLoaders[Source, "script"] = new ScriptLoader().LoadScripts;
    
           
        }

        private void Register(SetupStage stage)
        {
            stage.SetupAssetLoaders[Source, "library"] = Libraries.LoadAssembly;
        }
    }
}