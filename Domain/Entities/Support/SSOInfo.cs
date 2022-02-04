using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
    public class SSOInfo
    {
        public virtual long Seq_Num { get; set; }
        public virtual string SSO_User { get; set; }
        public virtual string SSO_Password { get; set; }
        public virtual string Pehr_User_Email { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual DateTime? Entry_Date { get; set; }
        public virtual string SSO_Type { get; set; }
        public virtual long? Entity_Seq_Num { get; set; }

    }
}
