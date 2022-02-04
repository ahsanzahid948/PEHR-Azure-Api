using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.UserMenu
{
    public class UserMenuViewModel
    {
        public virtual string Role { get; set; }
        public virtual long RoleId { get; set; }
        public virtual long MenuId { get; set; }
        public virtual long? TabId { get; set; }
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Assignment { get; set; }
        public virtual string Tab { get; set; }
        public virtual string CategoryType { get; set; }
    }
}
