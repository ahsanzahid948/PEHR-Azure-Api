using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.Client
{
    public class ClientProfileViewModel
    {
        public virtual long ClientProfileId { get; set; }
        public virtual string ShortName { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address1 { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string ZipCodeExt { get; set; }
        public virtual string TelNumber { get; set; }
        public virtual string Email { get; set; }
        public virtual string ContactPerson { get; set; }
        public virtual string PrimaryAdmin { get; set; }
        public virtual string Cardnumber { get; set; }
        public virtual string CardHolderName { get; set; }
        public virtual DateTime ExpiryMonthYear { get; set; }
        public virtual string Cvccode { get; set; }
        public virtual string ActiveFlag { get; set; }
        public virtual string EnteredBy { get; set; }
        public virtual DateTime EntryDate { get; set; }
        public virtual long CompanySeqNum { get; set; }
        public virtual long CustomerId { get; set; }
        public virtual string WalletId { get; set; }
        public virtual string CreditCardNo { get; set; }
        public virtual string CardType { get; set; }
        public virtual string FlatFee { get; set; }
        public virtual string ProviderFee { get; set; }
        public virtual string LiveDate { get; set; }
        public virtual string ImplementationManager { get; set; }
        public virtual string Comments { get; set; }
        public virtual string SalesPerson { get; set; }
        public virtual string ClientActivationCode { get; set; }
        public virtual string Speciality { get; set; }
        public virtual string ImplementationComments { get; set; }
        public virtual string PaymentCycle { get; set; }
        public virtual string PaymentTerms { get; set; }
        public virtual string ImplementationContact { get; set; }
        public virtual string ImplementationCoordinator { get; set; }

    }
}
