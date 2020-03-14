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
    public class UserController : Controller
    {
        private readonly IEmployeeRemoteCallService _employeeRemoteCallService;

        public UserController(
            IEmployeeRemoteCallService employeeRemoteCallService
            )
        {
            _employeeRemoteCallService = employeeRemoteCallService;
        }

        [Authorize(Policy ="Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
