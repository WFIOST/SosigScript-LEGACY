using System;

namespace SosigScript.Libraries
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class SosigScriptLibraryAttribute : Attribute
    {
        public string Name { get; }
        public string Version { get; }

        public SosigScriptLibraryAttribute(string name, string version)
        {
            Name = name;
            Version = version;
        }
    }
}