using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Support.PaymentInvoice
{
    public class PaymentInvoiceViewModel
    {
        public virtual long InvoiceId { get; set; }
        public virtual long? ClientId { get; set; }
        public virtual long? InvoiceNumber { get; set; }
        public virtual double? InvoiceAmount { get; set; }
        public virtual string InvoiceFilePath { get; set; }
        public virtual DateTime? InvoiceDate { get; set; }
        public virtual DateTime? EntryDate { get; set; }
        public virtual string EnteredBy { get; set; }
        public virtual long? AuthEntityId { get; set; }
        public virtual string FileName { get; set; }
        public virtual string InvoiceName { get; set; }
        public virtual string Type { get; set; }

    }
}
