using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{ 
    public class PracticeProgressDetailViewModel
    {
        public string AccountType { get; set; }
        public string AutoStatus { get; set; }
        public string Description { get; set; }
        public long EntityId { get; set; }
        public DateTime EntryDate { get; set; }
        public string LastComment { get; set; }
        public long ProgressId { get; set; }
        public string Status { get; set; }
        public string SupportMessage { get; set; }
        public string TargetCompletion { get; set; }
        public string TicketNo { get; set; }
        public string TicketStatus { get; set; }
        public string Title { get; set; }


    }
}
