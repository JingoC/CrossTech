using CrossTechTask.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace CrossTechTask.DAL.Service.Implementation
{
    public interface ICrossTechDbContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<Position> Positions { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Claim> Claims { get; set; }
        DbSet<UserClaim> UserClaims { get; set; }
    }

    public class CrossTechDbContext : DbContext, ICrossTechDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public CrossTechDbContext(DbContextOptions<CrossTechDbContext> options) : base(options)
        {

        }
    }
}
