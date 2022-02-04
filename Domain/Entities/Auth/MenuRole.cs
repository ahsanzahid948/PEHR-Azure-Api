using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class MenuRole
    {
        public virtual long Seq_Num { get; set; }
        public virtual string Short_Name { get; set; }
        public virtual string Comments { get; set; }
        public virtual long? Entity_Seq_Num { get; set; }
        public virtual string Is_Default { get; set; }

    }
}
