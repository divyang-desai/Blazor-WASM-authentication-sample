using JwtAuth.Shared;

namespace JwtAuth.Client.Services
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> Login(LoginRequest userForAuthentication);

        Task Logout();
    }
}
