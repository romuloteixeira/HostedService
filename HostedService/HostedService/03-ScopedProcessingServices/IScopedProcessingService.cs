using System.Threading;
using System.Threading.Tasks;

namespace HostedService.ScopedProcessingServices
{
    public interface IScopedProcessingService
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}
