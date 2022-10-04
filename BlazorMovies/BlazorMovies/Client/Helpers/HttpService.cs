using System.Text;
using System.Text.Json;

namespace BlazorMovies.Client.Helpers
{
    //class allows to centralize GET/POST/PUT/DELETE operations 
    public class HttpService : IHttpService
    {
        private readonly HttpClient httpClient;

        public HttpService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        // create generic methods to use through the application
        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T data) 
        {
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, stringContent);
            return new HttpResponseWrapper<object>(null, response.IsSuccessStatusCode, response);
        }
    }
}
