using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class AdvanceOptionAssignment
    {
        public virtual long Seq_Num { get; set; }
        public virtual long? Advance_Options_Seq_Num { get; set; }
        public virtual long? Entity_Seq_Num { get; set; }
        public virtual string Visible { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual DateTime? Entery_Date { get; set; }
    }
}
