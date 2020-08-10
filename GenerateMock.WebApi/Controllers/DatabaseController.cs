using AutoMapper;
using GenerateMock.Bll.Services;
using GenerateMock.Dal.Models.View.Out;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GenerateMock.Dal.CustomMapping;

namespace GenerateMock.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly ILogger<DatabaseController> _logger;
        private readonly ExploreHubService _exploreHubService;
        private readonly IMapper _mapper;

        public DatabaseController(ILogger<DatabaseController> logger, ExploreHubService exploreHubService, IMapper mapper)
        {
            _logger = logger;
            _exploreHubService = exploreHubService;
            _mapper = mapper;
        }

        /// <summary>
        /// Registering new database file with given path and attaching it to given repository id
        /// </summary>
        /// <param name="repoId"></param>
        /// <param name="dbFilePath"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<RepositoryDatabaseOutViewModel> LoadDatabase(Guid repoId, string dbFilePath)
        {
            var repoDb = await _exploreHubService.LoadRepositoryDatabase(repoId, dbFilePath);
            return _mapper.Map<RepositoryDatabaseOutViewModel>(repoDb);
        }

        /// <summary>
        /// Get database 
        /// </summary>
        /// <param name="repoId">Repository id</param>
        /// <param name="version">Database version. If -1 then would return latest</param>
        /// <param name="dbName">Database name</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{repoId}")]
        public async Task<List<RepositoryDatabaseOutViewModel>> GetDatabase([FromRoute]Guid repoId, [FromQuery]int version = -1, [FromQuery]string dbName = null)
        {
            var repoDb = await _exploreHubService.GetDatabase(repoId, version, dbName);
            return _mapper.Map<List<RepositoryDatabaseOutViewModel>>(repoDb);
        }

        /// <summary>
        /// Get available database for repo
        /// </summary>
        /// <param name="repoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{repoId}/Available")]
        public async Task<List<AvailableDatabaseForRepositoryOutViewModel>> GetAvailableDatabase([FromRoute]Guid repoId)
        {
            var availableDatabases = await _exploreHubService.AvailableDatabase(repoId);

            return availableDatabases.Map(repoId);
        }
    }
}