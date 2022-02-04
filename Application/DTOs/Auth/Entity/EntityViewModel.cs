using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Entity
{
    public class EntityViewModel
    {
        public virtual long EntityId { get; set; }
        public virtual long DBEntityId { get; set; }
        public virtual string ShortName { get; set; }
        public virtual string Description { get; set; }
        public virtual string LongDescription { get; set; }
        public virtual string Speciality { get; set; }
        public virtual string AccountType { get; set; }
        public virtual DateTime TrialStartDate { get; set; }
        public virtual DateTime TrialEndDate { get; set; }
        public virtual string Active { get; set; }
        public virtual string IISServerSeqNum { get; set; }
        public virtual string ClientSeqNum { get; set; }
        public virtual string MerchantClientId { get; set; }
        public virtual string GatwayId { get; set; }
        public virtual string TransFirst { get; set; }
        public virtual string NextBillingDate { get; set; }
        public virtual string PracticeSeqNum { get; set; }
        public virtual string PracticeBased { get; set; }
        public virtual string PaidDate { get; set; }
        public virtual string LiveDate { get; set; }
        public virtual string MultiPracticeSetup { get; set; }
        public virtual string PaymentGateway { get; set; }
        public virtual string StripePublicKey { get; set; }
        public virtual string TeleVisitPaymentMode { get; set; }
        public virtual string TeleVisitPaymentAmount { get; set; }
        public virtual string ExpectedLiveDate { get; set; }
        public virtual string Stage { get; set; }
        public virtual string Alert { get; set; }
        public virtual string Trialdays { get; set; }


    }
}
