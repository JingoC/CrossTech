using CrossTech.Core.Repository;
using CrossTech.Core.Repository.Implementations;
using CrossTechTask.DAL.Entity;
using CrossTechTask.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrossTechTask.DAL.Service.Implementation
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByIdAsync(int id);
        Task<UserExtended> GetByLoginAndPasswordAsync(string login, string password);
        Task<UserExtended> GetExtendedByAccessTokenAsync(string accessToken);
        Task<List<UserExtended>> GetAllAsync();
    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ICrossTechDbContext _crossTechDbContext;
        public UserRepository(ICrossTechDbContext crossTechDbContext) : base(crossTechDbContext.Users)
        {
            _crossTechDbContext = crossTechDbContext;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserExtended> GetByLoginAndPasswordAsync(string login, string password)
        {
            var user = await DbSet.FirstOrDefaultAsync(x => x.Login.Equals(login) && x.Password.Equals(password));

            if (user == null) return null;

            return await GetExtendedByUser(user);
        }

        public async Task<UserExtended> GetExtendedByAccessTokenAsync(string accessToken)
        {
            var user = await DbSet.FirstOrDefaultAsync(x => x.AccessToken.Equals(accessToken));

            if (user == null) return null;

            return await GetExtendedByUser(user);
        }

        public async Task<List<UserExtended>> GetAllAsync()
        {
            var users = await DbSet.ToListAsync();

            var extendedUsers = new List<UserExtended>();

            foreach(var user in users)
            {
                var extendedUser = await GetExtendedByUser(user);
                extendedUsers.Add(extendedUser);
            }

            return extendedUsers;
        }

        async Task<UserExtended> GetExtendedByUser(User user)
        {
            var claimsQuery = from uc in _crossTechDbContext.UserClaims
                              join c in _crossTechDbContext.Claims on uc.ClaimId equals c.Id

                              where uc.UserId == user.Id

                              select c;

            var claims = await claimsQuery.ToListAsync();

            return new UserExtended()
            {
                User = user,
                Claims = claims
            };
        }
    }
}
