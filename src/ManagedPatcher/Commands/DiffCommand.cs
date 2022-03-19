using System.Linq;
using System.Threading.Tasks;
using CliFx.Attributes;
using ManagedPatcher.Config;
using ManagedPatcher.Tasks.Diff;

namespace ManagedPatcher.Commands
{
    [Command("diff")]
    public class DiffCommand : BaseCommand
    {
        [CommandOption("input")]
        public string Input { get; init; } = "";
        
        public override async ValueTask ExecuteAsync(ConfigFile config)
        {
            using DiffTask differ = new();
            await differ.ExecuteAsync(new DiffArguments(config, Input.Split(';').ToList()));
        }
    }
}