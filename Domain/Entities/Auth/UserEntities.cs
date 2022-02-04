using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class UserEntities
    {
        public virtual long Seq_Num { get; set; }
        public virtual long Entity_Seq_Num { get; set; }
        public virtual string Description { get; set; }
        public virtual string Entity_Short_Name { get; set; }
        public virtual string Email { get; set; }
        public virtual long App_User_Seq_Num { get; set; }
        public virtual string User_Name { get; set; }

    }
}
