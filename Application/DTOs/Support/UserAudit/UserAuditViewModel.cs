using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.UserAudit
{
    public class UserAuditViewModel
    {
        public virtual string DisplayColName { get; set; }
        public virtual string OrignalValue { get; set; }
        public virtual string CurrentValue { get; set; }
        public virtual string AuditUser { get; set; }
        public virtual DateTime? AuditTime { get; set; }
    }
}
