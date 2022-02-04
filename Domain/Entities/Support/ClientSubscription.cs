using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Support
{
    public class ClientSubscription
    {
        public virtual long Seq_Num { get; set; }
        public virtual string Short_Name { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zipcode { get; set; }
        public virtual string Zipcode_Ext { get; set; }
        public virtual long? Tel_Num { get; set; }
        public virtual string Email { get; set; }
        public virtual string Contact_Person { get; set; }
        public virtual string Primary_Admin { get; set; }
        public virtual string Active_Flag { get; set; }
        public virtual string Customer_Id { get; set; }
        public virtual DateTime? Next_Billing_Date { get; set; }
        public virtual long? Auth_Entity_Seq_Num { get; set; }
        public virtual string Payment_Cycle { get; set; }
        public virtual string Payment_Terms { get; set; }
        public virtual string Credit_Card_No { get; set; }
        public virtual string Card_Type { get; set; }
        public virtual string Expiry_Month_Year { get; set; }
        public virtual string Card_Holder_Name { get; set; }
        public virtual string Wallet_Id { get; set; }
        public virtual string Rcm_Credit_Card_No { get; set; }
        public virtual string Rcm_Card_Type { get; set; }
        public virtual string Rcm_Expiry_Month_Year { get; set; }
        public virtual string Rcm_Card_Holder_Name { get; set; }
        public virtual string Rcm_Wallet_Id { get; set; }
        public virtual string Payment_Gateway { get; set; }
        public virtual string Is_Preferred { get; set; }

    }
}
