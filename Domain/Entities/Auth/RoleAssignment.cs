using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class RoleAssignment
    {
        public virtual long Seq_Num { get; set; }
        public virtual long? Role_Seq_Num { get; set; }
        public virtual long? Menu_Seq_Num { get; set; }
        public virtual long? Tab_Menu_Seq_Num { get; set; }
        public virtual long? Menu_Button_Seq_Num { get; set; }
        public virtual string Assignment_Flag { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual DateTime? Entry_Date { get; set; }

    }
}
