using JwtAuth.Client.Services;
using JwtAuth.Shared;
using Microsoft.AspNetCore.Components;

namespace JwtAuth.Client.Pages
{
    public partial class Login
    {
        private LoginRequest _userForAuthentication = new LoginRequest();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowAuthError { get; set; }
        public string Error { get; set; }
        public async Task ExecuteLogin()
        {
            ShowAuthError = false;
            var result = await AuthenticationService.Login(_userForAuthentication);
            if (string.IsNullOrEmpty(result.AccessToken))
            {
                Error = "This is some error";
                ShowAuthError = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
