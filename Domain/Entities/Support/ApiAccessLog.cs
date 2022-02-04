using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
   public class ApiAccessLog
    {
        public virtual long Seq_Num { get; set; }
        public virtual string Email { get; set; }
        public virtual string Data_Accessed { get; set; }
        public DateTime Accessed_Date { get; set; }
        public virtual string Ip_Address { get; set; }
        public virtual string Access_Token { get; set; }

    }
}
