using GenerateMock.Dal.Models.View.Out;
using GenerateMock.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace GenerateMock.WebApi.Controllers
{
    /// <summary>
    /// Authorization controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : BaseApiController
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly AuthenticationService _authService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Authorization controller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="authService"></param>
        /// <param name="mapper"></param>
        public AuthorizationController(ILogger<AuthorizationController> logger, AuthenticationService authService, IMapper mapper)
        {
            _logger = logger;

            _authService = authService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get authentication token
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<RegistrationOutViewModel> GetToken(string login, string password)
        {
            var identity = await _authService.GetIdentity(login, password);

            var encodedJwt = _authService.GenerateToken(identity);

            return new RegistrationOutViewModel
            {
                access_token = encodedJwt,
                login = login,
            };
        }

        /// <summary>
        /// Get current authenticated user
        /// </summary>
        /// <returns></returns>
        [HttpGet("Me")]
        [Authorize(Roles = "Member")]
        public async Task<UserOutViewModel> Get()
        {
            return _mapper.Map<UserOutViewModel>(await _authService.GetUser(CurrentUser()));
        }
    }
}