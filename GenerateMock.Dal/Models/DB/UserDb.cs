using System;
using System.Collections.Generic;

namespace GenerateMock.Dal.Models.DB
{
    public class UserDb
    {
        public UserDb()
        {
            Repositories = new HashSet<RepositoryDb>();
        }

        public Guid UserId { get; set; }
        public string Username { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<RepositoryDb> Repositories { get; set; }
    }
}
