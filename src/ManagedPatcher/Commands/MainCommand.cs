using System;
using System.IO;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using ManagedPatcher.Config;
using ManagedPatcher.Tasks.Decompile;
using ManagedPatcher.Tasks.Diff;
using ManagedPatcher.Tasks.Patch;
using ManagedPatcher.Tasks.Setup;
using Newtonsoft.Json;
using Spectre.Console;

namespace ManagedPatcher.Commands
{
    /// <summary>
    ///     Our core entrypoint command, which redirects the workload elsewhere.
    /// </summary>
    [Command]
    public class MainCommand : ICommand
    {
        /// <summary>
        ///     A dummy property used to display the "server" option in the help command display <br />
        ///     Use <see cref="Program.IsServer"/> instead.
        /// </summary>
        [CommandOption("server", 's', Description = "Defines that this instance is running under a server.")]
        [Obsolete($"Use {nameof(Program.IsServer)} instead!")]
        public bool IsServer { get; init; }

        [CommandOption("config", 'c', Description = "The path to the configuration file.")]
        public string ConfigPath { get; init; } = "config.json";

        [CommandOption("mode", 'm', Description = "The ManagedPatcher mode (decompile, diff, patch, setup).")]
        public string Mode { get; set; } = "";

        [CommandOption("input", Description = "Input used for various modes.")]
        public string Input { get; set; } = "";

        public async ValueTask ExecuteAsync(IConsole console)
        {
            if (!File.Exists(ConfigPath))
                throw new FileNotFoundException($"No file found at path \"{ConfigPath}\"!");

            ConfigFile? configFile = JsonConvert.DeserializeObject<ConfigFile>(await File.ReadAllTextAsync(ConfigPath));

            if (configFile is null)
                throw new InvalidOperationException($"Could not parse contents of file into a {nameof(ConfigFile)}!");

            if (string.IsNullOrEmpty(Mode) || !ValidateMode(Mode))
            {
                Mode = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Choose a mode:")
                        .AddChoices("setup", "decompile", "diff", "patch")
                );
            }

            switch (Mode)
            {
                case "setup":
                {
                    using SetupTask task = new();
                    await task.ExecuteAsync(new SetupArguments(configFile, Input));
                    break;
                }

                case "decompile":
                {
                    using DecompileTask task = new();
                    await task.ExecuteAsync(new DecompileArguments(configFile, Input));
                    break;
                }

                case "diff":
                {
                    using DiffTask task = new();
                    await task.ExecuteAsync(new DiffArguments(configFile, Input));
                    break;
                }

                case "patch":
                {
                    using PatchTask task = new();
                    await task.ExecuteAsync(new PatchArguments(configFile, Input));
                    break;
                }
            }
        }

        private static bool ValidateMode(string mode)
        {
            switch (mode)
            {
                case "patch":
                case "decompile":
                case "diff":
                case "setup":
                    return true;
            }

            return false;
        }
    }
}