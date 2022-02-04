using Application.DTOs.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Notes
{
    public class ClientNoteViewModel
    {
        public List<NotesViewModel> DsNoteDetails { get; set; }
        public List<TicketAttachmentsViewModel> DsAttachments { get; set; }
        public List<UserInfoViewModel> AssigneeDropdown { get; set; }

    }
}
