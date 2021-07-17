using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx.Configuration;
using HarmonyLib;
using MoonSharp.Interpreter.Interop;

using static SosigScript.Common.Logger;

namespace SosigScript.Libraries
{
    /// <summary>
    /// Loads SosigScript libraries
    /// </summary>
    public class LibraryLoader
    {
        /// <summary>
        /// List of all the loaded assemblies
        /// </summary>
        public IEnumerable<Assembly> LoadedAssemblies { get; }
        /// <summary>
        /// All loaded types
        /// </summary>
        public IEnumerable<SosigScriptTypeList> LoadedTypes { get; }
        /// <summary>
        /// Boolean expressing if the libraries have been loaded
        /// </summary>
        public bool LibrariesLoaded { get; private set; }
        
        

        public LibraryLoader()
        {
            LoadedAssemblies    = new List<Assembly>();
            LoadedTypes         = new List<SosigScriptTypeList>();
            LibrariesLoaded     = false;
        }
        
        /// <summary>
        /// Loads an assembly into memory
        /// </summary>
        public void LoadAssembly()
        {
            string[] dirs = Directory.GetDirectories(Common.PLUGINS_DIR);
            foreach (string dirPath in dirs)
            {
                var dir = new DirectoryInfo(dirPath);

                foreach (FileInfo ssLibMetaFile in dir.GetFiles("*.sslibmeta"))
                {
                    string contents = String.Empty;
                    using var reader = new StreamReader(ssLibMetaFile.OpenRead());
                    while (!reader.EndOfStream)
                    {
                        contents += $"{reader.ReadLine()}\n";
                    }
                }
            }
        }

        /// <summary>
        /// Loads all the classes with SosigScriptLibraryAttribute in the specified assembly
        /// </summary>
        /// <param name="asm">Assembly to get types from</param>
        private void LoadAssemblyTypes(Assembly asm)
        {
            var typeList = new SosigScriptTypeList() { Source = asm };
            foreach (var type in asm.SafeGetTypes())
            {
                if (!type.IsClass) continue;
                if (type.BaseType != typeof(SosigScriptTypeList)) continue;

                var types = new Type[] { typeof(SosigScriptTypeList) };

                var ctor = type.GetConstructor
                (
                    BindingFlags.Instance | BindingFlags.Public,
                    null,
                    CallingConventions.HasThis,
                    types,
                    null
                );

                
                ctor?.Invoke(new object[] { typeList });
            }
            LoadedTypes.AddItem(typeList);
        }

        /// <summary>
        /// Loads all the classes with SosigScriptLibraryAttribute in all loaded assemblies
        /// </summary>
        public void LoadAllAssemblyTypes()
        {
            foreach (var asm in LoadedAssemblies)
            {
                LoadAssemblyTypes(asm);
            }

            //So we dont reload the libraries every script
            LibrariesLoaded = true;
        }
    }
}