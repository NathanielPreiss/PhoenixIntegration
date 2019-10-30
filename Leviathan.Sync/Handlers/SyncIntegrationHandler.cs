using NServiceBus;
using System.Linq;
using System.Threading.Tasks;

namespace Leviathan.Sync
{
    public class SyncIntegrationHandler : IHandleMessages<SyncIntegration>
    {
        private readonly ILeviathanProvider _provider;

        public SyncIntegrationHandler(ILeviathanProvider provider)
        {
            _provider = provider;
        }

        public async Task Handle(SyncIntegration message, IMessageHandlerContext context)
        {
            var employees = await _provider.GetEmployees();

            await Task.WhenAll(employees.Select(e =>
                context.SendLocal<ProcessEmployeeIntegration>(m =>
                {
                    m.Id = e.Id;
                    m.FirstName = e.FirstName;
                    m.LastName = e.LastName;
                    m.Email = e.Email;
                    m.Role = e.Role;
                })
            ));
        }
    }
}
