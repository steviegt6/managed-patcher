using System.Threading.Tasks;

namespace ManagedPatcher.Tasks.Setup
{
    public class SetupTask : PatcherTask<SetupArguments>
    {
        public override async Task ExecuteAsync(SetupArguments args)
        {
            await Task.CompletedTask;
        }
    }
}