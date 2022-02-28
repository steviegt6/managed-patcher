using ManagedPatcher.Config;

namespace ManagedPatcher.Tasks.Diff
{
    public class DiffArguments : TaskArguments
    {
        public ConfigFile Config { get; }
        
        public DiffArguments(ConfigFile config)
        {
            Config = config;
        }
    }
}