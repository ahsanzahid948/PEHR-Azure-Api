using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.EntityConfigurations
{
   public class EntityConfigurationViewModel
    {
        public virtual long EntityId { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual string ECFlag { get; set; }
        public virtual string  BillingFlag  { get; set; }
        public virtual string TeleVisitFlag { get; set; }
        public virtual string AnnotationIconFlag { get; set; }
        public virtual string AnnotationCompression { get; set; }
        public virtual string ECRxFlag { get; set; }
        public virtual string EMRToEPMServer { get; set; }
        public virtual string WriteJsLog { get; set; }
        public virtual string SlotDuration { get; set; }
        public virtual string SystemSetupBar { get; set; }
        public virtual string ShowGrowthChart { get; set; }
        public virtual string ShowSupportCenter { get; set; }
        public virtual string SupperBillLetterSeqNum { get; set; }
        public virtual string ISUrgentCareFlow { get; set; }
        public virtual string AllowGoogleAds { get; set; }
        public virtual string ShowTrackerBoard { get; set; }
        public virtual string PMFlag { get; set; }
        public virtual string BillingBy { get; set; }
        public virtual string ICDRequriedOnErx  { get; set; }
        public virtual string CancelOrginalRx { get; set; }
        public virtual string NdcSameAsUnit { get; set; }
        public virtual string CashOnly { get; set; }
        public virtual string AutoVitalSigns { get; set; }
        public virtual string SpecialitySpecificCC { get; set; }
        public virtual string ShowProviderSchedule { get; set; }
        public virtual string ErxOnly { get; set; }
        public virtual string SystemAdvanceOption { get; set; }
        public virtual string UBFlag { get; set; }
        public virtual string ChiroRx { get; set; }
        public virtual string EraPrintOnStatement { get; set; }      
        public virtual string CDSFlag { get; set; }       
        public virtual string PastVisitDuration { get; set; }
        public virtual string PracticeSetup { get; set; }
        public virtual string RcmTaskEmailTo { get; set; }
        public virtual string CssnLicenseKey { get; set; }      
        public virtual string EVVTasks { get; set; }
        public virtual string FaxServer { get; set; }
        public virtual string RxEmrServer { get; set; }
        public virtual string ChannelPartner { get; set; }
        public virtual string Alert { get; set; }
        public virtual string Stage  { get; set; }
        public virtual string KareoCustomerKey { get; set; }
        public virtual string KareoUser { get; set; }
        public virtual string KareoPassword { get; set; }
        public virtual string StripeSwipe { get; set; }
        public virtual string GlobalPaymentPassword { get; set; }
        public virtual string IsIhcfa { get; set; }
        public virtual string StripePrivateKey { get; set; }


    }
}
