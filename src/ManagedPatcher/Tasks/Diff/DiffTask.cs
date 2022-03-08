using System.Threading.Tasks;
using Spectre.Console;

namespace ManagedPatcher.Tasks.Diff
{
    public class DiffTask : PatcherTask<DiffArguments>
    {
        public override async Task ExecuteAsync(DiffArguments args)
        {
            await Task.CompletedTask;
        }
    }
}