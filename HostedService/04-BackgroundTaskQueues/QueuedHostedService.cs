using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostedService.BackgroundTaskQueues
{
    public class QueuedHostedService : BackgroundService
    {
        private readonly ILogger<QueuedHostedService> logger;

        public QueuedHostedService(IBackgroundTaskQueue backgroundTaskQueue, ILogger<QueuedHostedService> logger)
        {
            this.logger = logger;
            BackgroundTaskQueue = backgroundTaskQueue;
        }

        public IBackgroundTaskQueue BackgroundTaskQueue { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation(
                                $"Queued Hosted Service is running.{Environment.NewLine}" +
                                $"{Environment.NewLine}Tap W to add a work item to the " +
                                $"background queue.{Environment.NewLine}");

            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await BackgroundTaskQueue.DequeueAsync(stoppingToken);

                try
                {
                    await workItem(stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error occurred excecuting {WorkItem}", nameof(workItem));
                }
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Queued Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
