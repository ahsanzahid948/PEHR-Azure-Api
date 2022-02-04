using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support.Ticket
{
    public class SupportTicketResolution
    {
        public long SeqNum { get; set; }
        public long TicketNo { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public DateTime EntryDate { get; set; }
        public string EnteredBy { get; set; }
    }
}
