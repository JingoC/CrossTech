using CrossTech.ClientApi.Models.Authorization;
using CrossTech.Core.Repository.Implementations;
using CrossTechTask.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

namespace CrossTechTask.DAL.Service.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ICrossTechDbContext _crossTechDbContext;
        public UserRepository(ICrossTechDbContext crossTechDbContext) : base(crossTechDbContext.Users)
        {
            _crossTechDbContext = crossTechDbContext;
        }

        public async Task<UserModel> GetByLoginAndPasswordAsync(string login, string password)
        {
            var user = await DbSet.FirstOrDefaultAsync(x => x.Login.Equals(login) && x.Password.Equals(password));

            if (user == null) return null;

            var claims = await GetClaimsByUserId(user.Id);

            return new UserModel()
            {
                AccessToken = user.AccessToken,
                Login = user.Login,
                Claims = claims
            };
        }

        public async Task<UserModel> GetByAccessTokenAsync(string accessToken)
        {
            var user = await DbSet.FirstOrDefaultAsync(x => x.AccessToken.Equals(accessToken));

            if (user == null) return null;

            var claims = await GetClaimsByUserId(user.Id);

            return new UserModel()
            {
                AccessToken = user.AccessToken,
                Login = user.Login,
                Claims = claims
            };
        }

        async Task<List<ClaimModel>> GetClaimsByUserId(int userId)
        {
            var claimsQuery = from uc in _crossTechDbContext.UserClaims
                              join c in _crossTechDbContext.Claims on uc.ClaimId equals c.Id

                              where uc.UserId == userId

                              select new ClaimModel()
                              {
                                  Id = c.Id,
                                  Name = c.Name
                              };

            return await claimsQuery.ToListAsync();
        }
    }
}
