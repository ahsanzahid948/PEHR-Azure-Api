using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Authentication
{
   public class KioskAuthenticatonResponse
    {
        public long UserId { get; set; }
        public virtual string Email { get; set; }
        public virtual long EntityId { get; set; }
        public virtual string PortalUrl { get; set; }
        public virtual string Active { get; set; }
        public virtual string KioskOptions { get; set; }
        public virtual string TemplateSeqNum { get; set; }
        public virtual string StripePublicKey { get; set; }
        public virtual string StripePrivateKey { get; set; }
        public virtual string KioskApptSearch { get; set; }
        public virtual string KioskUrl { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
