using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuth.Server
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWTSettings")["securityKey"])), SecurityAlgorithms.HmacSha256Signature);

            var tokenExpiry = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JWTSettings:expiryInMinutes"]));

            var jwt = new JwtSecurityToken(
                        signingCredentials: signingCredentials,
                        claims: claims,
                        notBefore: DateTime.UtcNow,
                        expires: tokenExpiry,
                        audience: "Anyone",
                        issuer: "Anyone"
                    );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
