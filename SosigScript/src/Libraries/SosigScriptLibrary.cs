using MoonSharp.Interpreter;

namespace SosigScript
{
    /// <summary>
    /// Abstract class providing utilities for creating a SosigScript library
    /// </summary>
    public abstract class SosigScriptLibrary
    {
        /// <summary>
        /// Table of the libraries global variables. Parsed into one big Table at runtime
        /// </summary>
        public Table Globals = new Table(SosigScript.Instance.ScriptLoader);
        
        /// <summary>
        /// Function for registering your Globals
        /// </summary>
        public abstract void Register();
        
    }
}