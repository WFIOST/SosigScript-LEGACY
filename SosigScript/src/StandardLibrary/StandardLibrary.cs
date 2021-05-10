using MoonSharp.Interpreter;

namespace SosigScript.StandardLibrary
{
    public class StandardLibrary : SosigScriptLibrary
    {
        public string LibraryName { get; } = "Standard Library";

        public Table Globals { get; }

        public override void RegisterUserData()
        {
            
        }
    }
}