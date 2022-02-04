using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserPreferences
    {
        public virtual long Seq_Num { get; set; }
        public virtual long User_Seq_Num { get; set; }
        public virtual string Type { get; set; }
        public virtual string Notification_Type { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual DateTime Entered_Date { get; set; }
    }
}
