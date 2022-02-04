using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.MultiProvider
{
    public class MultiProviderViewModel
    {
        public virtual long MultiProviderId { get; set; }
        public virtual long? ProviderId { get; set; }
        public virtual long? UserId { get; set; }
        public virtual long? AuthEntityId { get; set; }
    }
}
