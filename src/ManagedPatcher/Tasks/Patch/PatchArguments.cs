using ManagedPatcher.Config;

namespace ManagedPatcher.Tasks.Patch
{
    public class PatchArguments : TaskArguments
    {
        public ConfigFile Config { get; }
        
        public PatchArguments(ConfigFile config)
        {
            Config = config;
        }
    }
}