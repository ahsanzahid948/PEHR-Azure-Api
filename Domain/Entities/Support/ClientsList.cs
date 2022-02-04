using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ClientsList
    {
        public virtual long Entity_Seq_Num { get; set; }
        public virtual string Entity_Short_Name { get; set; }
        public virtual string Active_Flag { get; set; }
        public virtual string Speciality { get; set; }
        public virtual long Client_Seq_Num { get; set; }
        public virtual string Ec_Flag { get; set; }
        public virtual string Account_Type { get; set; }
        public virtual DateTime Trial_Start_Date { get; set; }
        public virtual DateTime Trial_End_Date { get; set; }
        public virtual DateTime Live_Date { get; set; }
        public virtual DateTime Paid_Date { get; set; }
        public virtual string Practice_Name { get; set; }
        public virtual string Practice_Address { get; set; }
        public virtual string Practice_Address2 { get; set; }
        public virtual string Practice_City { get; set; }
        public virtual string Practice_State { get; set; }
        public virtual string Practice_ZipCode { get; set; }
        public virtual string Practice_Tel_Num { get; set; }
        public virtual string Practice_Emil { get; set; }
        public virtual string Contact_Person { get; set; }
        public virtual string Primary_Admin { get; set; }
        public virtual string Card_Holder_Name { get; set; }
        public virtual string Expiry_Month_Year { get; set; }
        public virtual string CustomerId { get; set; }
        public virtual string Waller_Id { get; set; }
        public virtual string Credit_Card_No { get; set; }
        public virtual string Card_Type { get; set; }
        public virtual string Num_Of_Users { get; set; }
        public virtual DateTime Trial_Activation_Date { get; set; }
        public virtual string Trial_Activation_Status { get; set; }
        public virtual string Entity_Seq_Num_Char { get; set; }
        public virtual DateTime Trial_Registration_Date { get; set; }
        public virtual string Sales_Person { get; set; }
        public virtual string Company { get; set; }
        public virtual long AuthEntity_Seq_Num { get; set; }
        public virtual long Seq_Num { get; set; }
        public virtual long Entity_Practice_Seq_Num { get; set; }
        public virtual string Flat_Fee { get; set; }
        public virtual string Provider_Fee { get; set; }
        public virtual DateTime Next_Billing_Date { get; set; }
        public virtual string Implementation_Manager { get; set; }
        public virtual string Comments { get; set; }
        public virtual string Implmenetation_Comments { get; set; }
        public virtual string Payment_Cycle { get; set; }
        public virtual string Payment_Terms { get; set; }
        public virtual string Description { get; set; }
        public virtual string Implmentation_Contact { get; set; }
        public virtual string Reseller { get; set; }
        public virtual string Channel_Partner { get; set; }
        public virtual string Stage { get; set; }
        public virtual string Alert { get; set; }
        public virtual DateTime Expected_Live_Date { get; set; }
        public virtual string Timezone { get; set; }
        public virtual string User_Type { get; set; }
        public virtual string Type { get; set; }
        public virtual string Billing_Flag { get; set; }
        public virtual string RcmType { get; set; }
        public virtual string CH_Type { get; set; }
        public virtual string Is_Tele_Clinic { get; set; }
        public virtual string Kiosk_Flag { get; set; }

        public virtual string Implementation_Coordinator { get; set; }
    }
}
