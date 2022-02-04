using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.MenuRole
{
    public class MenuRoleViewModel
    {
        public virtual long RoleId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Comments { get; set; }
        public virtual long? EntityId { get; set; }
        public virtual string DefaultEnable { get; set; }
    }
}
