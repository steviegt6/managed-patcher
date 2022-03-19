using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DiffPatch;
using Spectre.Console;

namespace ManagedPatcher.Utilities
{
    public static class DirectoryPatcher
    {
        public static async Task PatchDirectories(DirectoryInfo patches, DirectoryInfo destination)
        {
            patches.Create();

            if (!destination.Exists)
                return;
            
            AnsiConsole.MarkupLine($"[gray]Patching directory \"{destination}\" from \"{patches}\".[/]");

            FileInfo[] files = patches.GetFiles("*.patch", SearchOption.AllDirectories);

            if (files.Length == 0)
                return;

            List<Task> tasks = new(files.Length);
            tasks.AddRange(files.Select(file => PatchFile(destination, patches, file)));

            await Task.WhenAll(tasks);
        }
        
        public static async Task PatchFile(DirectoryInfo destination, DirectoryInfo patches, FileInfo patch)
        {
            string shortName = patch.FullName[(patches.FullName.Length + 1)..];

            DirectoryInfo destFolder = new(Path.Combine(destination.FullName, Path.GetDirectoryName(shortName) ?? ""));
            destFolder.Create();

            string extension = Path.GetExtension(shortName);
            FileInfo destFile = new(Path.Combine(destFolder.FullName, Path.GetFileNameWithoutExtension(shortName)));

            AnsiConsole.MarkupLine($"[gray]Applying patch {shortName}...[/]");

            if (extension.Equals(DirectoryDiffer.PatchExtension))
            {
                FilePatcher patchFile = FilePatcher.FromPatchFile(patch.ToString());
                string[] lines =
                    new Patcher(
                        patchFile.PatchFile.Patches, await File.ReadAllLinesAsync(destFile.ToString())
                    ).Patch(default).ResultLines;

                await File.WriteAllLinesAsync(destFile.FullName, lines);
            }
            else
            {
                destFile = new FileInfo(Path.Combine(
                    destFile.DirectoryName!,
                    Path.GetFileNameWithoutExtension(destFile.Name)));

                switch (extension)
                {
                    case DirectoryDiffer.CreateExtension:
                        patch.CopyTo(destFile.ToString(), true);
                        break;

                    case DirectoryDiffer.DeleteExtension:
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