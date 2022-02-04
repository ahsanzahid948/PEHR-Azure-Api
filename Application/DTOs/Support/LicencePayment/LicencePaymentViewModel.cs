using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.LicencePayment
{
    public class LicencePaymentViewModel
    {
        public virtual long LicenceId { get; set; }
        public virtual long? ClientId { get; set; }
        public virtual long? AuthEntityId { get; set; }
        public virtual double? AmountPaid { get; set; }
        public virtual string TransactionId { get; set; }
        public virtual DateTime? TransactionDate { get; set; }
        public virtual DateTime? EntryDate { get; set; }
        public virtual string EnteredBy { get; set; }
        public virtual long? InvoiceId { get; set; }
        public virtual string Type { get; set; }
        public virtual string InvoiceNumber { get; set; }
        public virtual string PaymentMethod { get; set; }
        public virtual string CreditCardNo { get; set; }
        public virtual string CheckNumber { get; set; }

    }
}
