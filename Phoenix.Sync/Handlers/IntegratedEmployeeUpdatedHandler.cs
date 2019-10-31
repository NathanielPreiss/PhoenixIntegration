using Leviathan;
using NServiceBus;
using System.Threading.Tasks;

namespace Phoenix.Sync
{
    public class IntegratedEmployeeUpdatedHandler : IHandleMessages<IntegratedEmployeeUpdated>
    {
        public IntegratedEmployeeUpdatedHandler()
        {

        }

        public Task Handle(IntegratedEmployeeUpdated message, IMessageHandlerContext context)
        {
            // TODO: Update the record
            throw new System.NotImplementedException();
        }
    }
}
