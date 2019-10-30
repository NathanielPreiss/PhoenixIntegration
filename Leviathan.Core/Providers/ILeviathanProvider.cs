using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leviathan
{
    public interface ILeviathanProvider
    {
        Task<IEnumerable<EmployeeResponse>> GetEmployees();
        Task<EmployeeResponse> CreateEmployee(string firstName, string lastName, string telephone, string role);
    }
}
