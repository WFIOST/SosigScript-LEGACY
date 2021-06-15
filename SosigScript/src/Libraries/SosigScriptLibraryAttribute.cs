using System;
using MoonSharp.Interpreter;
using Semver;

namespace SosigScript.Libraries
{
	/// <summary>
	/// Attribute used for metadata for SosigScript libraries
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class SosigScriptLibraryAttribute : Attribute
	{
		/// <summary>
		/// Name of the library
		/// </summary>
		public string				LibraryName		{ get; }
		/// <summary>
		/// Version of the library, parsed from a string
		/// </summary>
		public SemVersion			LibraryVersion	{ get; }
		/// <summary>
		/// AccessMode, set to "default"
		/// </summary>
		public InteropAccessMode	AccessMode		{ get; }
		/// <summary>
		/// Metadata for a SosigScript library
		/// </summary>
		/// <param name="name">Name of the library</param>
		/// <param name="version">Library version, must be SemVer compliant (Major.Minor.Build)</param>
		public SosigScriptLibraryAttribute(string name, string version)
		{
			LibraryName = name;
			LibraryVersion = SemVersion.Parse(version);
			AccessMode = InteropAccessMode.Default;
		}
	}
}
