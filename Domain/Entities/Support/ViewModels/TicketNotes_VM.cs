using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Notes
{
    public class TicketNotes_VM
    {
        public List<Notes> Support_Clients { get; set; }
        public List<SupportTicketAttachments> Support_Tickets { get; set; }

        public List<SupportUserInfo> AssigneeList { get; set; }

    }
}
