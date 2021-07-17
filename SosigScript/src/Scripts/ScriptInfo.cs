namespace SosigScript.ScriptLoader
{
    public struct ScriptInfo
    {
        public string Raw           { get; set; }
        public string Path          { get; set; }
        public string Filename      { get; set; }

        //public Metadata Metadata    { get; set; } //Unused???
    }

    public struct Metadata
    {
        public string   Name            { get; set; }
        public string   Version         { get; set; }
        public string   Description     { get; set; }
        public string[] Dependencies    { get; set; }
    }
}