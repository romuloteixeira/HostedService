using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostedService.LoopWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var errorTime = 7;
            var warningTime = 5;
            while (!stoppingToken.IsCancellationRequested)
            {
                DateTimeOffset now = DateTimeOffset.Now;
                bool isErrorTime = (now.Second % errorTime) == 0;
                bool isWarningTime = (now.Second % warningTime) == 0;
                if (isErrorTime)
                {
                    _logger.LogError("Worker is running at: {time}", now);
                }
                if (isWarningTime)
                {
                    _logger.LogWarning("Worker is running at: {time}", now);
                }

                if (!isErrorTime && !isWarningTime)
                {
                    _logger.LogInformation("Worker is running at: {time}", now);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
