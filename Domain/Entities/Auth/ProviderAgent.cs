using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class ProviderAgent
    {
        public virtual long Seq_Num { get; set; }
        public virtual long? Provider_Seq_Num { get; set; }
        public virtual long? User_Agent_Seq_Num { get; set; }
        public virtual long? Auth_Entity_Seq_Num { get; set; }
    }
}
