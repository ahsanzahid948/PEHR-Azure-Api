using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.RoleAssignment
{
    public class RoleAssignmentViewModel
    {
        public virtual long AssignmentId { get; set; }
        public virtual long? RoleId { get; set; }
        public virtual long? MenuId { get; set; }
        public virtual long? TabMenuId { get; set; }
        public virtual long? MenuButtonId { get; set; }
        public virtual string Assignment { get; set; }
        public virtual string EnteredBy { get; set; }
        public virtual DateTime? EntryDate { get; set; }
    }
}
