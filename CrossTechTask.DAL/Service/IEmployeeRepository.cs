using CrossTech.Core.Repository;
using CrossTechTask.DAL.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossTechTask.DAL.Service
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
    }
}
