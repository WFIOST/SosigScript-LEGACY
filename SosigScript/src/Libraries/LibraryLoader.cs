using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Deli;
using Deli.Setup;
using Deli.VFS;
using Deli.VFS.Disk;
using HarmonyLib;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Compatibility;
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
            LoadedAssemblies = new List<Assembly>();
            LoadedTypes = new List<SosigScriptTypeList>();
            LibrariesLoaded = false;
        }
        
        /// <summary>
        /// Loads an assembly into memory
        /// </summary>
        public void LoadAssembly(SetupStage stage, Mod mod, IHandle handle)
        {
            if (handle is not IFileHandle rawfile) throw new ArgumentException($"ERROR: {handle} IS NOT A VALID ASSEMBLY!");
            if (rawfile is not IDiskHandle file) throw new ArgumentException($"ERROR: {rawfile} IS NOT A VALID ASSEMBLY!");
            
            Debug.Print($"Loading assembly {rawfile.Name}");
            Debug.Print($"Assembly path: {file.PathOnDisk}");

            var asm = Assembly.LoadFile(file.PathOnDisk);

            Debug.Print("Loaded Assembly");
            
            LoadedAssemblies.AddItem(asm);
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
                
                var types = new Type[1];
                types[0] = typeof(SosigScriptTypeList);

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
            typeList = null; //We ain't using this anymore
            
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