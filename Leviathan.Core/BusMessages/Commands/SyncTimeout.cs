using NServiceBus;
using System;

namespace Leviathan
{
    public class SyncTimeout : ICommand
    {
        public Guid IntegrationId { get; set; }
        public Guid SyncId { get; set; }
    }
}
