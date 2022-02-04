using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.Ticket
{
    public class TicketStatusViewModel
    {
        public string Status { get; set; }
        public string StatusName { get; set; }
        public string AssigneeShortName { get; set; }
        public string TicketType { get; set; }
        public string StatusCount { get; set; }
    }
}
