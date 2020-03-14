using CrossTech.Core.Repository.Implementations;
using CrossTechTask.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossTechTask.DAL.Service.Implementation
{
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(ICrossTechDbContext crossTechDbContext) : base(crossTechDbContext.Positions)
        {

        }

        public async Task<List<Position>> GetAll()
        {
            return await DbSet.ToListAsync();
        }
    }
}
