using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1.ScopedProcessingServices
{
    public class ConsumeScopedServiceHostedService : BackgroundService
    {
        private readonly ILogger<ConsumeScopedServiceHostedService> logger;

        public ConsumeScopedServiceHostedService(IServiceProvider serviceProvider, 
                                                 ILogger<ConsumeScopedServiceHostedService> logger)
        {
            ServiceProvider = serviceProvider;
            this.logger = logger;
        }

        public IServiceProvider ServiceProvider { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation(" >> Consume Scoped Hosted Service running.");

            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            logger.LogInformation(" >> Consume Scoped Service Hosted Service is working.");

            using (var scope = ServiceProvider.CreateScope())
            {
                var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IScopedProcessingService>();

                await scopedProcessingService.DoWork(cancellationToken: stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation(" >> Consume Scoped Service Hosted Service is stopping.");

            await Task.CompletedTask;
        }
    }
}
