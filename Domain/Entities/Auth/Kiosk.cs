using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
   public class Kiosk
    {
        public virtual long Seq_Num { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string Portal_Url { get; set; }
        public virtual string Active { get; set; }
        public virtual string Kiosk_Options { get; set; }
        public virtual long Entity_Seq_Num { get; set; }
        public virtual string Template_Seq_Num { get; set; }
        public virtual string Stripe_Public_Key { get; set; }
        public virtual string Stripe_Private_Key { get; set; }
        public virtual string Kiosk_Appt_Search { get; set; }
        public virtual string Kiosk_Url { get; set; }
        public virtual IList<UserToken> UserTokens { get; set; }
    }
}
