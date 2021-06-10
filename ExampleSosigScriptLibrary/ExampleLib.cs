using MoonSharp.Interpreter;
using SosigScript;

namespace ExampleSosigScriptLibrary
{
    [SosigScriptLibrary("ExampleLib1", "1.0.0")]
    public class ExampleLib1 : SosigScriptLibrary
    {
        public struct ExampleStruct
        {
            public string Text  { get; set; }
            public int    ID    { get; set; }
            public float  Value { get; set; }
        }

        public override void RegisterUserData()
        {
            UserData.RegisterType<ExampleStruct>();
        }

        public override void RegisterGlobals()
        {
            var exampleobj = new ExampleStruct
            {
                Text = "Hello World!",
                ID = 1,
                Value = 3.14f
            };

            Globals["ExampleObject"] = exampleobj;
        }
    }
    
    [SosigScriptLibrary("ExampleLib2", "1.0.0")]
    public class ExampleLib2 : SosigScriptLibrary
    {
        public override void RegisterUserData()
        {
            
        }

        public override void RegisterGlobals()
        {
            
        }
    }
    
    [SosigScriptLibrary("ExampleLib3", "1.0.0")]
    public class ExampleLib3 : SosigScriptLibrary
    {
        public override void RegisterUserData()
        {
            
        }

        public override void RegisterGlobals()
        {
            
        }
    }
}