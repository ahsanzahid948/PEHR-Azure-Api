using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Task
{
    public class TaskFilter : PayLoad
    {
        public string Priority { get; set; }
        public string TxtAllKeyWords { get; set; }
        public string Status { get; set; }
        public string Assignee { get; set; }
        public string PracticeName { get; set; }
        public string Client { get; set; }
        public string CustAccNum { get; set; }
        public string Team { get; set; }
        public string TxtInternalAssignee { get; set; }
    }
}
