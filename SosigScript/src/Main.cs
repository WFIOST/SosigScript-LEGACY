using BepInEx.Logging;
using Deli;
using Deli.Runtime;
using Deli.Setup;

// ReSharper disable UnusedMember.local
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable EmptyConstructor

namespace SosigScript
{
    public class Plugin : DeliBehaviour
    {
        public Plugin()
        {
            Logger.LogInfo("Initialising SosigScript");
        }

        internal static Mod MainMod { get; private set; }

        internal static ManualLogSource Console { get; set; } = new("SosigScript");

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