using System;

namespace GenerateMock.Dal.Models.View.Out
{
    public class RepositoryDatabaseOutViewModel
    {
        public Guid RepositoryId { get; set; }
        public Guid DatabaseId { get; set; }
        public string DatabaseFilePath { get; set; }
        public DateTime DatabaseLoadTime { get; set; }
        public int DatabaseVersion { get; set; }

        public string DatabaseApiUrl { get; set; }
    }
}
