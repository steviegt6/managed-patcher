using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.CSharp.ProjectDecompiler;
using ICSharpCode.Decompiler.Metadata;
using ManagedPatcher.Config;
using Spectre.Console;

namespace ManagedPatcher.Tasks.Decompile
{
    public class DecompileTask : PatcherTask<DecompileArguments>
    {
        public override async Task ExecuteAsync(DecompileArguments args)
        {
            DecompilationConfig decomp = args.Config.Decompilation;
            
            if (!decomp.DecompilationEnabled)
            {
                AnsiConsole.MarkupLine("[red]ERROR: Cannot decompile any programs as decompilation is not enabled![/]");
                return;
            }

            if (!Enum.TryParse(decomp.LanguageVersion, out LanguageVersion langVer))
            {
                AnsiConsole.MarkupLine(
                    $"[yellow]WARNING: Failed to parse language version (\"{decomp.LanguageVersion}\"), defaulting to CSharp7_3.[/]"
                );
                
                langVer = LanguageVersion.CSharp7_3;
            }
            
            string? targetFramework = decomp.FrameworkVersion;

            if (!string.IsNullOrEmpty(targetFramework))
            {
                AnsiConsole.MarkupLine(
                    "[gray]DEBUG: Target framework was not specified. Using framework ID detection.[/]"
                );

                targetFramework = null;
            }

            foreach (string key in decomp.DecompileTasks)
            {
                AnsiConsole.MarkupLine($"[gray]DEBUG: Executing decompilation task: \"{key}\"[/]");

                if (!decomp.AssemblyPaths.ContainsKey(key))
                    throw new KeyNotFoundException($"No assembly path key provided: \"{key}\"");
                
                if (!decomp.DecompilationPaths.ContainsKey(key))
                    throw new KeyNotFoundException($"No decompilation path key provided: \"{key}\"");
                
                string asmPath = decomp.AssemblyPaths[key];
                string decompPath = decomp.DecompilationPaths[key];
                
                if (Directory.Exists(decompPath))
                    Directory.Delete(decompPath, true);
                
                Directory.CreateDirectory(decompPath);

                if (string.IsNullOrEmpty(asmPath))
                {
                    if (args.PathOverrides.ContainsKey(asmPath)) 
                        asmPath = args.PathOverrides[key];
                    else if (!Program.IsServer)
                    {
                        asmPath = AnsiConsole.Ask<string>($"Enter the path to the assembly \"{key}\":");

                        if (asmPath.StartsWith('"') && asmPath.EndsWith('"'))
                        {
                            asmPath = asmPath.Remove(0, 1);
                            asmPath = asmPath.Remove(asmPath.Length - 1, 1);
                        }
                    }
                    else
                        throw new InvalidOperationException("Cannot prompt user input in a server environment.");
                }

                PEFile module = new(asmPath);
                UniversalAssemblyResolver resolver = new(
                    asmPath,
                    true,
                    targetFramework ?? module.Reader.DetectTargetFrameworkId()
                );

                DirectoryInfo dir = new(Path.GetDirectoryName(asmPath) ?? "");
                
                foreach (DirectoryInfo directory in dir.EnumerateDirectories("*", SearchOption.AllDirectories))
                    resolver.AddSearchDirectory(directory.FullName);

                DecompilerSettings settings = new(langVer) {ThrowOnAssemblyResolveErrors = false};

                WholeProjectDecompiler decompiler = new(settings, resolver, resolver, null);
                decompiler.Settings.SetLanguageVersion(langVer);

                decompiler.DecompileProject(module, decompPath);
            }

            await Task.CompletedTask;
        }
    }
}