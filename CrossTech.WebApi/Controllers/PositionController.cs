using CrossTech.ClientApi.Models.Employee;
using CrossTech.ClientApi.Models.Position;
using CrossTechTask.DAL.Entity;
using CrossTechTask.DAL.Service.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CrossTech.WebApi.Controllers
{
    [Route("position")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<EmployeeController> _logger;

        public PositionController(
            IPositionRepository positionRepository,
            IUserRepository userRepository,
            ILogger<EmployeeController> logger
            )
        {
            _positionRepository = positionRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        /// <summary>
        /// Получить список долностей
        /// </summary>
        /// <param name="request">Тело запроса</param>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<GetPositionsResponse> GetAll()
        {
            var positions = await _positionRepository.GetAllAsync();

            return new GetPositionsResponse()
            {
                Positions = positions.Select(x => new PositionModel() { Id = x.Id, Name = x.Name }).ToList(),
                IsSuccess = true
            };
        }

        [HttpPost()]
        public async Task Add([FromBody] AddPositionRequest request)
        {
            await _positionRepository.InsertAsync(new Position()
            {
                Name = request.Position.Name
            });
        }

        [HttpPut()]
        public async Task Update([FromBody] UpdatePositionRequest request)
        {
            var position = await _positionRepository.GetByIdAsync(request.Position.Id.Value);

            if (position != null)
            {
                position.Name = request.Position.Name;

                await _positionRepository.UpdateAsync(position);
            }
        }

        [HttpDelete()]
        public async Task Delete([FromBody] DeletePositionRequest request)
        {
            var position = await _positionRepository.GetByIdAsync(request.Id);

            if (position != null)
            {
                await _positionRepository.DeleteAsync(position);
            }
        }
    }
}