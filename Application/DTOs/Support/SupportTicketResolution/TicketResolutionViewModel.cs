using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.Ticket
{
    public class TicketResolutionViewModel
    {
        public long TicketResolutionId { get; set; }
        public long TicketNo { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public DateTime EntryDate { get; set; }
        public string EnteredBy { get; set; }
    }
}
