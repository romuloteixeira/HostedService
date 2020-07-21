using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1.ScopedProcessingServices
{
    public class ScopedProcessingService : IScopedProcessingService
    {
        private readonly ILogger<ScopedProcessingService> logger;
        private int executionCount = 0;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger)
        {
            this.logger = logger;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            logger.LogInformation(" >> Scoped Processing Service started.");
            
            while (cancellationToken.IsCancellationRequested)
            {
                executionCount++;

                logger.LogInformation($" >> Scoped Processing Service is working. Count: {executionCount}");

                await Task.Delay(millisecondsDelay: 1000, cancellationToken: cancellationToken);
            }
        }
    }
}
