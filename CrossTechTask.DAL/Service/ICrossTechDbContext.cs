using CrossTechTask.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace CrossTechTask.DAL.Service
{
    public interface ICrossTechDbContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<Position> Positions { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Claim> Claims { get; set; }
        DbSet<UserClaim> UserClaims { get; set; }
    }
}
