using System.Threading.Tasks;

namespace ManagedPatcher.Tasks.Decompile
{
    public class DecompileTask : PatcherTask<DecompileArguments>
    {
        public override async Task ExecuteAsync(DecompileArguments args)
        {
            await Task.CompletedTask;
        }
    }
}