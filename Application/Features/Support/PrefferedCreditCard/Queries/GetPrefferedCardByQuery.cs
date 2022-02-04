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

namespace Application.Features.Support.PrefferedCreditCard.Queries
{
    public class GetPrefferedCardByQuery : IRequest<Response<PaymentMethodViewModel>>
    {
        public long EntityId { get; set; }
        public string Type { get; set; }
        public GetPrefferedCardByQuery(long entityId, string type)
        {
            this.EntityId = entityId;
            this.Type = type;
        }
    }
    public class GetPrefferedCardByQueryHandler : IRequestHandler<GetPrefferedCardByQuery, Response<PaymentMethodViewModel>>
    {
        private readonly ISubscriptionRepositoryAsync _subscriptionRepository;
        private readonly IMapper _mapper;

        public GetPrefferedCardByQueryHandler(ISubscriptionRepositoryAsync subscriptionRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }
        public async Task<Response<PaymentMethodViewModel>> Handle(GetPrefferedCardByQuery request, CancellationToken cancellationToken)
        {
            var paymentMethodsList = await _subscriptionRepository.GetPrefferedCardAsync(request.EntityId, request.Type).ConfigureAwait(false);
            return new Response<PaymentMethodViewModel>(_mapper.Map<PaymentMethodViewModel>(paymentMethodsList));
        }
    }
}