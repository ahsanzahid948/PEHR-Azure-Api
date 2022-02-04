using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.Ticket
{
    public class TicketHistoryViewModel
    {
        public long TicketHistoryId { get; set; }
        public string ParentTicket { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Type { get; set; }
        public DateTime EnteredDate { get; set; }
        public string EnteredBy { get; set; }

    }
}
