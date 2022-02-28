using System;
using System.Threading.Tasks;

namespace ManagedPatcher.Tasks
{
    /// <summary>
    ///     Represents the raw abstraction of an async patcher task.
    /// </summary>
    /// <typeparam name="T">The argument type.</typeparam>
    public abstract class PatcherTask<T> : IDisposable where T : TaskArguments
    {
        /// <summary>
        ///     Executes the task's job.
        /// </summary>
        /// <param name="args">Any applicable arguments.</param>
        public abstract Task ExecuteAsync(T args);
        
        #region IDisposable Implementation

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}