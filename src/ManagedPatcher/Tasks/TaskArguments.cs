using ManagedPatcher.Config;

namespace ManagedPatcher.Tasks
{
    /// <summary>
    ///     Represents a task's arguments.
    /// </summary>
    public abstract class TaskArguments
    {
        public ConfigFile Config { get; }
        
        protected TaskArguments(ConfigFile config)
        {
            Config = config;
        }
    }
}