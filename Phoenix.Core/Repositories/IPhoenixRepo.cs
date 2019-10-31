using System;
using System.Threading.Tasks;

namespace Phoenix
{
    public interface IPhoenixRepo
    {
        Task CreateEmployeeIfNotExists(Employee employee);
        Task UpdateEmployee(Employee employee);
        Task<Employee> GetEmployee(Guid id);
    }
}
