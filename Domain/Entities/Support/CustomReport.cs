using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
    public class CustomReport
    {
        public virtual long Seq_Num { get; set; }
        public virtual long? Entity_Seq_Num { get; set; }
        public virtual string Report_Description { get; set; }
        public virtual string Report_Name { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual DateTime? Entry_Date { get; set; }
        public virtual string Telerik_Report_Id { get; set; }
        public virtual long? Sort_Order { get; set; }
        public virtual string Cust_View { get; set; }

    }
}
