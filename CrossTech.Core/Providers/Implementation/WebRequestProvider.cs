using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CrossTech.Core.Providers.Implementation
{
    public class WebRequestProvider : IWebRequestProvider
    {
        private static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new StringEnumConverter() },
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore
        };

        public async Task<TResponse> ExecutePostAsync<TResponse, TRequest>(string url, TRequest data)
        {
            var parameters = JsonConvert.SerializeObject(data, JsonSerializerSettings);

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(10000);

                var content = new StringContent(parameters, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync(url, content);

                if (responseMessage.StatusCode == HttpStatusCode.NoContent) { return default(TResponse); }

                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var response = responseMessage;
                    var responseString = await response.Content?.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<TResponse>(responseString);
                }

                throw new Exception($"Error code: {responseMessage.StatusCode}");
            }
        }
        public async Task<TResponse> ExecuteGetAsync<TResponse>(string url)
        {
            var html = await ExecuteGetStringAsync(url);
            
            return JsonConvert.DeserializeObject<TResponse>(html, JsonSerializerSettings);
        }

        public async Task<string> ExecuteGetStringAsync(string url)
        {
            using (var wc = new WebClient())
            {
                return await wc.DownloadStringTaskAsync(new Uri(url));
            }
        }
    }
}
