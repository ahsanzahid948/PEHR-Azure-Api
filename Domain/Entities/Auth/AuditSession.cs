using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class AuditSession
    {
        public long Seq_Num { get; set; }
        public string Url { get; set; }
        public string Ip_Address { get; set; }
        public string App_User { get; set; }
        public string Db_User { get; set; }
        public DateTime? Login_Time { get; set; }
        public DateTime? Logout_Time { get; set; }
        public DateTime? Entry_Date { get; set; }
        public string Entered_By { get; set; }

    }
}
