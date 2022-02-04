using Application.DTOs.Support.LicencePayment;
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

namespace Application.Features.Support.PaymentMethod.Queries
{
    public class GetPaymentsByIdQuery : IRequest<Response<IReadOnlyList<LicencePaymentViewModel>>>
    {
        public long EntityId { get; set; }
        public string ClientId { get; set; }
        public GetPaymentsByIdQuery(long entityId, string clientId)
        {
            this.EntityId = entityId;
            this.ClientId = clientId;
        }
    }
    public class GetPaymentsIdQueryHandler : IRequestHandler<GetPaymentsByIdQuery, Response<IReadOnlyList<LicencePaymentViewModel>>>
    {
        private readonly ISubscriptionRepositoryAsync _paymentMethodRepository;
        private readonly IMapper _mapper;

        public GetPaymentsIdQueryHandler(ISubscriptionRepositoryAsync paymentMethodRepository, IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<LicencePaymentViewModel>>> Handle(GetPaymentsByIdQuery request, CancellationToken cancellationToken)
        {
            var paymentMethodsList = await _paymentMethodRepository.GetPaymentsByIdAsync(request.EntityId, request.ClientId).ConfigureAwait(false);
            return new Response<IReadOnlyList<LicencePaymentViewModel>>(_mapper.Map<IReadOnlyList<LicencePaymentViewModel>>(paymentMethodsList));
        }
    }
}
