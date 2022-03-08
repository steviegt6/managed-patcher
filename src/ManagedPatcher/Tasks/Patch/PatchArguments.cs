using ManagedPatcher.Config;

namespace ManagedPatcher.Tasks.Patch
{
    public class PatchArguments : TaskArguments
    {
        public PatchArguments(ConfigFile config, string input) : base(config)
        {
        }
    }
}