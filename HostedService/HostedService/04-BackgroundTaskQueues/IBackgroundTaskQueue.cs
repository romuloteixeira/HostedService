using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostedService.BackgroundTaskQueues
{
    public interface IBackgroundTaskQueue
    {
        void QueueBackgoundWorkItem(Func<CancellationToken, Task> workItem);

        Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }
}
