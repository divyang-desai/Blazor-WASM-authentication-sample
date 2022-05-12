using JwtAuth.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JwtAuth.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = new User
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                Email = loginRequest.Email,
                Role = Roles.User.ToString()
            };
            var usersClaims = new[]
            {
                    new Claim(ApiConstants.JwtRegisteredClaimNamesUserId,  user.Id.ToString()),
                    new Claim(ApiConstants.JwtRegisteredClaimNamesUserEmail,  user.Email),
                    new Claim(ClaimTypes.Role,  user.Role)
            };

            //Add role

            var jwtToken = _authService.GenerateAccessToken(usersClaims);

            return Ok(new LoginResponse { UserId = user.Id, AccessToken = jwtToken });
        }
    }
}
