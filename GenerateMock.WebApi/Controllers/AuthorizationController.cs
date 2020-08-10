using GenerateMock.Dal.Models.View.Out;
using GenerateMock.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GenerateMock.WebApi.Controllers
{
    /// <summary>
    /// Authorization controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly AuthenticationService _authService;

        /// <summary>
        /// Authorization controller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="authService"></param>
        public AuthorizationController(ILogger<AuthorizationController> logger, AuthenticationService authService)
        {
            _logger = logger;

            _authService = authService;
        }

        /// <summary>
        /// Get authentication token
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RegistrationOutViewModel> Get(string login, string password)
        {
            var identity = await _authService.GetIdentity(login, password);

            var encodedJwt = _authService.GenerateToken(identity);

            return new RegistrationOutViewModel
            {
                access_token = encodedJwt,
                login = login,
            };
        }

    }
}