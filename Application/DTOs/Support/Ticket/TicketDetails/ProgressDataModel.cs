using Application.DTOs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Ticket
{
    public class ProgressDataModel
    {
        public string DsPracticeSetup { get; set; }

        public List<PracticeProgressDetailViewModel> ProgressData { get; set; }

        public List<TicketViewModel> AllTasks { get; set; }
        public List<TicketViewModel> OverdueTask { get; set; }

    }
}
