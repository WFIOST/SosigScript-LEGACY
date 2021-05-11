using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Deli;
using Deli.Runtime;
using Deli.VFS;

//because i can
using static SosigScript.Logger;

namespace SosigScript
{
    public class ScriptLoader
    {
        public IEnumerator LoadScripts(RuntimeStage stage, Mod mod, IHandle handle)
        {
            if (handle is not IFileHandle file) throw new ArgumentException($"ERROR: {handle} IS NOT A VALID SCRIPT");

            Print($"Loading script {file}");

            string script;
            yield return script = File.ReadAllLines(file.Path).ToString();

            Print($"Executing script {file}");

            var executioner = new Executioner();
            
            yield return executioner.ExecuteAsync(new KeyValuePair<Mod, string>(mod, script));
        }
    }
}