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

namespace Application.Features.Support.PaymentMethod.Commands
{
    public class CreatePaymentMethodCommand : IRequest<Response<int>>
    {
        public virtual long? ClientId { get; set; }
        public virtual long? AuthEntityId { get; set; }
        public virtual string CreditCardNo { get; set; }
        public virtual string ExpiryMonthYear { get; set; }
        public virtual string CardType { get; set; }
        public virtual string PayerId { get; set; }
        public virtual string Preferred { get; set; }
        public virtual string Type { get; set; }
    }

    public class CreatePaymentMethodCommandHandler : IRequestHandler<CreatePaymentMethodCommand, Response<int>>
    {
        private readonly ISubscriptionRepositoryAsync _paymentMethodRepository;
        private readonly IMapper _mapper;

        public CreatePaymentMethodCommandHandler(ISubscriptionRepositoryAsync paymentMethodRepository, IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var paymentMethod = _mapper.Map<Domain.Entities.Support.PaymentMethod>(request);
            var userStatus = await _paymentMethodRepository.CreatePaymentMethodAsync(paymentMethod).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}