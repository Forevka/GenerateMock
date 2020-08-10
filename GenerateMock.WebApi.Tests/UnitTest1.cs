using GenerateMock.Bll.Services;
using GenerateMock.Dal.Context;
using GenerateMock.Utilities.Exceptions;
using GenerateMock.WebApi.Controllers;
using NUnit.Framework;
using Octokit;

namespace GenerateMock.WebApi.Tests
{
    public class Tests
    {
        private readonly MockController _mockController;
        private readonly UserService _userService;
        private readonly PublicContext _publicContext;
        private readonly MockService _mockService;
        private readonly ExploreHubService _exploreHubService;
        public Tests()
        {
            _publicContext = new PublicContext("Host=194.99.21.140;Database=GenerateMock;Username=postgres;Password=werdwerd2012");
            _userService = new UserService(_publicContext);

            _exploreHubService = new ExploreHubService(_publicContext, _userService, new GitHubClient(new ProductHeaderValue("MyApp")));
            _mockService = new MockService(_exploreHubService);
            _mockController = new MockController(_mockService);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_ThrowExceptionWhenNotFound()
        {
            Assert.That(() => { _mockService.GetData("v1", "forevka", "demo", "db1", "").GetAwaiter().GetResult(); }, Throws.Exception);
        }

        [Test]
        public void Test2()
        {
            Assert.That(() => { _mockService.GetData("v1", "forevka", "demo", "db", "posts").GetAwaiter().GetResult(); }, Throws.Nothing);
        }
    }
}