using System.Collections.Generic;
using System.IO;
using BepInEx.Logging;
using Deli;
using Deli.Patcher;
using Deli.Setup;
using Deli.VFS;

namespace SosigScript.Common
{
    public class ScriptLoader 
    {

        public Dictionary<string, string> Scripts => _modInfo;

        private int _loaded;

        private Dictionary<string, string> _modInfo = new();
        
        public void Load(SetupStage stage, Mod mod, IHandle handle)
        {

            if (handle is IFileHandle file)
            {
                _modInfo.Add(mod.Info.Guid, stage.ImmediateReaders.Get<string>()(file));
            }
                
        }
        
        /*

        public void OnPatcher(PatcherStage stage) => 
            stage.PatcherAssetLoaders
            [
                ScriptLoader,
                "SosigScript"
            ] = Load; 
        
        */
    }
    
}