﻿using System;
using MoonSharp.Interpreter;
using Semver;

namespace SosigScript
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false, AllowMultiple = false)]
	public class SosigScriptLibraryAttribute : Attribute
	{
		public string LibraryName;
		public SemVersion LibraryVersion;
		
		public InteropAccessMode AccessMode { get; set; }
		
		public SosigScriptLibraryAttribute(string name, string version)
		{
			LibraryName = name;
			LibraryVersion = SemVersion.Parse(version);
			
			AccessMode = InteropAccessMode.Default;
		}
	}
}
