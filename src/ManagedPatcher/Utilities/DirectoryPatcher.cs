using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace ManagedPatcher.Utilities
{
    public class DirectoryPatcher
    {
        public async Task Patch(DirectoryInfo patches, DirectoryInfo destination)
        {
            patches.Create();
            destination.Create();
            
            AnsiConsole.MarkupLine($"Patching directory \"{destination}\" from \"{patches}\".");

            FileInfo[] files = patches.GetFiles("*.patch", SearchOption.AllDirectories);

            if (files.Length == 0)
                return;

            List<Task> tasks = new(files.Length);
            tasks.AddRange(files.Select(file => Patch(destination, patches, file)));

            await Task.WhenAll(tasks);
        }
        
        public static async Task Patch(DirectoryInfo destination, DirectoryInfo patches, FileInfo patch)
        {
            string shortName = patch.FullName[(patches.FullName.Length + 1)..];

            DirectoryInfo destFolder = new(Path.Combine(destination.FullName, Path.GetDirectoryName(shortName) ?? ""));
            destFolder.Create();

            string extension = Path.GetExtension(shortName);
            FileInfo destFile = new(Path.Combine(destFolder.FullName, Path.GetFileNameWithoutExtension(shortName)));

            AnsiConsole.MarkupLine($"[gray]Applying {shortName}[/]");

            if (extension.Equals(DirectoryDiffer.PatchExtension))
            {
                // This is probably inefficient and could be replaced with Streams (Readers/Writers) but the current
                // implementation of DiffPatch kinda sucks :/
                DirectoryDiffer patchFile = DirectoryDiffer.FromPatchFile(patch.ToString());
                string[] lines =
                    new Patcher(patchFile.PatchFile.Patches, await File.ReadAllLinesAsync(destFile.ToString()))
                        .Patch(default)
                        .ResultLines;

                await File.WriteAllLinesAsync(destFile.FullName, lines);
            }
            else
            {
                destFile = new FileInfo(Path.Combine(
                    destFile.DirectoryName!,
                    Path.GetFileNameWithoutExtension(destFile.Name)));

                switch (extension)
                {
                    case StandardDiffer.CreateExtension:
                        patch.CopyTo(destFile.ToString(), true);
                        break;

                    case StandardDiffer.DeleteExtension:
                    {
                        if (destFile.Exists)
                            destFile.Delete();

                        break;
                    }
                }
            }
        }
    }
}