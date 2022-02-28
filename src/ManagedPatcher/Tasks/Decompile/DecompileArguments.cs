using ManagedPatcher.Config;

namespace ManagedPatcher.Tasks.Decompile
{
    public class DecompileArguments : TaskArguments
    {
        public ConfigFile Config { get; }
        
        public DecompileArguments(ConfigFile config)
        {
            Config = config;
        }
    }
}