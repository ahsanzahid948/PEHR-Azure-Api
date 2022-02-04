using Application.DTOs.Support.PaymentInvoice;
using Application.Interfaces.Repositories.Support;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Invoice
{
    public class GetInvoicesByIdQuery : IRequest<Response<IReadOnlyList<PaymentInvoiceViewModel>>>
    {
        public long EntityId { get; set; }
        public GetInvoicesByIdQuery(long entityId)
        {
            this.EntityId = entityId;
        }
    }
    public class GetPaymentsIdQueryHandler : IRequestHandler<GetInvoicesByIdQuery, Response<IReadOnlyList<PaymentInvoiceViewModel>>>
    {
        private readonly ISubscriptionRepositoryAsync _paymentMethodRepository;
        private readonly IMapper _mapper;

        public GetPaymentsIdQueryHandler(ISubscriptionRepositoryAsync paymentMethodRepository, IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<PaymentInvoiceViewModel>>> Handle(GetInvoicesByIdQuery request, CancellationToken cancellationToken)
        {
            var paymentMethodsList = await _paymentMethodRepository.GetInvoicesByIdAsync(request.EntityId).ConfigureAwait(false);
            return new Response<IReadOnlyList<PaymentInvoiceViewModel>>(_mapper.Map<IReadOnlyList<PaymentInvoiceViewModel>>(paymentMethodsList));
        }
    }
}
