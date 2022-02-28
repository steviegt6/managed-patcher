using ManagedPatcher.Config;

namespace ManagedPatcher.Tasks.Patch
{
    public class PatchArguments : TaskArguments
    {
        public string Input { get; }
        
        public PatchArguments(ConfigFile config, string input) : base(config)
        {
            Input = input;
        }
    }
}