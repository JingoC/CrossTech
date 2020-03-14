using CrossTech.ClientApi.Models.Authorization;
using CrossTechTask.DAL.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrossTech.WebApi.Controllers
{
    [Route("authorization")]
    public class AuthorizationController
    {
        private readonly IUserRepository _userRepository;
        public AuthorizationController(
            IUserRepository userRepository
            )
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<LoginResponse> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.GetByLoginAndPasswordAsync(request.Login, request.Password);

            if (user == null) return new LoginResponse() { IsSuccess = false, Message = "Проверьте правильность ввода логина и пароля" };

            return new LoginResponse() { IsSuccess = true, User = user };
        }

        [HttpPost("get-access-token")]
        public async Task<GetAccessTokenResponse> GetAccessToken([FromBody] GetAccessTokenRequest request)
        {
            var user = await _userRepository.GetByLoginAndPasswordAsync(request.Login, request.Password);

            if (user == null) return new GetAccessTokenResponse() { IsSuccess = false, Message = "Проверьте правильность ввода логина и пароля" };

            return new GetAccessTokenResponse() { IsSuccess = true, AccessToken = user.AccessToken };
        }
    }
}
