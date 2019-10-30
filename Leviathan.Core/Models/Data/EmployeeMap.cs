using System;

namespace Leviathan
{
    public class EmployeeMap
    {
        public Guid PhoenixId { get; set; }
        public Guid LeviathanId { get; set; }

        public LeviathanEmployee Employee { get; set; }
    }
}
