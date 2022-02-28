using ManagedPatcher.Config;

namespace ManagedPatcher.Tasks.Diff
{
    public class DiffArguments : TaskArguments
    {
        public string Input { get; }
        
        public DiffArguments(ConfigFile config, string input) : base(config)
        {
            Input = input;
        }
    }
}