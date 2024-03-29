﻿using System;
using NServiceBus;
using System.Threading.Tasks;

namespace Leviathan.Sync
{
    public class IntegrateEmployeeHandler : IHandleMessages<IntegrateEmployee>
    {
        private readonly ILeviathanSyncRepo _repo;

        public IntegrateEmployeeHandler(ILeviathanSyncRepo repo)
        {
            _repo = repo;
        }

        public async Task Handle(IntegrateEmployee message, IMessageHandlerContext context)
        {
            var phoenixId = Guid.NewGuid();

            await _repo.CreateIntegrationMap(phoenixId, message.Id);

            var employee = new LeviathanEmployee
            {
                Id = message.Id,
                FirstName = message.FirstName,
                LastName = message.LastName,
                Email = message.Email,
                Role = message.Role
            };

            await _repo.CreateIntegratedEmployeeIfNotExists(employee);
            
            await context.Publish<EmployeeIntegrated>(m =>
            {
                m.Id = phoenixId;
                m.ExternalId = message.Id;
                m.FirstName = message.FirstName;
                m.LastName = message.LastName;
                m.Email = message.Email;
                m.Role = message.Role;
            });
        }
    }
}
