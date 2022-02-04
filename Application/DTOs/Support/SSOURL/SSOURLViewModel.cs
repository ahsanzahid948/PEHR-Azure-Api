using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.SSOURL
{
    public class SSOURLViewModel
    {
        public virtual long ServerId { get; set; }
        public virtual string SSOName { get; set; }
        public virtual string ValidationUrl { get; set; }
        public virtual string PostUrl { get; set; }
        public virtual long? EntityId { get; set; }
        public virtual string EncryptionKey { get; set; }

    }
}
