using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ManagedPatcher.Config
{
    public class ConfigFile
    {
        /// <summary>
        ///     Whether decompilation of a project is allowed (used only for C# projects).
        /// </summary>
        [JsonProperty("decompilationAllowed")]
        public bool DecompilationAllowed;

        /// <summary>
        ///     Defines the different types of diff options.
        /// </summary>
        [JsonProperty("diffs")]
        public Dictionary<string, string[]> Diffs = new();

        /// <summary>
        ///     Defines the different types of patch options.
        /// </summary>
        [JsonProperty("patches")]
        public Dictionary<string, string[]> Patches = new();
    }
}