using System;

namespace Application.DTOs.User
{
    public class SupportUserViewModel
    {
        public virtual long UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual long EntityId { get; set; }

        public virtual string Active { get; set; }

    }
}