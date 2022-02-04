using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
    public class PaymentMethod
    {
        public virtual long Seq_Num { get; set; }
        public virtual long? Client_Seq_Num { get; set; }
        public virtual long? Auth_Entity_Seq_Num { get; set; }

        public virtual string Credit_Card_No { get; set; }
        public virtual string Expiry_Month_Year { get; set; }
        public virtual string Card_Type { get; set; }
        public virtual string Card_Holder_Name { get; set; }
        public virtual string Payerid { get; set; }
        public virtual string Is_Preferred { get; set; }
        public virtual string Type { get; set; }

    }
}
