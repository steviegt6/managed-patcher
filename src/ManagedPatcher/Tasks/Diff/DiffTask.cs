using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ManagedPatcher.Config;
using ManagedPatcher.Utilities;
using Spectre.Console;

namespace ManagedPatcher.Tasks.Diff
{
    public class DiffTask : PatcherTask<DiffArguments>
    {
        public override async Task ExecuteAsync(DiffArguments args)
        {
            ConfigFile config = args.Config;

            foreach ((string? name, string[]? paths) in config.Diffs)
            {
                if (paths.Length != 3)
                    throw new InvalidOperationException("Cannot perform diff task when three paths are not provided.");

                string original = paths[0];
                string modified = paths[1];
                string patches = paths[2];
                
                AnsiConsole.MarkupLine($"Executing diff task \"{name}\": {original} -> {modified} (output: {patches})");

                await DirectoryDiffer.DiffFolders(
                    new DirectoryInfo(original),
                    new DirectoryInfo(modified),
                    new DirectoryInfo(patches)
                );
                
                AnsiConsole.MarkupLine($"Finished diff task \"{name}\"!");
            }
            
            await Task.CompletedTask;
        }
    }
}