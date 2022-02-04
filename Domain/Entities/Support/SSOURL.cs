using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
    public class SSOURL
    {
        public virtual long Seq_Num { get; set; }
        public virtual string SSO_Name { get; set; }
        public virtual string Validation_Url { get; set; }
        public virtual string Post_Url { get; set; }
        public virtual long? Entity_Seq_Num { get; set; }
        public virtual string Encryption_Key { get; set; }

    }
}
