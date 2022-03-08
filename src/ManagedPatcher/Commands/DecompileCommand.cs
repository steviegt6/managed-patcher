using System.Collections.Generic;
using System.Threading.Tasks;
using CliFx.Attributes;
using ManagedPatcher.Config;
using ManagedPatcher.Tasks.Decompile;
using ManagedPatcher.Utilities;

namespace ManagedPatcher.Commands
{
    [Command("decompile")]
    public class DecompileCommand : BaseCommand
    {
        /// <summary>
        ///     Overrides for <see cref="DecompilationConfig.AssemblyPaths"/>.
        /// </summary>
        public string Input { get; init; } = "";

        public override async ValueTask ExecuteAsync(ConfigFile config)
        {
            Dictionary<string, string> pathOverrides = string.IsNullOrEmpty(Input) 
                ? new Dictionary<string, string>() 
                : new PathOverriderCollection(Input).Paths;
            
            using DecompileTask task = new();
            await task.ExecuteAsync(new DecompileArguments(config, pathOverrides));
        }
    }
}