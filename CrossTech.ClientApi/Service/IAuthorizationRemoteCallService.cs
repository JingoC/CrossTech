using CrossTech.ClientApi.Models.Authorization;
using CrossTech.Core.Service;
using System.Threading.Tasks;

namespace CrossTech.ClientApi.Service
{
    public interface IAuthorizationRemoteCallService : IBaseRemoteCallService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<GetAccessTokenResponse> GetAccessToken(GetAccessTokenRequest request);
    }
}
