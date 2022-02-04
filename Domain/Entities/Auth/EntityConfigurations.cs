using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Auth
{
   public class EntityConfiguration
    {
        public virtual long Seq_Num { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual string EC_Flag { get; set; }
        public virtual string Billing_Flag { get; set; }
        public virtual string IS_Tele_Clinic { get; set; }
        public virtual string Annotation_Icons { get; set; }
        public virtual string Annotation_Compression { get; set; }   
        public virtual string ECERX { get; set; }
        public virtual string EmrToEpmServer { get; set; }
        public virtual string Write_Js_Log { get; set; }
        public virtual string Duration_Slot { get; set; }
        public virtual string System_Setup_Bar { get; set; }      
        public virtual string Show_Growth_Chart { get; set; }
        public virtual string Show_Support_Center { get; set; }
        public virtual string SuperBill_Letter_Seq_Num { get; set; }
        public virtual string Urgent_Care_Flow { get; set; }
        public virtual string Allow_Google_Ads { get; set; }
        public virtual string Show_Tracker_Board { get; set; }
        public virtual string Pm_Flag { get; set; }
        public virtual string Billing_By { get; set; }
        public virtual string Icd_Erx_Req { get; set; }
        public virtual string Cancel_Orginal_Rx { get; set; }
        public virtual string Ndc_Same_As_Unit { get; set; }
        public virtual string Cash_Only { get; set; }
        public virtual string Auto_Vital_Signs { get; set; }
        public virtual string Speciality_Specific_Cc { get; set; }
        public virtual string Show_Provider_Schedule { get; set; }
        public virtual string Erx_Only { get; set; }
        public virtual string Sys_Advance_Option { get; set; }
        public virtual string Ub { get; set; }
        public virtual string Chiro_Rx { get; set; }
        public virtual string Era_Print_On_Statement { get; set; }      
        public virtual string Cds_Flag { get; set; }
        public virtual string Past_Visit_Duration { get; set; }
        public virtual string Practice_Setup { get; set; }
        public virtual string RcmTask_Email_To { get; set; }
        public virtual string Cssn_Licence_Key { get; set; }
        public virtual string Evv_Tasks { get; set; }
        public virtual string FaxServer { get; set; }
        public virtual string RxEmrServer { get; set; }
        public virtual string Channel_Partner { get; set; }
        public virtual string Alert { get; set; }
        public virtual string Stage { get; set; }
        public virtual string Kareo_Customer_Key { get; set; }
        public virtual string Kareo_User { get; set; }
        public virtual string Kareo_Password { get; set; }
        public virtual string Stripe_Swipe { get; set; }
        public virtual string Global_Payment_Password { get; set; }
        public virtual string Ihcfa_Flag { get; set; }
        public virtual string Stripe_Private_Key { get; set; }

    }
}
