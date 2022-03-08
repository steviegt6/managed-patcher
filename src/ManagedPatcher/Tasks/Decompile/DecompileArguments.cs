using System.Collections.Generic;
using ManagedPatcher.Config;

namespace ManagedPatcher.Tasks.Decompile
{
    public class DecompileArguments : TaskArguments
    {
        public readonly Dictionary<string, string> PathOverrides;
        
        public DecompileArguments(ConfigFile config, Dictionary<string, string> pathOverrides) : base(config)
        {
            PathOverrides = pathOverrides;
        }
    }
}