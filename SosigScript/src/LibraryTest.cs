using MoonSharp.Interpreter;
using Semver;

namespace SosigScript
{
    [MoonSharpUserData]
    [SosigScriptLibrary("Test Library", "1.0.0")]
    public class LibraryTest : SosigScriptLibrary
    {
        public override void RegisterUserData()
        {
            
        }
    }
}