using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.Ticket
{
    public class SupportTicketFlagsViewModel
    {
        public int AssigneeFlag { get; set; }
        public int InternalAssigneeFlag { get; set; }
        public int TeamAssigneeChangeFlag { get; set; }
        public int ProductTypeChangeFlag { get; set; }
        public int TicketSourceChangeFlag { get; set; }
        public int TargetChangeFlag { get; set; }
        public int TaskTypeChangeFlag { get; set; }
        public int CategoryChangeFlag { get; set; }
        public int StatusChangeFlag { get; set; }
        public int TickleDateChangeFlag { get; set; }
        public int ChangePriorityFlag { get; set; }
    }
}
