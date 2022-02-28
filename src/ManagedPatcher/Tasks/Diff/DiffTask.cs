using System.Threading.Tasks;
using Spectre.Console;

namespace ManagedPatcher.Tasks.Diff
{
    public class DiffTask : PatcherTask<DiffArguments>
    {
        public override async Task ExecuteAsync(DiffArguments args)
        {
            if (!args.Config.Diffs.ContainsKey(args.Input))
            {
                AnsiConsole.MarkupLine($"[red]ERROR:[/] Cannot execute diff task \"{args.Input}\" as no such input exists!");
                return;
            }
            
            await Task.CompletedTask;
        }
    }
}