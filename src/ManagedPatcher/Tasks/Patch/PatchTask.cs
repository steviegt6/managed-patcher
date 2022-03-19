using System.Threading.Tasks;

namespace ManagedPatcher.Tasks.Patch
{
    public class PatchTask : PatcherTask<PatchArguments>
    {
        public override async Task ExecuteAsync(PatchArguments args)
        {
            await Task.CompletedTask;
        }
    }
}