using GenerateMock.Bll.Services;
using GenerateMock.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GenerateMock.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExploreHubService _exploreHubService;

        public HomeController(ILogger<HomeController> logger, ExploreHubService exploreHubService)
        {
            _logger = logger;
            _exploreHubService = exploreHubService;
        }

        public async Task<IActionResult> Index()
        {
            var repositories = await _exploreHubService.GetUserRepositories("forevka");
            return View(repositories);
        }

        /*public IActionResult Privacy()
        {
            return View();
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
