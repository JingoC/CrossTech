using CrossTech.ClientApi.Models.User;
using CrossTechTask.DAL.Entity;
using CrossTechTask.DAL.Models;
using CrossTechTask.DAL.Service.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CrossTech.WebApi.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<EmployeeController> _logger;

        public UserController(
            IUserRepository userRepository,
            ILogger<EmployeeController> logger
            )
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// Получить список долностей
        /// </summary>
        /// <param name="request">Тело запроса</param>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<GetAllResponse> GetAll()
        {
            var users = await _userRepository.GetAllAsync();

            return new GetAllResponse()
            {
                Users = users.Select(ConvertUserExtendedToUserModel).ToList()
            };
        }

        [HttpPost()]
        public async Task Add([FromBody] AddRequest request)
        {
            await _userRepository.InsertAsync(new User()
            {
                Login = request.Login,
                Password = request.Password
            });
        }

        [HttpPut()]
        public async Task Update([FromBody] UpdateRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user != null)
            {
                user.Login = request.Login;
                user.Password = request.Password;

                await _userRepository.UpdateAsync(user);
            }
        }

        [HttpDelete()]
        public async Task Delete([FromBody] DeleteRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);

            if (user != null)
            {
                await _userRepository.DeleteAsync(user);
            }
        }

        #region private_methods

        private UserModel ConvertUserExtendedToUserModel(UserExtended ue)
        {
            return new UserModel()
            {
                AccessToken = ue.User.AccessToken,
                Login = ue.User.Login,
                Claims = ue.Claims.Select(ConvertClaimToClaimModel).ToList()
            };
        }

        private ClaimModel ConvertClaimToClaimModel(Claim c)
        {
            return new ClaimModel()
            {
                Id = c.Id,
                Name = c.Name
            };
        }

        #endregion
    }
}