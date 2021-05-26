﻿using System;
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
        public static Executioner ScriptExecutor { get; private set; }
        public static SosigScript Instance { get; private set; }
        internal static ManualLogSource Console { get; private set; }

        public static LibraryLoader Libraries = new();

        public Script ScriptLoader { get; }

        public SosigScript()
        {
            Logger.LogInfo("Initialising SosigScript");
            //Start up MoonSharp so scripts load faster
            Logger.LogDebug("Warming up scripts");
            Script.WarmUp();
            //Set Console log
            Console = BepInEx.Logging.Logger.CreateLogSource("SosigScript");
            //Give it standard platform settings
            Logger.LogDebug("Setting Platform accessors");
            Script.GlobalOptions.Platform = new StandardPlatformAccessor();
            //Make a new manual log source specifically for checking if MoonSharp is initialised
            Logger.LogDebug("Setting DebugPrint logsource");
            Script.DefaultOptions.DebugPrint = message => { BepInEx.Logging.Logger.CreateLogSource("SosigScript [INITIALISATION]").LogInfo(message); };
            //Run a print command!
            Script.RunString("print('SosigScript initialised! Hello from Lua!')");

            //We set the "Soft Sandbox" so user has more options in their scripts
            ScriptLoader = new Script(CoreModules.Preset_SoftSandbox);
            
            ScriptExecutor = new Executioner();

            Instance = this;
            Logger.LogInfo("Sosigscript Initialised");
        }
        
        private void Register(RuntimeStage stage)
        {
            Debug.Print("Loading Scripts");
            stage.RuntimeAssetLoaders[Source, "script"] = new ScriptLoader().LoadScripts;
        }

        private void Register(SetupStage stage)
        {
            Debug.Print("Loading Scripts");
            stage.SetupAssetLoaders[Source, "library"] = Libraries.LoadAssembly;
        }
    }
}