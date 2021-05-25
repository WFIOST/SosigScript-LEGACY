using MoonSharp.Interpreter;

namespace SosigScript
{
    public abstract class SosigScriptLibrary
    {
        private Script _currentScript = SosigScript.Instance.ScriptLoader;

        public Table Globals = new Table(SosigScript.Instance.ScriptLoader);

        public abstract void RegisterUserData();
        
    }
}