using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Deli;
using Deli.Runtime;
using Deli.Setup;
using Deli.VFS;
using Deli.VFS.Disk;
using HarmonyLib;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop;

using static SosigScript.Logger;

namespace SosigScript
{
    /// <summary>
    /// Loads SosigScript libraries
    /// </summary>
    public class LibraryLoader
    {
        /// <summary>
        /// List of all the loaded assemblies
        /// </summary>
        public List<Assembly> LoadedAssemblies { get; private set; }
        /// <summary>
        /// Boolean expressing if the libraries have been loaded
        /// </summary>
        public bool LibrariesLoaded { get; private set;  }

        public LibraryLoader()
        {
            LoadedAssemblies = new List<Assembly>();
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
            
        LoadedAssemblies.Add(asm);
        }

        /// <summary>
        /// Loads all the classes with SosigScriptLibraryAttribute in the specified assembly
        /// </summary>
        /// <param name="asm">Assembly to get types from</param>
        /// <param name="useExtTypes">Extension types</param>
        private static void LoadAssemblyTypes(Assembly asm, bool useExtTypes = false)
        {
            if (useExtTypes)
            {
                Debug.Print($"Assembly {asm.FullName} has extension attributes!");
                
                var extensionTypes = asm.SafeGetTypes()
                    .Select
                    (
                        type => new
                        {
                            type, attributes = Framework.Do.GetCustomAttributes(type, typeof(ExtensionAttribute), true)
                        }
                    )
                    .Where(type1 => type1.attributes is { Length: > 0 })
                    .Select(@type1 => new { Attributes = @type1.attributes, DataType = @type1.type });

                foreach (var type in extensionTypes)
                {
                    Debug.Print($"Registering type {type.DataType.Name}");
                    UserData.RegisterExtensionType(type.DataType);
                }
            }

            var userDataTypes = asm.SafeGetTypes()
                .Select
                (
                    usrtype => new
                    {
                        usrtype, attributes = Framework.Do.GetCustomAttributes(usrtype, typeof(SosigScriptLibraryAttribute), true)
                    }
                )
                .Where(usrtype1 => usrtype1.attributes is { Length: > 0 })
                .Select(usrtype1 => new { Attributes = usrtype1.attributes, DataType = usrtype1.usrtype });

            foreach (var type in userDataTypes)
            {
                Debug.Print($"Registering type {type.DataType.FullName}");
                
                UserData.RegisterType
                (
                    type.DataType,
                    type.Attributes
                        .OfType<SosigScriptLibraryAttribute>()
                        .First()
                        .AccessMode
                );
            }
            
        }

        /// <summary>
        /// Loads all the classes with SosigScriptLibraryAttribute in all loaded assemblies
        /// </summary>
        public void LoadAllAssemblyTypes()
        {
            foreach (var asm in LoadedAssemblies)
            {
                LoadAssemblyTypes(asm, true);
            }

            //So we dont reload the libraries every script
            LibrariesLoaded = true;
        }
    }
}