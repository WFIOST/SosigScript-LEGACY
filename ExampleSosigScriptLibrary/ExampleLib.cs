using SosigScript;

namespace ExampleSosigScriptLibrary
{
    [SosigScriptLibrary("ExampleLib1", "1.0.0")]
    public class ExampleLib1 : SosigScriptLibrary
    {
        public string   Text    { get; set; }
        public int      ID      { get; set; }
        public float    Value   { get; set; }
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