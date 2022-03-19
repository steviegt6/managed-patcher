using System.Threading.Tasks;
using ManagedPatcher.Tasks.Decompile;
using Spectre.Console;

namespace ManagedPatcher.Tasks.Setup
{
    public class SetupTask : PatcherTask<SetupArguments>
    {
        public override async Task ExecuteAsync(SetupArguments args)
        {
            if (args.Config.Decompilation.DecompilationEnabled)
            {
                AnsiConsole.WriteLine("Executing decompilation task...");

                using DecompileTask task = new();
                await task.ExecuteAsync(new DecompileArguments(args.Config, args.PathOverrides));
            }
            
            /*AnsiConsole.WriteLine("Executing patch tasks...");

            foreach (string patchTask in args.Config.Patches.Keys)
            {
                using PatchTask task = new();
                await task.ExecuteAsync(new PatchArguments(args.Config));
            }*/
            
            await Task.CompletedTask;
        }
    }
}