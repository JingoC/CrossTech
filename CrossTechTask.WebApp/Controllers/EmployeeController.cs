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
            try
            {
                var response = await _employeeRemoteCallService.Get(request);
                return response;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        
        [HttpPost("update-employee")]
        public async Task<BaseResponse> UpdateEmployee([FromBody] UpdateEmployeeRequest request)
        {

            try
            {
                var response = await _employeeRemoteCallService.Update(request);
                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost("delete-employee")]
        public async Task<BaseResponse> DeleteEmployee([FromBody] DeleteEmployeeRequest request)
        {
            try
            {
                return await _employeeRemoteCallService.Delete(request);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost("add-employee")]
        public async Task<BaseResponse> AddEmployee([FromBody] AddEmployeeRequest request)
        {
            try
            {
                return await _employeeRemoteCallService.Add(request);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet("get-positions")]
        public async Task<GetPositionsResponse> GetPositions()
        {
            try
            {
                return await _employeeRemoteCallService.GetPositions(new BaseRequest());
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        
    }
}
