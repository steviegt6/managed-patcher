using System.Collections.Generic;
using ManagedPatcher.Config;

namespace ManagedPatcher.Tasks.Patch
{
    public class PatchArguments : TaskArguments
    {
        public List<string> Patches;
        
        public PatchArguments(ConfigFile config, List<string> patches) : base(config)
        {
            Patches = patches;
        }
    }
}