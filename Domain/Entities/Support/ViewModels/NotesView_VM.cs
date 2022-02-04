using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Notes
{
    public class NotesView_VM
    {
        public Notes ClientNote { get; set; }
        public List<SupportTicketAttachments> NoteAttachment { get; set; }

        public List<SupportUserInfo> AssignedUser { get; set; }

    }
}
