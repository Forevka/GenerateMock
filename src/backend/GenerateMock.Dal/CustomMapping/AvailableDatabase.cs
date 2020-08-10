using GenerateMock.Dal.Models.DB;
using GenerateMock.Dal.Models.View.Out;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenerateMock.Dal.CustomMapping
{
    public static class AvailableDatabase
    {
        public static List<AvailableDatabaseForRepositoryOutViewModel> Map(this List<RepositoryDatabaseDb> models, Guid repoId)
        {
            var groupedAvailableDatabases = models.GroupBy(x => x.DatabaseFilePath).Select(xx => new
            {
                xx.Key,
                LastVersion = xx.First(y => y.DatabaseVersion == xx
                                                .OrderByDescending(x => x.DatabaseVersion)
                                                .Select(x => x.DatabaseVersion)
                                                .FirstOrDefault()).DatabaseVersion,
                xx.First(y => y.DatabaseVersion == xx
                                  .OrderByDescending(x => x.DatabaseVersion)
                                  .Select(x => x.DatabaseVersion)
                                  .FirstOrDefault()).Repository.RepositoryUsername,
                xx.First(y => y.DatabaseVersion == xx
                                  .OrderByDescending(x => x.DatabaseVersion)
                                  .Select(x => x.DatabaseVersion)
                                  .FirstOrDefault()).Repository.RepositoryName,
                xx.First(y => y.DatabaseVersion == xx
                                  .OrderByDescending(x => x.DatabaseVersion)
                                  .Select(x => x.DatabaseVersion)
                                  .FirstOrDefault()).DatabaseLoadTime,
                ApiUrl = xx.First(y => y.DatabaseVersion == xx
                                  .OrderByDescending(x => x.DatabaseVersion)
                                  .Select(x => x.DatabaseVersion)
                                  .FirstOrDefault()).GetApiUrl()
            }).ToList();

            return groupedAvailableDatabases.Select(x => new AvailableDatabaseForRepositoryOutViewModel()
            {
                RepositoryId = repoId,
                LastVersion = x.LastVersion,
                DatabasePath = x.ApiUrl,
                LastUpdated = x.DatabaseLoadTime,

            }).ToList();
        }
    }
}
