using System;

namespace GenerateMock.Dal.Models.DB
{
    public class UserSecurity
    {
        public Guid UserId { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public Guid RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual UserDb User { get; set; }
    }
}
