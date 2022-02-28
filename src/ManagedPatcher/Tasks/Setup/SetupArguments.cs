using ManagedPatcher.Config;

namespace ManagedPatcher.Tasks.Setup
{
    public class SetupArguments : TaskArguments
    {
        public string Input { get; }
        
        public SetupArguments(ConfigFile config, string input) : base(config)
        {
            Input = input;
        }
    }
}