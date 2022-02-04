using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
    public class MergedDocument
    {
        public virtual long Seq_Num { get; set; }
        public virtual long? Auth_Seq_Num { get; set; }
        public virtual long? Patient_Seq_Num { get; set; }
        public virtual string Document_Seq_Num { get; set; }
        public virtual string Download_Completed { get; set; }
        public virtual string Assignee_User { get; set; }
        public virtual DateTime? Expiry_Date { get; set; }
        public virtual long? User_Messages_Seq_Num { get; set; }
        public virtual string Is_Expired { get; set; }

    }
}
