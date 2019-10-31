using Leviathan;
using NServiceBus;
using System.Threading.Tasks;

namespace Phoenix.Sync
{
    public class EmployeeIntegratedHandler : IHandleMessages<EmployeeIntegrated>
    {
        private readonly IPhoenixRepo _repo;

        public EmployeeIntegratedHandler(IPhoenixRepo repo)
        {
            _repo = repo;
        }

        public async Task Handle(EmployeeIntegrated message, IMessageHandlerContext context)
        {
            var employee = new Employee
            {
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                Role = message.Role,
                Email = message.Email
            };

            await _repo.CreateEmployeeIfNotExists(employee);
        }
    }
}
