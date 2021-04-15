using BepInEx.Logging;
using Deli;
using Deli.Runtime;
using Deli.Setup;

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