﻿using AutoMapper;
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
        /// <param name="label"></param>
        /// <returns></returns>
        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<Dal.Models.DB.RepositoryDb> RegisterRepository(string userName, string repoName, string label = null)
        {
            return await _exploreHubService.RegisterRepository(userName, repoName, label, CurrentUser());
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

        /// <summary>
        /// Get all repositories for logged in user
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Member")]
        [HttpGet("My")]
        public async Task<List<RepositoryOutViewModel>> GetUserRepository()
        {
            var res = await _exploreHubService.GetUserRepositories(CurrentUser());
            return _mapper.Map<List<RepositoryOutViewModel>>(res);
        }
    }
}