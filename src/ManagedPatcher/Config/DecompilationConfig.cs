using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ManagedPatcher.Config
{
    /// <summary>
    ///     Settings for C# assembly decompilation.
    /// </summary>
    public class DecompilationConfig
    {
        /// <summary>
        ///     Whether decompilation is enabled.
        /// </summary>
        [JsonProperty("decompilationEnabled")] public bool DecompilationEnabled = true;

        /// <summary>
        ///     The C# language version to decompile to.
        /// </summary>
        [JsonProperty("languageVersion")] public string LanguageVersion = "";
        
        /// <summary>
        ///     The target C# framework to decompile to.
        /// </summary>
        [JsonProperty("frameworkVersion")] public string FrameworkVersion = "";

        /// <summary>
        ///     Decompilation tasks, used as keys in assemblyPaths (<see cref="AssemblyPaths"/>) and decompilationPaths (<see cref="DecompilationPaths"/>).
        /// </summary>
        [JsonProperty("decompileTasks")] public string[] DecompileTasks = Array.Empty<string>();

        /// <summary>
        ///     The paths to assemblies that will be decompiled.
        /// </summary>
        [JsonProperty("assemblyPaths")] public Dictionary<string, string> AssemblyPaths = new();

        /// <summary>
        ///     The paths an assembly should be decompiled to.
        /// </summary>
        [JsonProperty("decompilationPaths")] public Dictionary<string, string> DecompilationPaths = new();
    }
}