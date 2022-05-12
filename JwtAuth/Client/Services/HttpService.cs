using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace JwtAuth.Client.Services
{
    public class HttpService : IHttpService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _client;
        public HttpService(ILocalStorageService localStorage, HttpClient client)
        {
            _localStorage = localStorage;
            _client = client;
        }

        public async Task<Response<T>> Get<T>(string uri)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }

            var response = await _client.GetAsync(uri);

            var stringContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new Response<T>() { Success = false, Message = stringContent };
            }

            var data = JsonSerializer.Deserialize<T>(stringContent,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return new Response<T>() { Success = true, Data = data, Message = stringContent };
        }

        public async Task<Response<T>> Post<T>(string uri, object value)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }

            var bodyContent = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(uri, bodyContent);

            var stringContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new Response<T>() { Success = false, Message = stringContent };
            }

            var data = JsonSerializer.Deserialize<T>(stringContent,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            return new Response<T>() { Success = true, Data = data };

        }
    }
}
