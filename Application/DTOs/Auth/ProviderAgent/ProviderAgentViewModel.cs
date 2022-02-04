using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.ProviderAgent
{
    public class ProviderAgentViewModel
    {
        public virtual long ProviderAgentId { get; set; }
        public virtual long? UserId { get; set; }

    }
}
