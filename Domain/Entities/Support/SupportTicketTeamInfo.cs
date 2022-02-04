using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SupportTicketTeamInfo
    {
        public virtual long Seq_Num { get; set; }
        public virtual string Email { get; set; }
        public virtual string Name { get; set; }
    }
}
