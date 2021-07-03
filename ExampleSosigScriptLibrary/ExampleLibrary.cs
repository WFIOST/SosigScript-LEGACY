using SosigScript.Libraries;

namespace ExampleSosigScriptLibrary
{
    public class ExampleLibrary : SosigScriptLibrary
    {
        public ExampleLibrary(SosigScriptTypeList types) : base(types)
        {
            types.AddType<ExampleType>();
        }
    }
}