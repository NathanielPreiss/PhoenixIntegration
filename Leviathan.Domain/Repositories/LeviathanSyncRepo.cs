using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Leviathan
{
    public class LeviathanSyncRepo : ILeviathanSyncRepo
    {
        private readonly LeviathanSyncDbContext _context;

        public LeviathanSyncRepo(LeviathanSyncDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeMap> CheckIntegration(Guid leviathanId)
        {
            return await _context.EmployeeMaps.FirstOrDefaultAsync(e => e.LeviathanId == leviathanId);
        }

        public async Task CreateIntegratedEmployeeIfNotExists(LeviathanEmployee employee)
        {
            if (await _context.LeviathanEmployees.AnyAsync(e => e.Id == employee.Id))
                return;

            await _context.LeviathanEmployees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task CreateIntegrationMap(Guid? phoenixId, Guid? leviathanId)
        {
            // TODO
            if (!leviathanId.HasValue)
                return;

            if(await _context.EmployeeMaps.AnyAsync(e => e.LeviathanId == leviathanId))
                return;

            await _context.EmployeeMaps.AddAsync(new EmployeeMap {LeviathanId = leviathanId.Value});
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployee(LeviathanEmployee employee)
        {
            var existingEmployee = await _context.LeviathanEmployees.FirstOrDefaultAsync(e => e.Id == employee.Id);
            if(existingEmployee == null)
                throw new DataException($"Employee could not be found: {employee.Id}");

            _context.LeviathanEmployees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<LeviathanEmployee> GetEmployee(Guid leviathanId)
        {
            return await _context.LeviathanEmployees.FirstOrDefaultAsync(e => e.Id == leviathanId);
        }
    }
}
