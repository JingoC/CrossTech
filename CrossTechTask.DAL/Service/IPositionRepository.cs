using CrossTech.Core.Repository;
using CrossTechTask.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrossTechTask.DAL.Service
{
    public interface IPositionRepository : IBaseRepository<Position>
    {
        Task<List<Position>> GetAll();
    }
}
