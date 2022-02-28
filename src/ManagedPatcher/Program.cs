using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CliFx;
using Spectre.Console;

namespace ManagedPatcher
{
    public static class Program
    {
        /// <summary>
        ///     Whether this program is being automated on a server.
        /// </summary>
        public static bool IsServer { get; private set; }

        public static async Task<int> Main(string[] args)
        {
            AnsiConsole.MarkupLine($"[gray]Working directory: {Directory.GetCurrentDirectory()}[/]");
            
            IsServer = args.Contains("--server") || args.Contains("-s");
            int errorCode = await new CliApplicationBuilder().AddCommandsFromThisAssembly().Build().RunAsync(args);

            if (errorCode == 0 || IsServer) 
                return errorCode;
            
            AnsiConsole.MarkupLine("\n[white]Please press any key to exit.[/]");
            Console.ReadKey(true);

            return errorCode;
        }
    }
}