using System;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using SosigScript.Libraries;

namespace ExampleSosigScriptLibrary
{
    [SosigScriptLibrary("Example Library", "1.0.0")]
    public class Library : SosigScriptLibrary
    {
        public Library(SosigScriptTypeList types) : base(types)
        {
            types.AddType<ExampleType>();
        }
    }
}