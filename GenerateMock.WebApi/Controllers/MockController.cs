using GenerateMock.Bll.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GenerateMock.WebApi.Controllers
{
    /// <summary>
    /// Mock data controller
    /// </summary>
    [Route("")]
    [ApiController]
    public class MockController : ControllerBase
    {
        private readonly MockService _mockService;

        /// <summary>
        /// Mock data controller
        /// </summary>
        /// <param name="mockService"></param>
        public MockController(MockService mockService)
        {
            _mockService = mockService;
        }

        #pragma warning disable 1572
        /// <summary>
        /// Main url for retrieve data from mocked db
        /// </summary>
        /// <param name="version">db version</param>
        /// <param name="user">user repository</param>
        /// <param name="repo">repository name</param>
        /// <param name="db">database file</param>
        /// <returns></returns>
        #pragma warning restore 1572
        [HttpGet("{*url}")]
        public async Task<IActionResult> GetData()
        {
            var rawPath = HttpUtility.UrlDecode(HttpContext.Request.Path);

            var path = rawPath.Split('/', 6).Where(x => !string.IsNullOrEmpty(x)).ToArray();
            if (path.Length < 5) return NotFound();

            string user = path[0];
            string repo = path[1];
            string version = path[2];
            string db = path[3];
            string query = path[4];

            var data = await _mockService.GetData(version, user, repo, db, query + HttpContext.Request.QueryString);
            switch (data.Count)
            {
                case 0:
                    return Ok(new Dictionary<string, string>());
                case 1:
                    return Ok(data[0].ToString());
                default:
                    return Ok(data.Select(x => x.ToObject<Dictionary<string, object>>()));
            }
        }

        /// <summary>
        /// Get schema for given database
        /// </summary>
        /// <param name="version"></param>
        /// <param name="user"></param>
        /// <param name="repo"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{user}/{repo}/{version}/{db}")]
        public async Task<string> GetSchema(string version, string user, string repo, string db)
        {
            return await _mockService.GetSchema(version, user, repo, db);
        }
    }
}