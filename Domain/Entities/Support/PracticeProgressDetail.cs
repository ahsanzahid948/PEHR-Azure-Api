using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PracticeProgressDetail
    {
        public string Account_Type { get; set; }
        public string Auto_Status { get; set; }
        public string Description { get; set; }
        public long Entity_Seq_Num { get; set; }
        public DateTime Entry_Date { get; set; }
        public string Last_Comment { get; set; }
        public long Seq_Num { get; set; }
        public string Status { get; set; }
        public string Support_Message { get; set; }
        public DateTime Target_Completion_Date { get; set; }
        public string Ticket_No { get; set; }
        public string Ticket_Status { get; set; }
        public string Title { get; set; }
    }
}
