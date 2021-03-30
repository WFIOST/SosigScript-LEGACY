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

        public Dictionary<string, string> Scripts { get; private set; };

        private int _loaded;
        
        public void Load(SetupStage stage, Mod mod, IHandle handle)
        {

            if (handle is IFileHandle file)
            {
                Scripts.Add(mod.Info.Guid, stage.ImmediateReaders.Get<string>()(file));
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