using CrossTech.ClientApi.Models.Authorization;
using CrossTech.Core.Repository;
using CrossTechTask.DAL.Entity;
using System.Threading.Tasks;

namespace CrossTechTask.DAL.Service
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<UserModel> GetByLoginAndPasswordAsync(string login, string password);
        Task<UserModel> GetByAccessTokenAsync(string accessToken);
    }
}
