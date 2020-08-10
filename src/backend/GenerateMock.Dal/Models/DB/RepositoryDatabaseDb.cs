using System;
using System.IO;

namespace GenerateMock.Dal.Models.DB
{
    public class RepositoryDatabaseDb
    {
        public Guid RepositoryId { get; set; }
        public Guid DatabaseId { get; set; }
        public string DatabaseFilePath { get; set; }
        public string DatabaseLoaded { get; set; }
        public string DatabaseSchema { get; set; }
        public DateTime DatabaseLoadTime { get; set; }
        public int DatabaseVersion { get; set; }

        public virtual RepositoryDb Repository { get; set; }

        public string GetApiUrl()
        {
            return
                $"{Repository.RepositoryUsername}/{Repository.RepositoryName}/v{DatabaseVersion}/{Path.GetFileNameWithoutExtension(DatabaseFilePath)}";
        }
    }
}
