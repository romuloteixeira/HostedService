using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1.TimedHostedServices
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<TimedHostedService> logger;
        private Timer timer;
        private int executionCount;

        public TimedHostedService(ILogger<TimedHostedService> logger)
        {
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation(" ** Timed Hosted Service started");

            timer = new Timer(callback: DoWork, state: null, dueTime: TimeSpan.Zero, period: TimeSpan.FromSeconds(2));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            logger.LogInformation($" ** Timed Hosted Service is working. Count: {count} DateTime: {DateTime.UtcNow}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation($" ** Timed Hosted Service is stopping. Total run: {executionCount}");

            timer?.Change(dueTime: Timeout.Infinite, period: 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            logger.LogInformation($" ** Timed Hosted Service is disposing.");
            timer?.Dispose();
        }
    }
}
