using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Deli;
using Deli.Runtime;
using Deli.VFS;
using HarmonyLib;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop;

using static SosigScript.Logger;

namespace SosigScript
{
    public class LibraryLoader
    {
        public IEnumerable<Assembly> LoadedAssemblies { get; } = new List<Assembly>();

        public IEnumerator<Assembly> LoadAssembly(RuntimeStage stage, Mod mod, IHandle handle)
        {
            if (handle is not IFileHandle file) throw new ArgumentException($"ERROR: {handle} IS NOT A VALID ASSEMBLY!");
            
            Debug.Print($"Loading assembly {file.Name}");

            Assembly asm;
            
            yield return asm = Assembly.LoadFile(file.Path);

            LoadedAssemblies.AddItem(asm);
        }

        public static void LoadAssemblyTypes(Assembly asm, bool useExtTypes = false)
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

        public void LoadAllAssemblyTypes()
        {
            foreach (var asm in LoadedAssemblies)
            {
                LoadAssemblyTypes(asm, true);
            }
        }
    }
}