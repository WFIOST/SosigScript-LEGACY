using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace SosigScript.Libraries
{
    /// <summary>
    /// Class for a list of types that may be used in a SosigScript script
    /// </summary>
    public class SosigScriptTypeList
    {
        /// <summary>
        /// The source assembly for the types
        /// </summary>
        public Assembly? Source                     { get; set; }
        /// <summary>
        /// List of registered types
        /// </summary>
        public IEnumerable<Type> RegisteredTypes    { get; private set; }

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