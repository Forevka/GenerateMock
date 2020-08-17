using GenerateMock.Dal.Context;
using GenerateMock.Dal.Models.DB;
using GenerateMock.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GenerateMock.Bll.Services
{
    public class ExploreHubService
    {
        private readonly PublicContext _publicContext;
        private readonly UserService _userService;
        private readonly IGitHubClient _gitHubClient;

        public ExploreHubService(PublicContext publicContext, UserService userService, IGitHubClient gitHubClient)
        {
            _publicContext = publicContext;
            _userService = userService;
            _gitHubClient = gitHubClient;
        }

        public async Task<List<RepositoryDb>> GetUserRepositories(string username)
        {
            return await _publicContext.Repository.Include(x => x.RepositoryDatabase).Where(x => x.RepositoryUsername == username)
                .ToListAsync();
        }

        public async Task<RepositoryDb> RegisterRepository(string userName, string repositoryName, string repositoryLabel, Guid userId)
        {

            //var user = await _userService.AddUserIfNotExist(userName);

            var repo = await CreateRepositoryIfNotExist(repositoryName, userName, repositoryLabel, userId);

            return repo;
        }

        public async Task<RepositoryDatabaseDb> LoadRepositoryDatabase(Guid repoId, string dbFilePath, string dbLabel = null)
        {
            var repo = await GetRepo(repoId);

            if (!dbFilePath.EndsWith(".json"))
                throw ExceptionFactory.SoftException(ExceptionEnum.FileTypeNotValid, "Sorry, only .json files");

            string repoDbJson;
            string schema;

            try
            {
                var repoDbContent = await _gitHubClient.Repository.Content.GetRawContent(repo.RepositoryUsername, repo.RepositoryName,
                    dbFilePath);
                repoDbJson = Encoding.UTF8.GetString(repoDbContent);

                if (!IsValidJson(repoDbJson))
                    throw ExceptionFactory.SoftException(ExceptionEnum.FileContentNotValid, "Content of provided file not valid.");

                schema = NJsonSchema.JsonSchema.FromSampleJson(repoDbJson).ToJson();
            }
            catch (Octokit.NotFoundException)
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.FileAtGivenPathDoesntExist,
                    $"File {dbFilePath} doesn't exist in repo {repo.RepositoryName}");
            }

            var dbFileVersion = repo.RepositoryDatabase.GroupBy(x => x.DatabaseFilePath).Select(x => new
            {
                x.Key,
                Count = x.Count()
            }).FirstOrDefault(x => x.Key == dbFilePath);

            var repoDb = new RepositoryDatabaseDb
            {
                DatabaseFilePath = dbFilePath,
                DatabaseId = Guid.NewGuid(),
                DatabaseLoaded = repoDbJson,
                DatabaseSchema = schema,
                DatabaseLoadTime = DateTime.UtcNow,
                RepositoryId = repoId,
                DatabaseVersion = dbFileVersion?.Count + 1 ?? 1,
                DatabaseLabel = dbLabel,
            };

            await _publicContext.RepositoryDatabase.AddAsync(repoDb);
            await _publicContext.SaveChangesAsync();

            return repoDb;
        }

        public async Task<RepositoryDb> GetRepo(Guid repoId)
        {
            var repo = await _publicContext.Repository
                .Where(x => x.RepositoryId == repoId)
                .Include(x => x.UserDb)
                .Include(x => x.RepositoryDatabase)
                .FirstOrDefaultAsync();

            if (repo == null)
                throw ExceptionFactory.SoftException(ExceptionEnum.RepositoryDoesntExist, $"Repository with id {repoId} doesn't exist");

            return repo;
        }

        public async Task<List<RepositoryDatabaseDb>> GetDatabase(Guid repoId, int version = -1, string dbName = null)
        {
            var repo = await GetRepo(repoId);

            if (string.IsNullOrEmpty(dbName) && version == -1)
                throw ExceptionFactory.SoftException(ExceptionEnum.VersionOrDatabaseNameMustBeProvided,
                    "If you don't provide version you must specify database name. Otherwise if you don't provide database name you must provide version");

            if (string.IsNullOrEmpty(dbName))
                return repo.RepositoryDatabase.Where(x => x.DatabaseVersion == version).ToList();

            if (version != -1)
                return repo.RepositoryDatabase.Where(x => x.DatabaseVersion == version && x.DatabaseFilePath == dbName).ToList();

            return new List<RepositoryDatabaseDb>
            {
                await _publicContext.RepositoryDatabase.OrderByDescending(x => x.DatabaseVersion).FirstOrDefaultAsync(x => x.DatabaseFilePath == dbName)
            };
        }

        public async Task<List<RepositoryDatabaseDb>> AvailableDatabase(Guid repoId)
        {
            return await _publicContext.RepositoryDatabase
                .Include(x => x.Repository)
                    .ThenInclude(x => x.UserDb)
                .Where(x => x.RepositoryId == repoId)
                .ToListAsync();
        }

        public async Task<RepositoryDatabaseDb> GetDatabase(string version, string user, string repo, string db)
        {
            var preparedVersion = version.Replace("v", "");
            var isVersionValid = int.TryParse(preparedVersion, out var parsedVersion);
            if (!isVersionValid)
                throw ExceptionFactory.SoftException(ExceptionEnum.GivenVersionNotValid,
                    $"Version {version} is not valid. Try something like: v1, v2, v8, v9");

            var repoDb = await _publicContext.Repository
                .Include(x => x.RepositoryDatabase)
                .FirstOrDefaultAsync(x =>
                    x.RepositoryName == repo 
                    && x.RepositoryUsername == user);

            return repoDb?.RepositoryDatabase.FirstOrDefault(x => x.DatabaseVersion == parsedVersion && x.DatabaseFilePath == db + ".json");
        }

        public async Task<RepositoryDb> CreateRepositoryIfNotExist(string repoName, string userName, string repositoryLabel, Guid userId)
        {
            var repo = await _publicContext.Repository.FirstOrDefaultAsync(x => x.RepositoryName == repoName && x.OwnerId == userId);

            if (repo != null) return repo;

            repo = new RepositoryDb
            {
                RepositoryId = Guid.NewGuid(),
                RepositoryName = repoName,
                RepositoryUsername = userName,
                RepositoryLabel = repositoryLabel,
                OwnerId = userId,
            };

            await _publicContext.Repository.AddAsync(repo);
            await _publicContext.SaveChangesAsync();

            return repo;
        }

        private static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((!strInput.StartsWith("{") || !strInput.EndsWith("}")) &&
                (!strInput.StartsWith("[") || !strInput.EndsWith("]"))) return false;

            try
            {
                JToken.Parse(strInput);
                return true;
            }
            catch (JsonReaderException)
            {
                //Console.WriteLine(jex.Message);
                return false;
            }
            catch (Exception) //some other exception
            {
                //Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
