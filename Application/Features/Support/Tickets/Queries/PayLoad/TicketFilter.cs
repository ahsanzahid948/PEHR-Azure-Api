using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Ticket
{
    public class TicketFilter : PayLoad
    {
        public string SearchKeyWords { get; set; }
        public string ParentTicket { get; set; }
        public string Status { get; set; }
        public bool UnResolvedFlag { get; set; }
        public string Assignee { get; set; }
        public string PracticeName { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
        public string Suspended { get; set; }
        public string Client { get; set; }
        public string EntitySeqNum { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Team { get; set; }
        public string InternalAssignee { get; set; }

    }
}
