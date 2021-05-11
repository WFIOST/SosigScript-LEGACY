using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Compatibility;
using MoonSharp.Interpreter.Interop;

namespace SosigScript
{
    public abstract class SosigScriptLibrary
    {
        public Table Globals;

        public abstract void RegisterUserData();
        
    }
}