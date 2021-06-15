using System;
using System.Collections.Generic;

namespace SosigScript.Libraries
{
    public interface ISosigScriptLibrary
    {
        public IEnumerable<Type> TypesToLoad { get; set; }
        public IEnumerable<Type> StaticTypesToLoad { get; set; }
    }
}