using CrossTech.Core.Repository;
using CrossTech.Core.Repository.Implementations;
using CrossTechTask.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossTechTask.DAL.Service.Implementation
{
    public interface IPositionRepository : IBaseRepository<Position>
    {
        Task<Position> GetByIdAsync(int id);
        Task<List<Position>> GetAllAsync();
    }

    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(ICrossTechDbContext crossTechDbContext) : base(crossTechDbContext.Positions)
        {

        }

        public async Task<List<Position>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<Position> GetByIdAsync(int id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
