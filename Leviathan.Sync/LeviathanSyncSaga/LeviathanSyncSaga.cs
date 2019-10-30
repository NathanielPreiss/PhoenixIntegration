using NServiceBus;

namespace Leviathan.Sync
{
    public partial class LeviathanSyncSaga : Saga<LeviathanSync>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<LeviathanSync> mapper)
        {
            mapper.ConfigureMapping<ScheduleSync>(message => message.IntegrationId).ToSaga(saga => saga.IntegrationId);
            mapper.ConfigureMapping<SyncTimeout>(message => message.IntegrationId).ToSaga(saga => saga.IntegrationId);
        }
    }
}
