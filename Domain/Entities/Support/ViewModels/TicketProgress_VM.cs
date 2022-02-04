using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TicketProgress_VM
    {
        public string Practice_Setup { get; set; }

        public List<PracticeProgressDetail> Progress_Data { get; set; }

        public List<SupportTicket> All_Tasks { get; set; }
        public List<SupportTicket> Overdue_Tasks { get; set; }
    }
}
