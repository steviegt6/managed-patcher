using System.Collections.Generic;
using System.Threading.Tasks;
using CliFx.Attributes;
using ManagedPatcher.Config;
using ManagedPatcher.Tasks.Setup;
using ManagedPatcher.Utilities;

namespace ManagedPatcher.Commands
{
    [Command("setup")]
    public class SetupCommand : BaseCommand
    {
        /// <summary>
        ///     Overrides for <see cref="DecompilationConfig.AssemblyPaths"/>.
        /// </summary>
        [CommandOption("input")]
        public string Input { get; init; } = "";

        public override async ValueTask ExecuteAsync(ConfigFile config)
        {
            Dictionary<string, string> pathOverrides = string.IsNullOrEmpty(Input) 
                ? new Dictionary<string, string>() 
                : new PathOverriderCollection(Input).Paths;

            using SetupTask task = new();
            await task.ExecuteAsync(new SetupArguments(config, pathOverrides));
        }
    }
}