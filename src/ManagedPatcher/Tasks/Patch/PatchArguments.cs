using ManagedPatcher.Config;

namespace ManagedPatcher.Tasks.Patch
{
    public class PatchArguments : TaskArguments
    {
        public PatchArguments(ConfigFile config) : base(config)
        {
        }
    }
}