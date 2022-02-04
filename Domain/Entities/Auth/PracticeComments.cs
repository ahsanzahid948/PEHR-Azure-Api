using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class PracticeComments
    {
        public virtual long Seq_Num { get; set; }
        public virtual string Comments { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual DateTime? Entered_Date { get; set; }
        public virtual long? Entitty_Seq_Num { get; set; }
    }
}
