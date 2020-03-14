using CrossTech.ClientApi.Models.Authorization;
using CrossTech.Core.Providers;
using CrossTech.Core.Service.Implementation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrossTech.ClientApi.Service.Implementation
{
    public class AuthorizationRemoteCallService : BaseRemoteCallService, IAuthorizationRemoteCallService
    {
        protected override string _apiSchemeAndHostConfigKey { get; set; } = "Host.CrossTech.WebApi";

        public AuthorizationRemoteCallService(
            IConfiguration configuration,
            IWebRequestProvider webRequestProvider
            ) : base(configuration, webRequestProvider)
        {

        }

        public async Task<LoginResponse> Login(LoginRequest request)
            => await ExecutePostAsync<LoginResponse, LoginRequest>("/authorization/login", request);
        public async Task<GetAccessTokenResponse> GetAccessToken(GetAccessTokenRequest request)
            => await ExecutePostAsync<GetAccessTokenResponse, GetAccessTokenRequest>("/authorization/get-access-token", request);
    }
}
