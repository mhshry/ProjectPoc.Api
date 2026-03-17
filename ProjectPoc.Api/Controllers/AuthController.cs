using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ProjectPoc.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // NOTE: For demo only. Move secrets to secure configuration in production.
        private const string JwtKey = "super_secret_key_123!"; // same as in Program.cs
        private const string JwtIssuer = "ProjectPoc";
        private const string JwtAudience = "ProjectPocUsers";

        public record LoginRequest(string Username, string Password);

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            if (req is null)
                return BadRequest();

            // Dummy validation - replace with real user store
            if ((req.Username == "admin" || req.Username == "user") && req.Password == "password")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, req.Username),
                };

                // assign role and department claim based on username
                if (req.Username == "admin")
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    claims.Add(new Claim("department", "IT"));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, "User"));
                    claims.Add(new Claim("department", "Sales"));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: JwtIssuer,
                    audience: JwtAudience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { access_token = tokenString });
            }

            return Unauthorized();
        }
    }
}
