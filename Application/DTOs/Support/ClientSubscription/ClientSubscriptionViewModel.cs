using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.ClientSubscription
{
    public class ClientSubscriptionViewModel
    {
        public virtual long ClientId { get; set; }
        public virtual string ShortName { get; set; }
        public virtual string Name { get; set; }
        public virtual string AddressLineOne { get; set; }
        public virtual string AddressLineTwo { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zipcode { get; set; }
        public virtual string ZipcodeExt { get; set; }
        public virtual long? TelephoneNum { get; set; }
        public virtual string Email { get; set; }
        public virtual string ContactPerson { get; set; }
        public virtual string PrimaryAdmin { get; set; }
        public virtual string Active { get; set; }
        public virtual string CustomerId { get; set; }
        public virtual DateTime? NextBillingDate { get; set; }
        public virtual long? AuthEntityId { get; set; }
        public virtual string PaymentCycle { get; set; }
        public virtual string PaymentTerms { get; set; }
        public virtual string CreditCardNo { get; set; }
        public virtual string CardType { get; set; }
        public virtual string ExpiryMonthYear { get; set; }
        public virtual string CardHolderName { get; set; }
        public virtual string WalletId { get; set; }
        public virtual string RCMCreditCardNo { get; set; }
        public virtual string RCMCardType { get; set; }
        public virtual string RCMExpiryMonthYear { get; set; }
        public virtual string RCMCardHolderName { get; set; }
        public virtual string RCMWalletId { get; set; }
        public virtual string PaymentGateway { get; set; }
        public virtual string Preferred { get; set; }
    }
}
