using CrossTech.ClientApi.Models;
using CrossTech.ClientApi.Models.Employee;
using CrossTech.ClientApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace CrossTechTask.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRemoteCallService _employeeRemoteCallService;

        public EmployeeController(
            IEmployeeRemoteCallService employeeRemoteCallService
            )
        {
            _employeeRemoteCallService = employeeRemoteCallService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("get-employees")]
        public async Task<GetEmployeesResponse> GetEmployees([FromBody] BaseRequest request)
        {
            return await _employeeRemoteCallService.Get(request);
        }
        
        [HttpPost("update-employee")]
        public async Task<BaseResponse> UpdateEmployee([FromBody] UpdateEmployeeRequest request)
        {
            return await _employeeRemoteCallService.Update(request);
        }

        [HttpPost("delete-employee")]
        public async Task<BaseResponse> DeleteEmployee([FromBody] DeleteEmployeeRequest request)
        {
            return await _employeeRemoteCallService.Delete(request);
        }

        [HttpPost("add-employee")]
        public async Task<BaseResponse> AddEmployee([FromBody] AddEmployeeRequest request)
        {
            return await _employeeRemoteCallService.Add(request);
        }

        [HttpGet("get-positions")]
        public async Task<GetPositionsResponse> GetPositions()
        {
            return await _employeeRemoteCallService.GetPositions(new BaseRequest());
        }
    }
}
