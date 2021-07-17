namespace ExampleSosigScriptLibrary
{
    public struct ExampleType
    {
        public string   GUID    { get; set; }
        public int      ID      { get; set; }
        public object   Value   { get; set; }

        public string Parse() => $"{GUID}, {ID}, {Value}";
    }
}