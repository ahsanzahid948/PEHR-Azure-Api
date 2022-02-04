using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
    public class PracticeSetup
    {
        public virtual long Seq_Num { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string Auto_Status { get; set; }
        public virtual long? Entity_Seq_Num { get; set; }
        public virtual string Status { get; set; }
        public virtual string Support_Message { get; set; }

    }
}
