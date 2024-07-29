using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CaesarsTest.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;

        public class AuthenticationRequest
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequest authenticationRequest)
        {
            var user = ValidateUserCredentials(authenticationRequest.UserName,
                authenticationRequest.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var tokenClaims = new List<Claim>();
            tokenClaims.Add(new Claim("sub", user.UserId.ToString()));
            tokenClaims.Add(new Claim("given_name", user.FirstName));
            tokenClaims.Add(new Claim("family_name", user.LastName));
            tokenClaims.Add(new Claim("role", user.Role));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                tokenClaims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
               .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private User ValidateUserCredentials(string? username, string? password)
        {
            return new User(1, username ?? "", "Kirk", "Thomas", "admin");
        }

        private class User
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Role { get; set; }

            public User(int userId, string userName, string firstName, string lastName, string role)
            {
                UserId = userId;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;
                Role = role;
            }
        }
    }
}
