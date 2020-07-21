using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1.BackgroundTaskQueues
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        public Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();

        }

        public void QueueBackgoundWorkItem(Func<CancellationToken, Task> workItem)
        {
            throw new NotImplementedException();
        }
    }
}
