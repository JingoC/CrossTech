using CrossTech.ClientApi.Enums;
using CrossTech.ClientApi.Models;
using CrossTech.ClientApi.Models.Employee;
using CrossTechTask.DAL.Entity;
using CrossTechTask.DAL.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrossTech.WebApi.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly IUserRepository _userRepository;

        public EmployeeController(
            IEmployeeRepository employeeRepository,
            IPositionRepository positionRepository,
            IUserRepository userRepository
            )
        {
            _employeeRepository = employeeRepository;
            _positionRepository = positionRepository;
            _userRepository = userRepository;
        }

        [HttpPost("add")]
        public async Task<BaseResponse> Add([FromBody] AddEmployeeRequest request)
        {
            if (request == null) return BaseResponse.GetFail("Тело запроса пустое");

            var access = await CheckAccessToken(request.AccessToken, new List<ClaimsTypeEnum>() { ClaimsTypeEnum.Admin });
            if (!access.IsSuccess) return access;

            if (request.Employee == null) return BaseResponse.GetFail("Поле 'Сотрудник' в запросе пустое");
            
            if (!request.Employee.Birthday.HasValue) return BaseResponse.GetFail("Дата рождения не задана");
            var validateBirthday = ValidateBirthday(request.Employee.Birthday.Value);
            if (!validateBirthday.IsSuccess) return validateBirthday;

            if (!request.Employee.Sex.HasValue) return BaseResponse.GetFail("Пол не задан");
            if (string.IsNullOrWhiteSpace(request.Employee.FirstName)) return BaseResponse.GetFail("Имя не задано");
            var validateFirstName = ValidateNames(request.Employee.FirstName);
            if (!validateFirstName.IsSuccess) return validateFirstName;

            if (string.IsNullOrWhiteSpace(request.Employee.LastName)) return BaseResponse.GetFail("Фамилия не задана");
            var validateLastName = ValidateNames(request.Employee.LastName);
            if (!validateLastName.IsSuccess) return validateLastName;

            if (!string.IsNullOrWhiteSpace(request.Employee.FatherName))
            {
                var validateFatherName = ValidateNames(request.Employee.FatherName);
                if (!validateFatherName.IsSuccess) return validateFatherName;
            }

            if (string.IsNullOrWhiteSpace(request.Employee.Phone)) return BaseResponse.GetFail("Телефон не задан");
            if (!request.Employee.PositionId.HasValue) return BaseResponse.GetFail("Должность не задана");

            await _employeeRepository.InsertAsync(ConvertEmployeeModelToEmployee(request.Employee));

            return new BaseResponse() { IsSuccess = true };
        }

        [HttpPost("update")]
        public async Task<BaseResponse> Update([FromBody] UpdateEmployeeRequest request)
        {
            if (request == null) return BaseResponse.GetFail("Тело запроса пустое");

            var access = await CheckAccessToken(request.AccessToken, new List<ClaimsTypeEnum>() { ClaimsTypeEnum.Admin });
            if (!access.IsSuccess) return access;

            if (request.Employee == null) return BaseResponse.GetFail("Поле 'Сотрудник' в запросе пустое");

            var e = request.Employee;

            if (!e.Id.HasValue) return BaseResponse.GetFail("Не задан Id обновляемого сотрудника");

            var id = e.Id.Value;
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null) return BaseResponse.GetFail($"Сотрудник с id='{id}' не найден");

            if (e.Birthday.HasValue)
            {
                var validate = ValidateBirthday(e.Birthday.Value);
                if (!validate.IsSuccess) return validate;

                employee.Birthday = e.Birthday.Value;
            }

            if (e.FirstName != null)
            {
                var validate = ValidateNames(e.FirstName);
                if (!validate.IsSuccess) return validate;

                employee.FirstName = e.FirstName;
            }
            if (e.FatherName != null)
            {
                var validate = ValidateNames(e.FatherName);
                if (!validate.IsSuccess) return validate;

                employee.FatherName = e.FatherName;
            }
            if (e.LastName != null)
            {
                var validate = ValidateNames(e.LastName);
                if (!validate.IsSuccess) return validate;

                employee.LastName = e.LastName;
            }
            if (e.Phone != null) employee.Phone = e.Phone;
            if (e.PositionId.HasValue) employee.PositionId = (PositionTypeEnum)e.PositionId.Value;
            if (e.Sex.HasValue) employee.Sex = (SexTypeEnum)e.Sex.Value;

            try
            {
                await _employeeRepository.UpdateAsync(employee);
            }
            catch
            {
                return BaseResponse.GetFail("Непредвиденная ошибка при обновлении пользователя");
            }
            
            return new BaseResponse() { IsSuccess = true };
        }

        [HttpPost("get")]
        public async Task<GetEmployeesResponse> Get([FromBody] BaseRequest request)
        {
            var access = await CheckAccessToken(request.AccessToken, null);
            if (!access.IsSuccess) return new GetEmployeesResponse() { IsSuccess = false, Message = access.Message };

            var employees = await _employeeRepository.GetAllAsync();
            var employeeModels = employees.Select(x => ConvertEmployeeToEmployeeModel(x)).ToList();
            
            return new GetEmployeesResponse()
            {
                IsSuccess = true,
                Employees = employeeModels
            };
        }

        [HttpPost("delete")]
        public async Task<BaseResponse> Delete([FromBody] DeleteEmployeeRequest request)
        {
            if (request == null) return BaseResponse.GetFail("Тело запроса пустое");

            var access = await CheckAccessToken(request.AccessToken, new List<ClaimsTypeEnum>() { ClaimsTypeEnum.Admin });
            if (!access.IsSuccess) return access;

            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            
            if (employee == null) return BaseResponse.GetFail($"Сотрудник с id='{request.Id}' не найден");

            try
            {
                await _employeeRepository.DeleteAsync(employee);
            }
            catch
            {
                return BaseResponse.GetFail("Непредвиденная ошибка при удалении пользователя");
            }

            return new BaseResponse() { IsSuccess = true };
        }

        [HttpPost("get-positions")]
        public async Task<GetPositionsResponse> GetPositions([FromBody] BaseRequest request)
        {
            var access = await CheckAccessToken(request.AccessToken, null);
            if (!access.IsSuccess) return new GetPositionsResponse() { IsSuccess = false, Message = access.Message };

            var positions = await _positionRepository.GetAll();

            return new GetPositionsResponse()
            {
                Positions = positions.Select(x => new PositionModel() { Id = x.Id, Name = x.Name }).ToList(),
                IsSuccess = true
            };
        }

        #region private methods

        BaseResponse ValidateNames(string name)
        {
            if (Regex.IsMatch(name, "[^а-яА-Я]+"))
                return BaseResponse.GetFail("Имя\\Отчество\\Фамилия должны состоят только из букв Русского алфавита");

            return new BaseResponse() { IsSuccess = true };
        }

        BaseResponse ValidateBirthday(DateTime dt)
        {
            var firstDate = new DateTime(1940, 1, 1, 0, 0, 0);
            var lastDate = new DateTime(2005, 12, 31, 23, 59, 59);

            if ((dt < firstDate) || (dt > lastDate))
                return BaseResponse.GetFail($"Дата рождения за пределами допустимого диапозона {firstDate.Date} - {lastDate.Date}");

            return new BaseResponse() { IsSuccess = true };
        }

        Employee ConvertEmployeeModelToEmployee(EmployeeModel x)
        {
            return new Employee()
            {
                Birthday = x.Birthday.Value,
                FatherName = x.FatherName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Phone = x.Phone,
                PositionId = (PositionTypeEnum)x.PositionId.Value,
                Sex = (SexTypeEnum)x.Sex.Value
            };
        }

        EmployeeModel ConvertEmployeeToEmployeeModel(Employee x)
        {
            return new EmployeeModel()
            {
                Id = x.Id,
                Birthday = x.Birthday,
                FatherName = x.FatherName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Phone = x.Phone,
                PositionId = x.PositionId,
                Sex = x.Sex
            };
        }

        async Task<BaseResponse> CheckAccessToken(string accessToken, List<ClaimsTypeEnum> claimsAvailable)
        {
#if false
            if (string.IsNullOrWhiteSpace(accessToken)) return BaseResponse.GetFail("AccessToken не передан");

            var user = await _userRepository.GetByAccessTokenAsync(accessToken);

            if (user == null) return BaseResponse.GetFail($"Пользователь с AccessToken='{accessToken}' не найден");

            if (claimsAvailable != null && claimsAvailable.Count > 0)
            {
                var isAvailable = user.Claims.Any(x => claimsAvailable.Any(y => x.Id == (int)y));
                if (!isAvailable) return BaseResponse.GetFail($"У пользователя с AccessToken={accessToken} нет доступа на данную операцию");
            }
#endif
            return new BaseResponse() { IsSuccess = true };
        }

#endregion
    }
}