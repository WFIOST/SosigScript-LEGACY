using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using SosigScript.Libraries;

namespace ExampleSosigScriptLibrary
{
    [SosigScriptLibrary("Example SosigScript Library", "1.0.0")]
    public class ExampleLibrary : ISosigScriptLibrary
    {
        public IEnumerable<Type> TypesToLoad { get; set; } = new Type[]
        {
            typeof(ExampleType)
        };

        public IEnumerable<Type> StaticTypesToLoad { get; set; } = new Type[]
        {
            typeof(ExampleStaticType)
        };
    }
}