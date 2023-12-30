using CMS.Core.Services;
using CMS.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public IAuthenticationService _authenticationService { get; }
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<AuthModel> Register(RegisterModel registerModel)
        {
            AuthModel authModel = new AuthModel();
            authModel = await _authenticationService.RegisterUserAsync(registerModel);
            return authModel;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<AuthModel> Login(LoginModel loginModel)
        {
            AuthModel authModel = new AuthModel();
            authModel = await _authenticationService.LoginUserAsync(loginModel);

            return authModel;
        }
    }
}
