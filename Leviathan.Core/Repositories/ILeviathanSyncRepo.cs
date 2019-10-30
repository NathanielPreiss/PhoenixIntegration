using System;
using System.Threading.Tasks;

namespace Leviathan
{
    public interface ILeviathanSyncRepo
    {
        Task<EmployeeMap> CheckIntegration(Guid leviathanId);
        Task CreateIntegratedEmployeeIfNotExists(LeviathanEmployee employee);
        Task CreateIntegrationMap(Guid? phoenixId, Guid? leviathanId);
        Task UpdateEmployee(LeviathanEmployee employee);
        Task<LeviathanEmployee> GetEmployee(Guid leviathanId);
    }
}
