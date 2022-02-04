using Application.DTOs.Common.CreateRequestResponse;
using Domain.Entities.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Support
{
    public interface ISubscriptionRepositoryAsync : IGenericRepositoryAsync<PaymentMethod>
    {
        public Task<IReadOnlyList<PaymentMethod>> GetPaymentMethodsByIdAsync(long id, string type);
        public Task<IReadOnlyList<LicencePayment>> GetPaymentsByIdAsync(long id, string clientId);
        Task<IReadOnlyList<PaymentInvoice>> GetInvoicesByIdAsync(long id);
        Task<CreateRequestResponse> CreateInvoiceAsync(PaymentInvoice paymentInvoice);
        Task<int> CreatePaymentMethodAsync(PaymentMethod paymentMethod);
        Task<int> CreatePaymentAsync(LicencePayment licencePayment);
        Task<PaymentMethod> GetPrefferedCardAsync(long entityId, string type);
        Task<int> UpdatePrefferedCardAsync(string paymentId, string expiryMonthYear, string preffered);
        Task<int> UpdatePreferedCardsAsync(long entityId, string type);

    }
}
