using GenerateMock.Bll.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenerateMock.WebApi.Controllers
{
    [Route("")]
    [ApiController]
    public class MockController : ControllerBase
    {
        private readonly MockService _mockService;

        public MockController(MockService mockService)
        {
            _mockService = mockService;
        }

        [HttpGet("{*url}")]
        public async Task<IActionResult> GetData()
        {
            var path = HttpContext.Request.Path.ToString().Split('/', 6).Where(x => !string.IsNullOrEmpty(x)).ToArray();
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

        [HttpGet]
        [Route("{user}/{repo}/{version}/{db}")]
        public async Task<string> GetSchema(string version, string user, string repo, string db)
        {
            return await _mockService.GetSchema(version, user, repo, db);
        }
    }
}