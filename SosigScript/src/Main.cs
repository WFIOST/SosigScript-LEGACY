using System;
using System.Linq;
using System.Threading;
using BepInEx.Logging;
using Deli;
using Deli.Patcher;
using Deli.Runtime;
using Deli.Setup;
using Deli.VFS;
using MoonSharp.Interpreter;

using static SosigScript.Logger;

// ReSharper disable UnusedMember.local
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable EmptyConstructor

namespace SosigScript
{
    public class Plugin : DeliBehaviour
    {
        internal static Mod MainMod { get; private set; }
        
        internal static ManualLogSource Console { get; set; } = new("SosigScript");


        public Plugin()
        {
            Logger.LogInfo("Initialising SosigScript");
        }

        private void Awake()
        {
            Logger.LogInfo("Started SosigScript!");
            MainMod = Source;
        }
        
        private void Register(RuntimeStage stage)
        {
            stage.RuntimeAssetLoaders[MainMod, "SosigScript"] = new ScriptLoader().LoadScripts;
        }
    }
    
}