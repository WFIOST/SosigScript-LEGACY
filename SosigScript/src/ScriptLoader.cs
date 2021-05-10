using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Deli;
using Deli.Runtime;
using Deli.VFS;
//because i can
using static SosigScript.Logger;

namespace SosigScript
{
    public class ScriptLoader
    {
        public static Dictionary<Mod, string> Scripts { get; } = new();

        public IEnumerator LoadScripts(RuntimeStage stage, Mod mod, IHandle handle)
        {
            if (handle is not IFileHandle file) throw new ArgumentException($"ERROR: {handle} IS NOT A VALID SCRIPT");

            Print($"Loading script {file}");
            string script;

            yield return script = File.ReadAllLines(file.Path).ToString();

            Scripts.Add(mod, script);

            Print($"Executing script {file}");
            yield return Executioner.ExecuteAsync(Scripts.FirstOrDefault());
        }
    }
}