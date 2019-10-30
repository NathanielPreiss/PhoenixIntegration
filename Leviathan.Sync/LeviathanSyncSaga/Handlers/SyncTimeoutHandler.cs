using NServiceBus;
using System.Threading.Tasks;

namespace Leviathan.Sync
{
    public partial class LeviathanSyncSaga : IHandleTimeouts<SyncTimeout>
    {
        public async Task Timeout(SyncTimeout message, IMessageHandlerContext context)
        {
            if (Data.SyncId != message.SyncId)
                return;

            var syncTask = context.SendLocal<SyncIntegration>(m =>
            {
                m.IntegrationId = message.IntegrationId;
                m.SyncId = message.SyncId;
            });

            var scheduleTask = context.SendLocal<ScheduleSync>(m =>
            {
                m.IntegrationId = message.IntegrationId;
            });

            await Task.WhenAll(syncTask, scheduleTask);
        }
    }
}
