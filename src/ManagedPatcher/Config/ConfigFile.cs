using System.Collections.Generic;
using Newtonsoft.Json;

namespace ManagedPatcher.Config
{
    public class ConfigFile
    {
        /// <summary>
        ///     Settings for decompilation.
        /// </summary>
        [JsonProperty("decompilation")] public DecompilationConfig Decompilation = new();

        /// <summary>
        ///     Defines the different types of diff options.
        /// </summary>
        [JsonProperty("diffs")] public Dictionary<string, string[]> Diffs = new();

        /// <summary>
        ///     Defines the different types of patch options.
        /// </summary>
        [JsonProperty("patches")] public Dictionary<string, string[]> Patches = new();
    }
}