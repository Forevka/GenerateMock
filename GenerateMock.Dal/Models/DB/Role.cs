using System;
using System.Collections.Generic;

namespace GenerateMock.Dal.Models.DB
{
    public class Role
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        public virtual ICollection<UserSecurity> UserSecurities { get; set; }
    }
}
