using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CliFx.Attributes;
using ManagedPatcher.Config;
using ManagedPatcher.Tasks.Patch;

namespace ManagedPatcher.Commands
{
    [Command("patch")]
    public class PatchCommand : BaseCommand
    {
        [CommandOption("input")]
        public string Input { get; init; } = "";
        
        public override async ValueTask ExecuteAsync(ConfigFile config)
        {
            using PatchTask patcher = new();
            await patcher.ExecuteAsync(new PatchArguments(config, Input.Split(';').ToList()));
        }
    }
}