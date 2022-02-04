using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class ProviderLocation
    {
        public virtual long Seq_Num { get; set; }
        public virtual long? Auth_Entity_Seq_Num { get; set; }
        public virtual string Login_User { get; set; }
        public virtual long? Provider_Seq_Num { get; set; }
        public virtual long? Location_Seq_Num { get; set; }
    }
}
