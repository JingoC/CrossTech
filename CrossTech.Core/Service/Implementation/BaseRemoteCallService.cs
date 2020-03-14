using CrossTech.Core.Providers;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace CrossTech.Core.Service.Implementation
{
    public abstract class BaseRemoteCallService : IBaseRemoteCallService
    {
        protected abstract string _apiSchemeAndHostConfigKey { get; set; }
        private readonly IConfiguration _configuration;
        private readonly IWebRequestProvider _requestService;

        public BaseRemoteCallService(IConfiguration configuration, IWebRequestProvider requestService)
        {
            _configuration = configuration;
            _requestService = requestService;
    }

        protected string GetUrl(string path)
        {
            if (string.IsNullOrEmpty(_apiSchemeAndHostConfigKey) == true)
                throw new InvalidOperationException("ApiSchemeAndHostConfigKey is empty");

            var schemeAndHost = _configuration[_apiSchemeAndHostConfigKey];

            if (string.IsNullOrEmpty(schemeAndHost))
                throw new InvalidOperationException($"schemeAndHost is empty by key = {_apiSchemeAndHostConfigKey}");

            var baseUri = new UriBuilder(new Uri(new Uri(schemeAndHost), path));

            return new UriBuilder(scheme: baseUri.Scheme,
                                                host: baseUri.Host,
                                                port: baseUri.Port,
                                                path: baseUri.Path,
                                                extraValue: baseUri.Query).Uri.ToString();
        }

        public async Task<TResponse> ExecutePostAsync<TResponse, TRequest>(string path, TRequest request) =>
            await _requestService.ExecutePostAsync<TResponse, TRequest>(GetUrl(path), request);
        
        public async Task<TResponse> ExecuteGetAsync<TResponse>(string path) =>
            await _requestService.ExecuteGetAsync<TResponse>(GetUrl(path));

        public async Task<string> ExecuteGetStringAsync(string path) =>
            await _requestService.ExecuteGetStringAsync(GetUrl(path));
    }
}
