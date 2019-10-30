using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Leviathan.Sync
{
    public partial class LeviathanSyncSaga : IAmStartedByMessages<ScheduleSync>
    {
        public async Task Handle(ScheduleSync message, IMessageHandlerContext context)
        {
            Data.SyncId = Guid.NewGuid();
            var scheduledTime = DateTime.UtcNow.AddSeconds(10);

            await RequestTimeout(context, scheduledTime, new SyncTimeout
            {
                IntegrationId = Data.IntegrationId,
                SyncId = Data.SyncId
            });
        }
    }
}
