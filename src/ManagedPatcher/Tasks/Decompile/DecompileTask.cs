using System.Threading.Tasks;
using Spectre.Console;

namespace ManagedPatcher.Tasks.Decompile
{
    public class DecompileTask : PatcherTask<DecompileArguments>
    {
        public override async Task ExecuteAsync(DecompileArguments args)
        {
            if (!args.Config.DecompilationAllowed)
            {
                AnsiConsole.MarkupLine("[red]ERROR:[/] Cannot decompile any programs as decompilation is not enabled!");
                return;
            }
            
            AnsiConsole.MarkupLine($"Decompiling project located at \"{args.Input}\"...");

            await Task.CompletedTask;
        }
    }
}