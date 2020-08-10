using System;
using GenerateMock.Utilities.Exceptions;
using GenerateMock.Utilities.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            query = query.TrimStart('/');

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

            try
            {
                var o = JObject.Parse(repoDb.DatabaseLoaded);
                return o.SelectTokens(jsonFilter).ToList();
            }
            catch (IndexOutOfRangeException)
            {
                throw ExceptionFactory.SoftException(ExceptionEnum.ThisFeatureNotImplemented,
                    "Indexing with negative numbers don't implemented yet.");
            }
        }

        private static string ParsePath(IEnumerable<string> queryPath)
        {
            var pathBuilder = new StringBuilder("$");

            foreach (var expr in queryPath.ToList().Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Split('?')[0]))
            {
                pathBuilder.Append(int.TryParse(expr, out var intExpr) ? $"[{intExpr}]" : $".{expr}");
            }

            return pathBuilder.ToString(); //"$." + string.Join('.', ));
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

            var filterJsonPath = new StringBuilder();

            queryFilter.Each((filter, filterIndex) =>
            {
                for (var valIndex = 0; valIndex < filter.Value.Count; valIndex++)
                {
                    var value = filter.Value[valIndex];
                    filterJsonPath.Append(int.TryParse(value, out var result)
                        ? $"@.{filter.Key}=={result}"
                        : $"@.{filter.Key}=='{value}'");

                    if (valIndex != filter.Value.Count - 1)
                        filterJsonPath.Append(" || ");
                }

                if (filterIndex != queryFilter.Count - 1)
                    filterJsonPath.Append(" || ");
            });

            return $"[?({filterJsonPath})]";
        }
    }
}
