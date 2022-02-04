using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.UserEntities
{
    public class UserEntitiesViewModel
    {
        public virtual long UserEntityId { get; set; }
        public virtual long EntityId { get; set; }
        public virtual string Description { get; set; }
        public virtual string EntityShortName { get; set; }
        public virtual string Email { get; set; }
        public virtual long AppUserId { get; set; }
        public virtual string UserName { get; set; }
    }
}
