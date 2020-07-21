using System.Threading;
using System.Threading.Tasks;

namespace WorkerService1.ScopedProcessingServices
{
    public interface IScopedProcessingService
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}
