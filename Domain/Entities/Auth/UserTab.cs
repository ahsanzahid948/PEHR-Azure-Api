using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class UserTab
    {
        public virtual string Login { get; set; }
        public virtual string Role { get; set; }
        public virtual long Role_Seq_Num { get; set; }
        public virtual long Seq_Num { get; set; }
        public virtual string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Assignment_Flag { get; set; }
        public virtual string Allow_Delete { get; set; }

    }
}
