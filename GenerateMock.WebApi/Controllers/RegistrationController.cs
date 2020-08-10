using AutoMapper;
using GenerateMock.Dal.Models.DB;
using GenerateMock.Dal.Models.View.Out;
using GenerateMock.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GenerateMock.WebApi.Controllers
{
    /// <summary>
    /// Registration controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly ILogger<RegistrationController> _logger;
        private readonly IMapper _mapper;

        private readonly RegistrationService _registrationService;
        private readonly AuthenticationService _authenticationService;

        /// <summary>
        /// Registration controller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="registrationService"></param>
        /// <param name="authenticationService"></param>
        public RegistrationController(ILogger<RegistrationController> logger, IMapper mapper, RegistrationService registrationService, AuthenticationService authenticationService)
        {
            _logger = logger;
            _mapper = mapper;

            _registrationService = registrationService;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Register new member
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RegistrationOutViewModel> Post(string login, string password)
        {
            var userModel = await _registrationService.Register(login, password);

            var token = _authenticationService.GenerateToken(await _authenticationService.GetIdentity(login, password));

            return new RegistrationOutViewModel
            {
                access_token = token,
                login = login,
            };
        }
    }
}