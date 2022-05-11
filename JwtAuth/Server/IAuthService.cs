using System.Security.Claims;

namespace JwtAuth.Server
{
    public interface IAuthService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
    }
}
