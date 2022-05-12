using JwtAuth.Shared;

namespace JwtAuth.Client.Services
{
    public interface IAuthenticationService
    {
        
        Task<Response<LoginResponse>> Login(LoginRequest userForAuthentication);

        Task Logout();

        //Task SendAuthenticatedRequest();
    }
}
