using GenerateMock.Utilities.Exceptions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GenerateMock.Bll.Services
{
    public class MockService
    {
        private readonly ExploreHubService _exploreHubService;

        public MockService(ExploreHubService exploreHubService)
        {
            _exploreHubService = exploreHubService;
        }

        public async Task<string> GetSchema(string version, string user, string repo, string db)
        {
            var repoDb = await _exploreHubService.GetDatabase(version, user, repo, db);
            if (repoDb == null)
                throw ExceptionFactory.NotFoundException(ExceptionEnum.DatabaseWithGivenParametersNotExist, $"Can't find any database for path {user}/{repo}/{version}/{db}.Please check you database path parameters.");

            return repoDb.DatabaseSchema; //NJsonSchema.JsonSchema.FromSampleJson(repoDb.DatabaseLoaded).ToJson();
        }

        public async Task<List<JToken>> GetData(string version, string user, string repo, string db, string query)
        {
            var jsonFilter = "";

            query = HttpUtility.UrlDecode(query)?.TrimStart('/');

            if (!string.IsNullOrEmpty(query))
            {
                var queryPath = query.Split('/');
                var lastPathAndQuery = queryPath.LastOrDefault();
                var last = lastPathAndQuery?.Split('?');

                jsonFilter = ParsePath(queryPath) + ParseFilter(last);
            }

            var repoDb = await _exploreHubService.GetDatabase(version, user, repo, db);

            if (repoDb == null) 
                throw ExceptionFactory.NotFoundException(ExceptionEnum.DatabaseWithGivenParametersNotExist, $"Can't find any database for path {user}/{repo}/{version}/{db}.Please check you database path parameters.");

            var o = JObject.Parse(repoDb.DatabaseLoaded);
            //posts?nested.test=123 - ok
            return o.SelectTokens(jsonFilter).ToList();

        }

        private static string ParsePath(string[] queryPath)
        {
            return "$." + string.Join('.', queryPath.ToList().Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Split('?')[0]));
        }

        private static string ParseFilter(string[] lastQuery)
        {
            Dictionary<string, StringValues> queryFilter = null;
            if (lastQuery != null)
                if (lastQuery.Length == 2)
                {
                    queryFilter = QueryHelpers.ParseNullableQuery(lastQuery[1]);
                }

            if (queryFilter == null)
                return "";

            string filterJsonPath = "[?(";

            var filterIndex = 0;
            foreach (var filter in queryFilter)
            {
                filterIndex++;
                for (var valIndex = 0; valIndex < filter.Value.Count; valIndex++)
                {
                    var value = filter.Value[valIndex];
                    if (int.TryParse(value, out var result))
                        filterJsonPath += $"@.{filter.Key}=={result}";
                    else
                        filterJsonPath += $"@.{filter.Key}=='{value}'";

                    if (valIndex != filter.Value.Count - 1)
                        filterJsonPath += " || ";
                }

                if (filterIndex != queryFilter.Count)
                    filterJsonPath += " || ";
            }


            filterJsonPath += ")]";
            return filterJsonPath;
        }
    }
}
