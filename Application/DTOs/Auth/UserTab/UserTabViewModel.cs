using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.UserTab
{
    public class UserTabViewModel
    {
        public virtual string Role { get; set; }
        public virtual long RoleId { get; set; }
        public virtual long TabId { get; set; }
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Assignment { get; set; }
    }
}
