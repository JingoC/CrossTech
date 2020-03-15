using CrossTech.ClientApi.Models;
using CrossTech.ClientApi.Models.Authorization;
using CrossTech.ClientApi.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CrossTechTask.WebApp.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IAuthorizationRemoteCallService _authorizationRemoteCallService;
        public AuthorizationController(
            IAuthorizationRemoteCallService authorizationRemoteCallService
            )
        {
            _authorizationRemoteCallService = authorizationRemoteCallService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<BaseResponse> Login([FromBody] LoginRequest request)
        {
            var response = await _authorizationRemoteCallService.Login(request);

            if (response.IsSuccess)
            {
                await Authenticate(response);
            }

            return response;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Authorization");
        }

        [HttpPost]
        public async Task<GetAccessTokenResponse> GetAccessToken([FromBody] GetAccessTokenRequest request)
        {
            return await _authorizationRemoteCallService.GetAccessToken(request);
        }

        private async Task Authenticate(LoginResponse loginResponse)
        {
            var claims = loginResponse.User.Claims.Select(x => new Claim(x.Name, loginResponse.User.Login)).ToList();
            
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
