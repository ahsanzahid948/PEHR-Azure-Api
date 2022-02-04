using System;
namespace Application.DTOs.Support.Practice
{
    public class AuditViewModel
    {
        public long AuditId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string EnteredBy { get; set; }
        public DateTime EntryDate { get; set; }
        public long EntityId { get; set; }
    }
}
