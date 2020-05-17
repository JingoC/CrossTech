using CrossTech.ClientApi.Enums;
using CrossTech.ClientApi.Models;
using CrossTech.ClientApi.Models.Employee;
using CrossTechTask.DAL.Entity;
using CrossTechTask.DAL.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            var positions = await _positionRepository.GetAll();

            return new GetPositionsResponse()
            {
                Positions = positions.Select(x => new PositionModel() { Id = x.Id, Name = x.Name }).ToList(),
                IsSuccess = true
            };
        }
    }
}