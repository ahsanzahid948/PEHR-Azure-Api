using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.Ticket
{
    public class TicketCommentViewModel
    {
        public long TicketCommentId { get; set; }
        public string ParentTicket { get; set; }
        public string Comments { get; set; }
        public DateTime EnteredDate { get; set; }
        public string EnteredBy { get; set; }
        public string ViewableBy {get; set; }
        public string UserNotified { get; set; }
        public string EnteredByName { get; set; }
        public string Resolution { get; set; }
    }
}
