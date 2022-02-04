using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class SupportInbox
    {
        public long Seq_Num { get; set; }
        public string View_Status { get; set; }
        public string Ticket_No { get; set; }
        public string Email_Text { get; set; }
        public string Description { get; set; }
        public long User_Pref_Seq_Num { get; set; }
        public long User_Seq_Num { get; set; }
        public string Type { get; set; }
        public string Notification_Type { get; set; }
        public DateTime Entered_Date { get; set; }
        public byte[] Email_Text_Blob { get; set; }
        public string Email_Blob_Text_Js { get; set; }
    }
}
