using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.EntityUser
{
    public class EntityUserViewModel
    {
        public virtual long EntityId { get; set; }
        public virtual long? UserAccessId { get; set; }
        public virtual string Email { get; set; }
        public virtual string UserName { get; set; }
    }
}
