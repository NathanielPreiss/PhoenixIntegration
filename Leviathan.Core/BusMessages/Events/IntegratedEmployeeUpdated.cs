using NServiceBus;
using System;

namespace Leviathan
{
    public class IntegratedEmployeeUpdated : IEvent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
