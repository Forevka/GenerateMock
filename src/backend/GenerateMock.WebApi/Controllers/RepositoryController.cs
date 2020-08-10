using AutoMapper;
using GenerateMock.Bll.Services;
using GenerateMock.Dal.Models.View.Out;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GenerateMock.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RepositoryController : BaseApiController
    {
        private readonly ILogger<RepositoryController> _logger;
        private readonly ExploreHubService _exploreHubService;
        private readonly IMapper _mapper;

        public RepositoryController(ILogger<RepositoryController> logger, ExploreHubService exploreHubService, IMapper mapper)
        {
            _logger = logger;
            _exploreHubService = exploreHubService;
            _mapper = mapper;
        }

        /// <summary>
        /// Registering new repository in system
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="repoName"></param>
        /// <returns></returns>

        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<Dal.Models.DB.RepositoryDb> RegisterRepository(string userName, string repoName)
        {
            return await _exploreHubService.RegisterRepository(userName, repoName, CurrentUser());
        }

        /// <summary>
        /// Get all repositories for user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<RepositoryOutViewModel>> GetUserRepository(string username)
        {
            var res = await _exploreHubService.GetUserRepositories(username);
            return _mapper.Map<List<RepositoryOutViewModel>>(res);
        }
    }
}