using CaesarsTest.API.Entities;

namespace CaesarsTest.API.Services
{
    public interface IAuthService
    {
        public User ValidateUserCredentials(string username, string password);
        public string GetToken(User user);
    }
}
