using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostedService.BackgroundTaskQueues
{
    public class MonitorLoop
    {
        private readonly IBackgroundTaskQueue backgroundTaskQueue;
        private readonly ILogger<MonitorLoop> logger;
        private readonly CancellationToken cancellationToken;

        public MonitorLoop(IBackgroundTaskQueue backgroundTaskQueue,
                           ILogger<MonitorLoop> logger,
                           IHostApplicationLifetime hostApplicationLifetime)
        {
            this.backgroundTaskQueue = backgroundTaskQueue;
            this.logger = logger;
            cancellationToken = hostApplicationLifetime.ApplicationStopping;
        }

        public void StartMonitorLoop()
        {
            logger.LogInformation("Monitor Loop is starting.");

            Task.Run(() => Monitor());
        }

        public void Monitor()
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var keyStroke = Console.ReadKey();
                //WKeyCommand(keyStroke);
                if (keyStroke.Key == ConsoleKey.W)
                {
                    backgroundTaskQueue.QueueBackgroundWorkItem(async token =>
                    {
                        // Simulate three 5-second tasks to complete
                        // for each enqueued work item

                        var delayLoop = 0;
                        var guid = Guid.NewGuid().ToString();

                        logger.LogInformation("Queued Background Task {Guid is starting.}", 
                                              guid);

                        while (!token.IsCancellationRequested)
                        {
                            try
                            {
                                await Task.Delay(TimeSpan.FromSeconds(5), token);
                            }
                            catch (OperationCanceledException)
                            {
                                // Prevent thrwoing if the Delay is cancelled
                            }

                            delayLoop++;

                            logger.LogInformation("Queued Background Ttask {Guid} is running. {DelayLoop}/3",
                                                  guid,
                                                  delayLoop);
                        }

                        if (delayLoop == 3)
                        {
                            logger.LogInformation("Queued Background Task {Guid} is complete.",
                                                  guid);
                        }
                        else
                        {
                            logger.LogInformation("Queued Background Task {Guid} was cancelled.",
                                                 guid);
                        }
                    });
                }
            }
        }

        private static void WKeyCommand(ConsoleKeyInfo keyStroke)
        {
            
        }
    }
}
