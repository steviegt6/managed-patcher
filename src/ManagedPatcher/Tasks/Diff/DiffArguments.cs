using System.Collections.Generic;
using ManagedPatcher.Config;

namespace ManagedPatcher.Tasks.Diff
{
    public class DiffArguments : TaskArguments
    {
        public List<string> Diffs;
        
        public DiffArguments(ConfigFile config, List<string> diffs) : base(config)
        {
            Diffs = diffs;
        }
    }
}