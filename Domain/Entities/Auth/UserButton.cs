using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class UserButton
    {
        public virtual string Login { get; set; }
        public virtual string Role { get; set; }
        public virtual long Role_Seq_Num { get; set; }
        public virtual long Seq_Num { get; set; }
        public virtual long? Menu_Seq_Num { get; set; }
        public virtual long? Tab_Seq_Num { get; set; }
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Assignment_Flag { get; set; }
        public virtual string Tab { get; set; }
        public virtual string Menu { get; set; }
        public virtual string Category_Type { get; set; }

    }
}
