using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
    public class LoginLog
    {
        public virtual long Seq_Num { get; set; }
        public virtual string Login_User { get; set; }
        public virtual DateTime? Login_Time { get; set; }
        public virtual DateTime? Logout_Time { get; set; }
        public virtual string Browser_Type { get; set; }
        public virtual string Resolution { get; set; }
        public virtual string Download_Speed { get; set; }
        public virtual string Upload_Speed { get; set; }
        public virtual long? Auth_Entity_Seq_Num { get; set; }
        public virtual string Frontend_Version { get; set; }
        public virtual string Backend_Version { get; set; }
        public virtual string NPI { get; set; }

    }
}
