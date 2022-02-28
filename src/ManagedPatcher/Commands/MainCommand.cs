using System;
using System.IO;
using System.Threading.Tasks;
using CliFx;
using CliFx.Attributes;
using CliFx.Infrastructure;
using ManagedPatcher.Config;
using Newtonsoft.Json;

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

        public async ValueTask ExecuteAsync(IConsole console)
        {
            if (!File.Exists(ConfigPath))
                throw new FileNotFoundException($"No file found at path \"{ConfigPath}\"!");

            ConfigFile? file = JsonConvert.DeserializeObject<ConfigFile>(await File.ReadAllTextAsync(ConfigPath));

            if (file is null)
                throw new InvalidOperationException($"Could not parse contents of file into a {nameof(ConfigFile)}!");
            
            await console.Output.WriteLineAsync("");
        }
    }
}