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
    ///     A base command providing several command options shared between different commands.
    /// </summary>
    public abstract class BaseCommand : ICommand
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

            ConfigFile? configFile = JsonConvert.DeserializeObject<ConfigFile>(await File.ReadAllTextAsync(ConfigPath));

            if (configFile is null)
                throw new InvalidOperationException($"Could not parse contents of file into a {nameof(ConfigFile)}!");
            
            await ExecuteAsync(configFile);
        }

        public abstract ValueTask ExecuteAsync(ConfigFile config);
    }
}