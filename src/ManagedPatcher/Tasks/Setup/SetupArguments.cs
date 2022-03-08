using System.Collections.Generic;
using ManagedPatcher.Config;

namespace ManagedPatcher.Tasks.Setup
{
    public class SetupArguments : TaskArguments
    {
        public readonly Dictionary<string, string> PathOverrides;
        
        public SetupArguments(ConfigFile config, Dictionary<string, string> pathOverrides) : base(config)
        {
            PathOverrides = pathOverrides;
        }
    }
}