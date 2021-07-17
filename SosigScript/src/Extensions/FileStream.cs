using System.Collections.Generic;
using System.IO;

namespace SosigScript.Extensions
{
    public static partial class Extensions
    {
        public static IEnumerable<string> ReadAllLines(this FileStream stream, bool lineBreak = true)
        {
            var lines = new List<string>();
            using var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
                lines.Add(lineBreak ? $"{reader.ReadLine()}\n" : $"{reader.ReadLine()}");
            return lines;
        }
    }
}