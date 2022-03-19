using System.Collections.Generic;
using System.Threading.Tasks;
using ManagedPatcher.Tasks.Decompile;
using ManagedPatcher.Tasks.Diff;
using ManagedPatcher.Tasks.Patch;
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

            AnsiConsole.WriteLine("Executing patch tasks...");

            using (PatchTask patcher = new()) 
                await patcher.ExecuteAsync(new PatchArguments(args.Config, new List<string>()));
            
            AnsiConsole.WriteLine("Executing diff tasks...");

            using (DiffTask differ = new())
                await differ.ExecuteAsync(new DiffArguments(args.Config, new List<string>()));

            await Task.CompletedTask;
        }
    }
}