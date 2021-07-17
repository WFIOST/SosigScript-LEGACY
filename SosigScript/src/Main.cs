using System.Collections.Generic;
using System.Linq;

using BepInEx;
using BepInEx.Logging;

using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Platforms;
using Script = MoonSharp.Interpreter.Script;

using SosigScript.Libraries;
using SosigScript.ScriptLoader;
using PluginInfo = SosigScript.Common.PluginInfo;

using static SosigScript.Common.Logger;

#region ERROR DISABLES
// ReSharper disable UnusedMember.local
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable EmptyConstructor
#pragma warning disable 8618
#endregion

namespace SosigScript
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.NAME, PluginInfo.VERSION)]
    public class SosigScript : BaseUnityPlugin
    {

        /// <summary>
        /// Global manual log source
        /// </summary>
        internal static ManualLogSource Console         { get; private set; }

        /// <summary>
        /// Scriptloader, used in the Executioner, libraries and LibraryLoader
        /// </summary>
        public static Script            ScriptLoader    { get; private set; }

        /// <summary>
        /// All actively running scripts
        /// </summary>
        public static List<Executioner> ActiveScripts   { get; private set; }

        /// <summary>
        /// Library Loader instance
        /// </summary>
        public static LibraryLoader     Libraries       { get; private set; }
        
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
            //We set the "Soft Sandbox" so user has more options in their scripts
            ScriptLoader    = new Script(CoreModules.Preset_SoftSandbox);
            Libraries       = new LibraryLoader();
            ActiveScripts   = new List<Executioner>();
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
    }
}