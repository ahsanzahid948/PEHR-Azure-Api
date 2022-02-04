using System;
using System.Collections.Generic;

namespace Domain.Entities.Auth
{
    public class Entity
    {
        
        public virtual long Seq_Num { get; set; }
        public virtual long Entity_Seq_Num { get; set; }
        public virtual string Short_Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Long_Description { get; set; }
        public virtual string Speciality { get; set; }
        public virtual string Account_Type { get; set; }
        public virtual DateTime Trial_Start_Date { get; set; }
        public virtual DateTime Trial_End_Date { get; set; }
        public virtual string Active_Flag { get; set; }
        public virtual string IIS_Server_Seq_Num { get; set; }
        public virtual string Client_Seq_Num { get; set; }
        public virtual string Merchant_Client_Id { get; set; }
        public virtual string Gatway_Id { get; set; }
        public virtual string Trans_First { get; set; }
        public virtual string Next_Billing_Date { get; set; }
        public virtual string Practice_Seq_Num { get; set; }
        public virtual string Practice_Based { get; set; }
        public virtual DateTime? Paid_Date { get; set; }
        public virtual DateTime? Live_Date { get; set; }
        public virtual string Multi_Practice_Setup { get; set; }
        public virtual string Payment_Gateway { get; set; }
        public virtual string Stripe_Public_Key { get; set; }
        public virtual string Practice_Setup { get; set; }
        public virtual string Tele_Visit_Payment_Mode { get; set; }
        public virtual string Tele_Visit_Payment_Amount { get; set; }
        public virtual DateTime Expected_Live_Date { get; set; }
        public virtual string Trialdays { get; set; }

    }
}
