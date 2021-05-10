using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop;

namespace SosigScript
{
    public abstract class SosigScriptLibrary
    {
        public Table? Globals;

        public abstract void RegisterUserData();
        
        public void RegisterAssembly(Assembly? asm = null, bool includeExtensionTypes = false)
        {
            asm ??= Assembly.GetCallingAssembly();

            if (includeExtensionTypes)
            {
                var extensionTypes = from t in asm.SafeGetTypes()
                    let attributes = Framework.Do.GetCustomAttributes(t, typeof(ExtensionAttribute), true)
                    where attributes is { Length: > 0 } 
                    select new { Attributes = attributes, DataType = t };

                foreach (var extType in extensionTypes)
                {
                    UserData.RegisterExtensionType(extType.DataType);
                }
            }


            var userDataTypes = from t in asm.SafeGetTypes()
                let attributes = Framework.Do.GetCustomAttributes(t, typeof(SosigScriptLibraryAttribute), true)
                where attributes is { Length: > 0 }
                select new { Attributes = attributes, DataType = t };

            foreach (var userDataType in userDataTypes)
            {
                UserData.RegisterType
                (
                    userDataType.DataType,
                    userDataType.Attributes
                        .OfType<SosigScriptLibraryAttribute>()
                        .First()
                        .AccessMode
                );
            }
        }
    }
}