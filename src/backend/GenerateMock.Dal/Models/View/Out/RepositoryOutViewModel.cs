using System;
using System.Collections.Generic;

namespace GenerateMock.Dal.Models.View.Out
{
    public class RepositoryOutViewModel
    {
        public Guid RepositoryId { get; set; }
        public string RepositoryName { get; set; }
        public Guid OwnerId { get; set; }
        public virtual List<RepositoryDatabaseOutViewModel> RepositoryDatabase { get; set; }
    }
}
