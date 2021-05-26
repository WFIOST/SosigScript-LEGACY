using MoonSharp.Interpreter;

namespace SosigScript
{
    public abstract class SosigScriptLibrary
    {
        public Script CurrentScript = SosigScript.Instance.ScriptLoader;

        public Table Globals = new Table(SosigScript.Instance.ScriptLoader);

        public abstract void Register();
        
    }
}