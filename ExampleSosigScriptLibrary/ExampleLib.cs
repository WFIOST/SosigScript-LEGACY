using SosigScript;

namespace ExampleSosigScriptLibrary
{
    [SosigScriptLibrary("ExampleLib1", "1.0.0")]
    public class ExampleLib1 : SosigScriptLibrary
    {
        public override void Register() {}
    }
    
    [SosigScriptLibrary("ExampleLib2", "1.0.0")]
    public class ExampleLib2 : SosigScriptLibrary
    {
        public override void Register() {}
    }
    
    [SosigScriptLibrary("ExampleLib3", "1.0.0")]
    public class ExampleLib3 : SosigScriptLibrary
    {
        public override void Register() {}
    }
}