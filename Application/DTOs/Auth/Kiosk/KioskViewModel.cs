using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.KioskAdmin
{
   public class KioskViewModel
    {
        public virtual string SeqNum { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string PortalUrl { get; set; }
        public virtual string Active { get; set; }
        public virtual string KioskOptions { get; set; }
        public virtual string EntityId { get; set; }
        public virtual string TemplateSeqNum { get; set; }
        public virtual string StripePublicKey { get; set; }
        public virtual string StripePrivateKey { get; set; }
        public virtual string KioskApptSearch { get; set; }
        public virtual string KioskUrl { get; set; }
    }
}
