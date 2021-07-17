using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoonSharp.Interpreter;
using SosigScript.Extensions;

//because i can
using static SosigScript.Common.Logger;

namespace SosigScript.ScriptLoader
{
    /// <summary>
    /// Class for loading scripts into memory
    /// </summary>
    public class ScriptLoader
    {
        public IEnumerable<IEnumerable<ScriptInfo>> LoadedScripts { get; private set; }

        public ScriptLoader()
        {
            string[] directories = Directory.GetDirectories(Common.PLUGINS_DIR);
            var loadedScripts = new List<List<ScriptInfo>>(directories.Length);
            for (var i = 0; i < directories.Length; i++)
            {
                var dir = new DirectoryInfo(directories[i]);

                //We use the first one cause it don't make sense to have multiple script files
                var files = dir.GetFiles("*.lua");
                loadedScripts[i] = new List<ScriptInfo>(files.Length);
                foreach (FileInfo file in files)
                {
                    var script = new ScriptInfo()
                    {
                        Filename = file.Name,
                        Path = file.FullName,
                        Raw = file.OpenRead().ReadAllLines().ToString()
                    };
                    loadedScripts[i].Add(script);
                }
            }

            LoadedScripts = loadedScripts.ToArray();
        }
        
        /// <summary>
        /// Loads the script, then executes it through ScriptExecutor
        /// </summary>
        public IEnumerator LoadAndExecuteScripts()
        {
            foreach (IEnumerable<ScriptInfo> scriptList in LoadedScripts)
            {
                foreach (var script in scriptList)
                {
                    Executioner exec;
                    yield return exec = new Executioner(script);
                    SosigScript.ActiveScripts.Add(exec);
                }
            }
        }
    }
}