using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.MenuRoleAudit
{
    public class MenuRoleAuditViewModel
    {
        public virtual string Value { get; set; }
        public virtual string CurrentValue { get; set; }
        public virtual string AuditUser { get; set; }
        public virtual DateTime? AuditTime { get; set; }
        public virtual string MenuRole { get; set; }
    }
}
