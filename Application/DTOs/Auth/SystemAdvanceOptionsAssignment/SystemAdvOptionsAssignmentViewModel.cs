using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth
{
    public class SystemAdvOptionsAssignmentViewModel
    {
        public long SeqNum { get; set; }
        public long AdvanceOptionsSeqNum { get; set; }
        public long EntityId { get; set; }
        public string Visible { get; set; }
        public string Title { get; set; }
        public string HelpText { get; set; }
    }
}
