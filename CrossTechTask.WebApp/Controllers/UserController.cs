using CrossTech.ClientApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CrossTechTask.Controllers
{
    [Authorize(Policy="Admin")]
    public class UserController : Controller
    {
        private readonly IEmployeeRemoteCallService _employeeRemoteCallService;

        public UserController(
            IEmployeeRemoteCallService employeeRemoteCallService
            )
        {
            _employeeRemoteCallService = employeeRemoteCallService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
