using SosigScript.Libraries;

namespace ExampleSosigScriptLibrary
{
    public class ExampleType : ISosigScriptType
    {
        public string ID;
        public float Value;
    }

    public class ExampleStaticType : ISosigScriptType
    {
        public static string ID = "EC2E301C-9DE2-435D-959C-25464D525AA6";
        public static float Value = 3.14f;
    }
}