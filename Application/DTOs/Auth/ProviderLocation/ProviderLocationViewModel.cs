using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.ProviderLocation
{
    public class ProviderLocationViewModel
    {
        public virtual long ProviderLocationId { get; set; }
        public virtual long? AuthEntityId { get; set; }
        public virtual string UserEmail { get; set; }
        public virtual long? ProviderId { get; set; }
        public virtual long? LocationId { get; set; }
    }
}
