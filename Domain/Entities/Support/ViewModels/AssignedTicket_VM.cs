using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AssignedTicket_VM
    {
        public List<SupportUserInfo> Ds_Login { get; set; }
        public List<SupportTicketTeamInfo> Ds_Team { get; set; }
    }
}
