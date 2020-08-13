using System;

namespace GenerateMock.Dal.Models.View.Out
{
    public class UserOutViewModel
    {
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public UserSecurityOutViewModel UserSecurity { get; set; }
    }
}
