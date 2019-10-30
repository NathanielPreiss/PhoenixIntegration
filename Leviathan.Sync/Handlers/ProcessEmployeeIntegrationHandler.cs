using NServiceBus;
using System.Threading.Tasks;

namespace Leviathan.Sync
{
    public class ProcessEmployeeIntegrationHandler : IHandleMessages<ProcessEmployeeIntegration>
    {
        private readonly ILeviathanSyncRepo _repo;

        public ProcessEmployeeIntegrationHandler(ILeviathanSyncRepo repo)
        {
            _repo = repo;
        }

        public async Task Handle(ProcessEmployeeIntegration message, IMessageHandlerContext context)
        {
            var integratedEmployee = await _repo.CheckIntegration(message.Id);

            if (integratedEmployee == null)
            {
                await context.SendLocal<IntegrateEmployee>(m =>
                {
                    m.Id = message.Id;
                    m.FirstName = message.FirstName;
                    m.LastName = message.LastName;
                    m.Email = message.Email;
                    m.Role = message.Role;
                });
            }
            else
            {
                var employee = await _repo.GetEmployee(message.Id);

                employee.FirstName = message.FirstName;
                employee.LastName = message.LastName;
                employee.Email = message.Email;
                employee.Role = message.Role;

                await _repo.UpdateEmployee(employee);

                await context.Publish<IntegratedEmployeeUpdated>(m =>
                {
                    m.Id = integratedEmployee.PhoenixId;
                    m.FirstName = message.FirstName;
                    m.LastName = message.LastName;
                    m.Email = message.Email;
                    m.Role = message.Role;
                });
            }
        }
    }
}
