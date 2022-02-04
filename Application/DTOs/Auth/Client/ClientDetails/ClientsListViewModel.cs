using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.Client
{
    public class ClientsListViewModel : PayLoad
    {
        public virtual long EntityId { get; set; }
        public virtual string EntityShortName { get; set; }
        public virtual string ActiveFlag { get; set; }
        public virtual string Speciality { get; set; }
        public virtual long ClientId { get; set; }
        public virtual string EcFlag { get; set; }
        public virtual string AccountType { get; set; }
        public virtual DateTime TrialStartDate { get; set; }
        public virtual DateTime TrialEndDate { get; set; }
        public virtual DateTime LiveDate { get; set; }
        public virtual DateTime PaidDate { get; set; }
        public virtual string PracticeName { get; set; }
        public virtual string PracticeAddress { get; set; }
        public virtual string PracticeAddress2 { get; set; }
        public virtual string PracticeCity { get; set; }
        public virtual string PracticeState { get; set; }
        public virtual string PracticeZipCode { get; set; }
        public virtual string PracticeTelNum { get; set; }
        public virtual string PracticeEmail { get; set; }
        public virtual string ContactPerson { get; set; }
        public virtual string PrimaryAdmin { get; set; }
        public virtual string CardHolderName { get; set; }
        public virtual string ExpiryMonthYear { get; set; }
        public virtual long CustomerId { get; set; }
        public virtual string WalletId { get; set; }
        public virtual string CreditCardNo { get; set; }
        public virtual string CardType { get; set; }
        public virtual string NumofUsers { get; set; }
        public virtual DateTime TrialActivationDate { get; set; }
        public virtual string TrialActivationStatus { get; set; }
        public virtual string EntityIdChar { get; set; }
        public virtual DateTime TrialRegistrationDate { get; set; }
        public virtual string SalesPeron { get; set; }
        public virtual string Company { get; set; }
        public virtual long AuthEntityId { get; set; }
        public virtual long PEHRClientId { get; set; }
        public virtual long EntityPracticeId { get; set; }
        public virtual string FlatFee { get; set; }
        public virtual string ProviderFee { get; set; }
        public virtual DateTime NextBillingDate { get; set; }
        public virtual string ImplementationManager { get; set; }
        public virtual string Comments { get; set; }
        public virtual string ImplementationComments { get; set; }
        public virtual string PaymentCycle { get; set; }
        public virtual string PaymentTerms { get; set; }
        public virtual string Description { get; set; }
        public virtual string ImplementationContact { get; set; }
        public virtual string ChannelPartner { get; set; }
        public virtual string Stage { get; set; }
        public virtual string Alert { get; set; }
        public virtual DateTime ExpectedLiveDate { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual string UserType { get; set; }
        public virtual string Type { get; set; }
        public virtual string BillingFlag { get; set; }
        public virtual string RcmType { get; set; }
        public virtual string ChType { get; set; }
        public virtual string IsTeleClinic { get; set; }
        public virtual string KioskFlag { get; set; }
        public virtual string ImplementationCoordinator { get; set; }
    }
}
