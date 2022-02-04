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
    public class CreatePaymentCommand : IRequest<Response<int>>
    {
        public virtual long? ClientId { get; set; }
        public virtual long? AuthEntityId { get; set; }
        public virtual double? AmountPaid { get; set; }
        public virtual string TransactionId { get; set; }
        public virtual long? InvoiceId { get; set; }
        public virtual string Type { get; set; }
        public virtual string PaymentMethod { get; set; }
        public virtual string CreditCardNo { get; set; }
        public virtual string CheckNumber { get; set; }
    }

    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Response<int>>
    {
        private readonly ISubscriptionRepositoryAsync _paymentMethodRepository;
        private readonly IMapper _mapper;

        public CreatePaymentCommandHandler(ISubscriptionRepositoryAsync paymentMethodRepository, IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var licencePayment = _mapper.Map<Domain.Entities.Support.LicencePayment>(request);
            var userStatus = await _paymentMethodRepository.CreatePaymentAsync(licencePayment).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}