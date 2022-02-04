using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.UserProviderAgent
{
    public class UserProviderAgentViewModel
    {
        public virtual long? UserAgentId { get; set; }
        public virtual long? ProviderId { get; set; }
    }
}
