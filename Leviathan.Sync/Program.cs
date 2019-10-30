using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;
using System.IO;

namespace Leviathan.Sync
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var host = new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.AddJsonFile("appSettings.json", false, true);

                    if (args != null)
                        configuration.AddCommandLine(args);
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>((hostContext, builder) =>
                    {
                        var syncConfiguration = hostContext.Configuration.Get<SyncConfiguration>();

                        var restClient = new RestClient(syncConfiguration.ApiRoot);
                        var provider = new LeviathanProvider(syncConfiguration.ApiUser, syncConfiguration.ApiKey, restClient);
                        builder.RegisterInstance(provider).As<ILeviathanProvider>();

                        builder.RegisterModule(new NsbModule(syncConfiguration.SyncEndpointName));

                        builder.RegisterType<LeviathanSyncDbContext>().AsSelf();
                        builder.RegisterType<LeviathanSyncRepo>().As<ILeviathanSyncRepo>();
                    })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<NsbHostedService>();
                })
                .Build();

            host.Run();
        }
    }
}
