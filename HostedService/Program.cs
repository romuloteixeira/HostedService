using HostedService.BackgroundTaskQueues;
using HostedService.LoopWorker;
using HostedService.ScopedProcessingServices;
using HostedService.TimedHostedServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HostedService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();

            var monitorLoop = host.Services.GetRequiredService<MonitorLoop>();
            monitorLoop.StartMonitorLoop();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                //.ConfigureServices((hostContext, services) =>
                //{
                //    services.AddHostedService<Worker>();

                //    services.AddHostedService<TimedHostedService>();

                //    services.AddHostedService<Worker2>();
                //}) // threads
                //.ConfigureServices((hostContext, services) =>
                //{
                //    services.AddHostedService<ConsumeScopedServiceHostedService>();
                //    services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
                //})
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MonitorLoop>();
                    services.AddHostedService<QueuedHostedService>();
                    services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
                });

            //var host = hostBuilder.Build();

            //var monitorLoop = host.Services.GetRequiredService<MonitorLoop>();
            //monitorLoop.StartMonitorLoop();

            return hostBuilder;
        }
    }
}
