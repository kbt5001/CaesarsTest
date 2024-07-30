using CaesarsTest.API.Models;
using CaesarsTest.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaesarsTest.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequest authenticationRequest)
        {
            var user = _authService.ValidateUserCredentials(authenticationRequest.UserName,
                authenticationRequest.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(_authService.GetToken(user));
        }
    }
}
