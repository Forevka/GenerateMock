using System;
using System.Collections.Generic;
using System.Text;

namespace GenerateMock.Dal.Models.View.Out
{
    public class UserSecurityOutViewModel
    {
        public string Login { get; set; }
        public Guid RoleId { get; set; }
        public virtual RoleOutViewModel Role { get; set; }
    }
}
