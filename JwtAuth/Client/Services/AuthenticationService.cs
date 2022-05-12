using Blazored.LocalStorage;
using JwtAuth.Client.AuthProviders;
using JwtAuth.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace JwtAuth.Client.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly IHttpService _httpService;
        public AuthenticationService(HttpClient client, 
            AuthenticationStateProvider authStateProvider, 
            ILocalStorageService localStorage,
            IHttpService httpService)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
            _httpService = httpService;
        }

        public async Task<Response<LoginResponse>> Login(LoginRequest userForAuthentication)
        {            
            var loginResponse = await _httpService.Post<LoginResponse>("api/auth/login", userForAuthentication);

            if (loginResponse.Success)
            {
                await _localStorage.SetItemAsync("authToken", loginResponse.Data.AccessToken);
                ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(loginResponse.Data.AccessToken);
            }

            return loginResponse;
        }
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }        
        //public async Task SendAuthenticatedRequest()
        //{
        //    var token = await _localStorage.GetItemAsync<string>("authToken");
        //    _client.DefaultRequestHeaders.Accept.Clear();

        //    if (!string.IsNullOrEmpty(token))
        //    {                
        //        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        //    }

        //    var authResult = await _client.GetAsync("api/values");

        //    var authContent = await authResult.Content.ReadAsStringAsync();

        //    if (!authResult.IsSuccessStatusCode)
        //    {
        //        //var error = await authResult.Content.ReadAsStringAsync();
        //        return;
        //    }
            
        //    var final = JsonSerializer.Deserialize<List<string>>(authContent);
            
        //}
    }
}
