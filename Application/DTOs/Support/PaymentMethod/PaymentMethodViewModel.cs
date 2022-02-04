using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.PaymentMethod
{
    public class PaymentMethodViewModel
    {
        public virtual long PaymentId { get; set; }
        public virtual long? ClientId { get; set; }
        public virtual long? AuthEntityId { get; set; }
        public virtual string CreditCardNo { get; set; }
        public virtual string ExpiryMonthYear { get; set; }
        public virtual string CardType { get; set; }
        public virtual string CardHolderName { get; set; }
        public virtual string PayerId { get; set; }
        public virtual string Preferred { get; set; }
        public virtual string Type { get; set; }
    }
}
