using CaesarsTest.API.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CaesarsTest.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        
        public User ValidateUserCredentials(string username, string password)
        {
            if (username == "admin" && password == "admin")
                return new User(1, username, "admin", "admin", "admin");
            else if (username == "kthomas" &&  password == "kthomas")
                return new User(2, username, "Kirk", "Thomas", "user");

            return null;
        }

        public string GetToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var tokenClaims = new List<Claim>
            {
                new Claim("sub", user.UserId.ToString()),
                new Claim("given_name", user.FirstName),
                new Claim("family_name", user.LastName),
                new Claim("role", user.Role)
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                tokenClaims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        
    }
}
