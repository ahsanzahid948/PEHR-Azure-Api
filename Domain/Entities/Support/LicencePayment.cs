using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
    public class LicencePayment
    {
        public virtual long Seq_Num { get; set; }
        public virtual long? Client_Seq_Num { get; set; }
        public virtual long? Auth_Entity_Seq_Num { get; set; }
        public virtual double? Amount_Paid { get; set; }
        public virtual string Transaction_Id { get; set; }
        public virtual DateTime? Transaction_Date { get; set; }
        public virtual DateTime? Entry_Date { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual long? Invoice_Seq_Num { get; set; }
        public virtual string Type { get; set; }
        public virtual string Invoice_Number { get; set; }
        public virtual string Payment_Method { get; set; }
        public virtual string Credit_Card_No { get; set; }
        public virtual string Check_Number { get; set; }

    }
}
