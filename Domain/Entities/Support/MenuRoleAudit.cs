using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
    public class MenuRoleAudit
    {
        public virtual long Seq_Num { get; set; }
        public virtual string Data_Table_Name { get; set; }
        public virtual string Audit_Table_Name { get; set; }
        public virtual string Data_Col_Name { get; set; }
        public virtual string Display_Col_Name { get; set; }
        public virtual string Primary_Key { get; set; }
        public virtual long? Row_Seq_Num { get; set; }
        public virtual string Col_Type { get; set; }
        public virtual string Org_Value { get; set; }
        public virtual string Current_Value { get; set; }
        public virtual string Audit_User { get; set; }
        public virtual DateTime? Audit_Timestamp { get; set; }
        public virtual string Audit_Action { get; set; }
        public virtual string Unique_Name { get; set; }
        public virtual string Role_Name { get; set; }
        public virtual string Visible_Flag { get; set; }
        public virtual string Machine { get; set; }
        public virtual string Description { get; set; }
        public virtual string Menu_Button { get; set; }
        public virtual string Menu_Role { get; set; }
    }
}
