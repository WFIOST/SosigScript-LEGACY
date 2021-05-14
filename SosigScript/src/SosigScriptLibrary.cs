using MoonSharp.Interpreter;

namespace SosigScript
{
    public abstract class SosigScriptLibrary
    {
        private Script _currentScript = SosigScript.SosigScriptInstance.ScriptLoader;

        public Table Globals = new Table(SosigScript.SosigScriptInstance.ScriptLoader);

        public abstract void RegisterUserData();
        
    }
}