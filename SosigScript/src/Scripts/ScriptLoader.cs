using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Deli;
using Deli.Runtime;
using Deli.VFS;
using Deli.VFS.Disk;

//because i can
using static SosigScript.Logger;

namespace SosigScript
{
    public class ScriptLoader
    {
        /// <summary>
        /// Loads the script, then executes it through ScriptExecutor
        /// </summary>
        public IEnumerator LoadScripts(RuntimeStage stage, Mod mod, IHandle handle)
        {
            if (handle is not IFileHandle rawfile) throw new ArgumentException($"ERROR: {handle} IS NOT A VALID SCRIPT");
            if (rawfile is not IDiskHandle file) throw new ArgumentException($"ERROR: {handle} IS NOT A VALID SCRIPT");

            Print($"Loading script {file}");

            string script;
            yield return script = File.ReadAllLines(file.PathOnDisk).ToString();

            Print($"Executing script {file.PathOnDisk}");

            if (!SosigScript.Libraries.LibrariesLoaded && SosigScript.Libraries.LoadedAssemblies.Count > 0)
            {
                Print("Loading libraries");
                SosigScript.Libraries.LoadAllAssemblyTypes();
            }

            yield return SosigScript.Instance.ScriptExecutor.Execute(new KeyValuePair<Mod, string>(mod, script));
        }
    }
}