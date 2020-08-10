using System;
using System.Collections.Generic;
using System.Text;

namespace GenerateMock.Dal.Models.View.Out
{
    public class AvailableDatabaseForRepositoryOutViewModel
    {
        public Guid RepositoryId { get; set; }
        public string DatabasePath { get; set; }
        public int LastVersion { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
