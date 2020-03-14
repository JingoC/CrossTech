using CrossTech.Core.Repository.Implementations;
using CrossTechTask.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossTechTask.DAL.Service.Implementation
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ICrossTechDbContext crossTechDbContext) : base(crossTechDbContext.Employees)
        {

        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
