using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DiffPatch;
using Spectre.Console;

namespace ManagedPatcher.Utilities
{
    public class DirectoryDiffer
    {
        public const string PatchExtension = ".patch";
        public const string DeleteExtension = ".d";
        public const string CreateExtension = ".c";
        
        /// <summary>
        ///     A list of files that should be ignored while diffing.
        /// </summary>
        public readonly List<string> FilesToIgnoreDiff = new() { ".dll" };

        /// <summary>
        ///     Diffs two directories.
        /// </summary>
        /// <param name="original">The original files.</param>
        /// <param name="modified">The modified files.</param>
        /// <param name="patches">The output patch directory.</param>
        public async Task DiffFolders(DirectoryInfo original, DirectoryInfo modified, DirectoryInfo patches)
        {
            original.Create();
            modified.Create();
            patches.Create();

            AnsiConsole.MarkupLine($"[gray]Diffing \"{original}\" against \"{modified}\" using {patches}.[/]");

            FileInfo[] originalFiles = original.EnumerateFiles("*.*", SearchOption.AllDirectories).ToArray();
            FileInfo[] modifiedFiles = modified.EnumerateFiles("*.*", SearchOption.AllDirectories).ToArray();

            List<string> strippedOriginal = SelectFilter(originalFiles, original);
            List<string> strippedUpdated = SelectFilter(modifiedFiles, modified);

            IList<string> toDiff = strippedOriginal.Where(x => !FilesToIgnoreDiff.Any(x.EndsWith)).ToList();
            IList<string> toCreate = strippedUpdated.Where(x => !strippedOriginal.Any(so => so.Equals(x, StringComparison.OrdinalIgnoreCase))).ToArray();
            IList<string> toDelete = strippedOriginal.Where(so => !strippedUpdated.Any(x => x.Equals(so, StringComparison.OrdinalIgnoreCase))).ToArray();

            patches.Delete(true);
            patches.Create();

            LineMatchedDiffer differ = new() {MaxMatchOffset = 5};

            await toDiff.DoEnumerableAsync(p => Diff(differ, original.FullName, modified.FullName, patches.FullName, p));
            await toCreate.DoAsync(p => WriteCreatePatch(modified.FullName, patches.FullName, p));
            await toDelete.DoAsync(p => WriteDeletePatch(patches.FullName, p));

            await Task.CompletedTask;
        }

        private static List<string> SelectFilter(ICollection<string> collection, FileSystemInfo root)
        {
            List<string> items = new(collection.Count);
            items.AddRange(collection
                .Select(x => StripPath(x, root.FullName))
                .Where(y => !y.StartsWith('.') && !y.StartsWith("bin") && !y.StartsWith("obj"))
            );

            return items;
        }

        private static string StripPath(string path, string root) => path.Remove(0, root.Length + 1);

        private static async Task Diff(Differ differ, string originalRoot, string destinationRoot, string patchRoot,
            string shortName)
        {
            try
            {
                // Console.WriteLine($"Diff data: {originalRoot}, {destinationRoot}. {patchRoot}, {shortName}");

                string destinationPath = Path.Combine(destinationRoot, shortName);

                if (!File.Exists(destinationPath))
                    return;

                PatchFile diff = differ.Diff(Path.Combine(originalRoot, shortName), destinationPath, 3,
                    includePaths: false);

                if (!diff.IsEmpty)
                {
                    shortName += PatchExtension;
                    await WriteDiffPatch(patchRoot, shortName, diff.ToString());
                }
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine($"[red]ERROR: Diff \"{shortName}\" failed due to exception:");
                AnsiConsole.WriteException(e);
            }
        }


        private static async Task WriteDiffPatch(string destRoot, string file, string content) => await WritePatch(
            Path.Combine(destRoot, file), file,
            async p => await File.WriteAllTextAsync(p, content));

        private static async Task WriteCreatePatch(string destRoot, string patchesRoot, string file) =>
            await WritePatch(Path.Combine(destRoot, file), file,
                p => Task.Run(() =>
                {
                    string newFilePath = Path.Combine(patchesRoot, $"{file}{PatchExtension}{CreateExtension}");
                    Directory.CreateDirectory(Path.GetDirectoryName(newFilePath)!);
                    File.Copy(p, newFilePath);
                }));

        public static async Task WriteDeletePatch(string patchesRoot, string file) => await WritePatch(
            Path.Combine(patchesRoot, file),
            file,
            _ => Task.Run(() => File.Create(
                Path.Combine(patchesRoot, file + PatchExtension + DeleteExtension)
            ).Close())
        );

        public static async Task WritePatch(string file, string displayPath, Func<string, Task> action)
        {
            AnsiConsole.MarkupLine($"[gray]Creating patch {displayPath}...[/]");

            try
            {
                DirectoryInfo patchDir = new(Path.GetDirectoryName(file)!);
                patchDir.Create();

                await action(file);
            }
            catch (Exception e)
            {
                AnsiConsole.MarkupLine($"[red]ERROR: Patch \"{displayPath}\" failed due to exception:");
                AnsiConsole.WriteException(e);
            }
        }
    }
}