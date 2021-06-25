using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace SosigScript.Libraries
{
    public class SosigScriptTypeList
    {
        public Assembly? Source { get; set; }
        public IEnumerable<Type> RegisteredTypes { get; private set; }

        public SosigScriptTypeList()
        {
            RegisteredTypes = new List<Type>();
        }

        public void AddType<T>()
        {
            RegisteredTypes.AddItem(typeof(T));
        }
    }
}