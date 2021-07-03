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
using static SosigScript.Common.Logger;

namespace SosigScript.ScriptLoader
{
    /// <summary>
    /// Class for loading scripts into memory
    /// </summary>
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
            yield return script = String.Join("\n", File.ReadAllLines(file.PathOnDisk));

            Print($"Executing script {file.PathOnDisk}");
            
            Debug.Print($"SCRIPT CONTENTS: {script}");

            Debug.Print
            (
                $"Libraries loaded?: {SosigScript.Libraries.LibrariesLoaded.ToString()}\n" +
                $"Loaded Assemblies: {SosigScript.Libraries.LoadedAssemblies.Count()}"
            );
            
            if (!SosigScript.Libraries.LibrariesLoaded && SosigScript.Libraries.LoadedAssemblies.Any())
            {
                Print("Loading libraries");
                SosigScript.Libraries.LoadAllAssemblyTypes();
            }

            Executioner scriptrunner;

            yield return scriptrunner = new Executioner(new KeyValuePair<Mod, string>(mod, script));

            SosigScript.Instance.ActiveScripts.Add(scriptrunner);

        }
    }
}