using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
    public class PaymentInvoice
    {
        public virtual long Seq_Num { set; get; }
        public virtual long? Client_Seq_Num { set; get; }
        public virtual long? Invoice_Number { set; get; }
        public virtual double? Invoice_Amount { set; get; }
        public virtual string Invoice_Pdf { set; get; }
        public virtual DateTime? Invoice_Date { set; get; }
        public virtual DateTime? Entry_Date { set; get; }
        public virtual string Entered_By { set; get; }
        public virtual long? Auth_Entity_Seq_Num { set; get; }
        public virtual string File_Name { set; get; }
        public virtual string Invoice_Name { set; get; }
        public virtual string Type { set; get; }

    }
}
