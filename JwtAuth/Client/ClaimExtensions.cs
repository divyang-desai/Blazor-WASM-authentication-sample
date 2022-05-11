using System.Security.Claims;
using JwtAuth.Shared;

namespace JwtAuth.Client
{
    public static class ClaimExtensions
    {
        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
                return null;

            var currentUser = user;
            var value = currentUser.FindFirst(c => c.Type == ApiConstants.JwtRegisteredClaimNamesUserEmail).Value;

            return value;
        }
    }
}
