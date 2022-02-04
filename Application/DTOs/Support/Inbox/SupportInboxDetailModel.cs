using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Ticket
{
    public class SupportInboxDetailModel
    {
        public long InboxId { get; set; }
        public string ViewStatus { get; set; }
        public string TicketNo { get; set; }
        public string EmailText { get; set; }
        public string Description { get; set; }
        public long UserPrefSeqNum { get; set; }
        public long UserSeqNum { get; set; }
        public string Type { get; set; }
        public string NotificationType { get; set; }
        public DateTime EnteredDate { get; set; }
        public string EmailTextBlob { get; set; }
        public string EmailBlobTextJs { get; set; }

    }
}
