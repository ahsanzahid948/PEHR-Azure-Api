using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class AdvanceOption
    {
        public virtual long Seq_Num { get; set; }
        public virtual string Description { get; set; }
        public virtual string Type { get; set; }
        public virtual string Title { get; set; }
        public virtual string Help_Text { get; set; }
    }
}
