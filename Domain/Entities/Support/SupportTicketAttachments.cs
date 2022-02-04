using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Notes
{
    public class SupportTicketAttachments
    {
        public long Seq_Num { get; set; }
        public string Ticket_Number { get; set; }
        public long Comment_Seq_Num { get; set; }
        public string File_Path { get; set; }
        public string File_Name { get; set; }
        public DateTime Entry_Date { get; set; }
        public string Entered_By { get; set; }

    }
}
