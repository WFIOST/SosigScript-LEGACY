using System.Security.Permissions;
using MoonSharp.Interpreter;

namespace SosigScript
{
    public abstract class SosigScriptLibrary
    {
        public string LibraryName   { get; }
        public Table  Globals       { get; }
        
        public abstract void RegisterUserData();
    }
}