using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support.Ticket
{
    public class SupportTicketComments
    {
        public long Seq_Num { get; set; }
        public string Parent_Ticket { get; set; }
        public string Comments { get; set; }
        public DateTime Entered_Date { get; set; }
        public string Entered_By { get; set; }
        public string Viewable_By { get; set; }
        public string User_Notified { get; set; }
        public string Entered_By_Name { get; set; }
        public string Resolution { get; set; }
    }
}
