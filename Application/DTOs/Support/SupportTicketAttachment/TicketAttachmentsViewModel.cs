namespace Application.DTOs.Notes
{
    using System;

    public class TicketAttachmentsViewModel
    {
        public long TicketAttachmentId { get; set; }
        public string TicketNumber { get; set; }
        public long CommentSeqNum { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime EntryDate { get; set; }
        public string EnteredBy { get; set; }

    }
}
