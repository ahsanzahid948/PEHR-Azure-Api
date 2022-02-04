using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.AdvanceOptionAssignment
{
    public class AdvanceOptionAssignmentViewModel
    {
        public virtual long OptionId { get; set; }
        public virtual long? AdvanceOptionId { get; set; }
        public virtual long? EntityId { get; set; }
        public virtual string Visible { get; set; }
        public virtual string EnteredBy { get; set; }
        public virtual DateTime? EnteryDate { get; set; }

    }
}
