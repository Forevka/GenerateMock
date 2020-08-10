using System;
using System.Collections.Generic;

namespace GenerateMock.Dal.Models.DB
{
    public class RepositoryDb
    {
        public RepositoryDb()
        {
            RepositoryDatabase = new HashSet<RepositoryDatabaseDb>();
        }

        public Guid RepositoryId { get; set; }
        public string RepositoryName { get; set; }
        public Guid OwnerId { get; set; }
        public virtual UserDb UserDb { get; set; }
        public virtual ICollection<RepositoryDatabaseDb> RepositoryDatabase { get; set; }
    }
}
