using System.Threading.Tasks;
using Spectre.Console;

namespace ManagedPatcher.Tasks.Patch
{
    public class PatchTask : PatcherTask<PatchArguments>
    {
        public override async Task ExecuteAsync(PatchArguments args)
        {
            if (!args.Config.Diffs.ContainsKey(args.Input))
            {
                AnsiConsole.MarkupLine($"[red]ERROR:[/] Cannot execute patch task \"{args.Input}\" as no such input exists!");
                return;
            }
            
            await Task.CompletedTask;
        }
    }
}