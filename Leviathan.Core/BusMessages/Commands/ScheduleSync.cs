using NServiceBus;
using System;

namespace Leviathan
{
    public class ScheduleSync : ICommand
    {
        public Guid IntegrationId { get; set; }
    }
}
