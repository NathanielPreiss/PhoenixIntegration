using Microsoft.Extensions.Hosting;
using NServiceBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Leviathan.Sync
{
    public class NsbHostedService : IHostedService
    {
        private readonly Task<IStartableEndpoint> _endpointTask;
        private IEndpointInstance _endpoint;

        public NsbHostedService(EndpointConfiguration configuration)
        {
            _endpointTask = Endpoint.Create(configuration);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return;

            var endpoint = await _endpointTask;

            if (cancellationToken.IsCancellationRequested)
                return;

            _endpoint = await endpoint.Start();

            await _endpoint.SendLocal(new ScheduleSync
            {
                IntegrationId = Guid.NewGuid()
            });
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_endpoint != null)
                await _endpoint.Stop();
        }
    }
}
