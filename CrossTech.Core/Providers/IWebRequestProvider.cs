using System.Threading.Tasks;

namespace CrossTech.Core.Providers
{
    public interface IWebRequestProvider
    {
        Task<TResponse> ExecutePostAsync<TResponse, TRequest>(string url, TRequest data);
        Task<TResponse> ExecuteGetAsync<TResponse>(string url);
        Task<string> ExecuteGetStringAsync(string url);
    }
}
