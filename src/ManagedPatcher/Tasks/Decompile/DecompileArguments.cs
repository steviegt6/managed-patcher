using ManagedPatcher.Config;

namespace ManagedPatcher.Tasks.Decompile
{
    public class DecompileArguments : TaskArguments
    {
        public string Input { get; }

        public DecompileArguments(ConfigFile config, string input) : base(config)
        {
            Input = input;
        }
    }
}