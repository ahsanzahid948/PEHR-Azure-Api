using Application.DTOs.Support.PaymentMethod;
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
    public class GetPaymentMethodsByIdQuery : IRequest<Response<IReadOnlyList<PaymentMethodViewModel>>>
    {
        public long EntityId { get; set; }
        public string Type { get; set; }
        public GetPaymentMethodsByIdQuery(long entityId, string type)
        {
            this.EntityId = entityId;
            this.Type = type;
        }
    }
    public class GetCustomReportsByIdQueryHandler : IRequestHandler<GetPaymentMethodsByIdQuery, Response<IReadOnlyList<PaymentMethodViewModel>>>
    {
        private readonly ISubscriptionRepositoryAsync _subscriptionRepository;
        private readonly IMapper _mapper;

        public GetCustomReportsByIdQueryHandler(ISubscriptionRepositoryAsync subscriptionRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<PaymentMethodViewModel>>> Handle(GetPaymentMethodsByIdQuery request, CancellationToken cancellationToken)
        {
            var paymentMethodsList = await _subscriptionRepository.GetPaymentMethodsByIdAsync(request.EntityId, request.Type).ConfigureAwait(false);
            return new Response<IReadOnlyList<PaymentMethodViewModel>>(_mapper.Map<IReadOnlyList<PaymentMethodViewModel>>(paymentMethodsList));
        }
    }
}
