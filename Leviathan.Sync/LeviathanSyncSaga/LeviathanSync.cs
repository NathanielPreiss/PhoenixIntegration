using NServiceBus;
using System;

namespace Leviathan.Sync
{
    public class LeviathanSync : ContainSagaData
    {
        public virtual Guid IntegrationId { get; set; }
        public virtual Guid SyncId { get; set; }
    }
}
