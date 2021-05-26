using System;
using MoonSharp.Interpreter;
using Semver;

namespace SosigScript
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
	public class SosigScriptLibraryAttribute : Attribute
	{
		public string				LibraryName		{ get; }
		public SemVersion			LibraryVersion	{ get; }
		public InteropAccessMode	AccessMode		{ get; }
		
		public SosigScriptLibraryAttribute(string name, string version)
		{
			LibraryName = name;
			LibraryVersion = SemVersion.Parse(version);
			AccessMode = InteropAccessMode.Default;
		}
	}
}
