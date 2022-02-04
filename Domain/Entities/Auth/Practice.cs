using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
    public class Practice
    {
        public virtual long Seq_Num { get; set; }
        public virtual string Practice_Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zip_Code { get; set; }
        public virtual string Zip_Code_Ext { get; set; }
        public virtual string Speciality { get; set; }
        public virtual string Tax_Id { get; set; }
        public virtual string Group_Npi { get; set; }
        public virtual string Phone_No { get; set; }
        public virtual string Pehr_Provided_Fax_No { get; set; }
        public virtual string Product_Type { get; set; }
        public virtual long? No_Of_Providers { get; set; }
        public virtual string Pre_Ehr_Vendor { get; set; }
        public virtual string Imp_Conact_Name { get; set; }
        public virtual string Imp_Conact_Phone { get; set; }
        public virtual string Imp_Conact_Email { get; set; }
        public virtual string Data_Migration { get; set; }
        public virtual string Email_Appt_Reminder { get; set; }
        public virtual string Call_Sms_Appt_Reminder { get; set; }
        public virtual string Direct_Email { get; set; }
        public virtual string Electronic_Prescription { get; set; }
        public virtual string Elect_Pat_Statment_Service { get; set; }
        public virtual string Lab_Inegration { get; set; }
        public virtual string Epcs_Set_Up { get; set; }
        public virtual string Telemedicine { get; set; }
        public virtual string Company_Logo { get; set; }
        public virtual string FEE_Schedule { get; set; }
        public virtual string Paper_Encounter_Super_Bill { get; set; }
        public virtual string Kiosk_Checkin { get; set; }
        public virtual string Prepopulate_Patient { get; set; }
        public virtual string Group_MCR_PTAN { get; set; }
        public virtual string Group_MCR_DME { get; set; }
        public virtual string Group_MCR_RR { get; set; }
        public virtual long? Entitty_Seq_Num { get; set; }
        public virtual string Comments { get; set; }
        public virtual string Medcaid_Provider { get; set; }
        public virtual string BCBS_Provider { get; set; }
        public virtual string Provider_Type { get; set; }
        public virtual string EHR_Notified { get; set; }
        public virtual string No_Insurance_Needed { get; set; }

    }
}
