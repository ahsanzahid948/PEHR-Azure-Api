using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ClientProfile
    {
        public virtual long Seq_Num { get; set; }
        public virtual string Short_Name { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string ZipCode_Ext { get; set; }
        public virtual string Tel_Num { get; set; }
        public virtual string Email { get; set; }
        public virtual string Contact_Person { get; set; }
        public virtual string Primary_Admin { get; set; }
        public virtual string Card_Number { get; set; }
        public virtual string Card_Holder_Name { get; set; }
        public virtual string Expiry_Month_Year { get; set; }
        public virtual string Cvc_Code { get; set; }
        public virtual string Active_Flag { get; set; }
        public virtual string Entered_By { get; set; }
        public virtual DateTime Entry_Date { get; set; }
        public virtual long Company_Seq_Num { get; set; }
        public virtual string Customer_Id { get; set; }
        public virtual string Wallet_Id { get; set; }
        public virtual string Credit_Card_No { get; set; }
        public virtual string Card_Type { get; set; }
        public virtual string Flat_Fee { get; set; }
        public virtual string Provider_Fee { get; set; }
        public virtual DateTime Live_Date { get; set; }
        public virtual string Implementation_Manager { get; set; }
        public virtual string Comments { get; set; }
        public virtual string Sales_Person { get; set; }
        public virtual DateTime Client_Activation_Date { get; set; }
        public virtual string Speciality { get; set; }
        public virtual string Implemnetation_Comments { get; set; }
        public virtual string Payment_Cycle { get; set; }
        public virtual string Payment_Terms { get; set; }
        public virtual string Implementation_Contact { get; set; }
        public virtual string Reseller { get; set; }
        public virtual string Implementation_Coordinator { get; set; }

    }
}
