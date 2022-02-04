using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support.Ticket
{
    public class SupportTicketChangeHistory
    {
        public long SeqNum { get; set; }
        public string ParentTicket { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Type { get; set; }
        public string EnteredDate { get; set; }
        public string EnteredBy { get; set; }
    }
}
