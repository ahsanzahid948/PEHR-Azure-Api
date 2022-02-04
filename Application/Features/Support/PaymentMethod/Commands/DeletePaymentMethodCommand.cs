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
    public class DeletePaymentMethodCommand : IRequest<Response<int>>
    {
        public virtual long PaymentId { get; set; }
        public DeletePaymentMethodCommand(long paymentId)
        {
            this.PaymentId = paymentId;
        }
    }

    public class DeletePaymentMethodCommandHandler : IRequestHandler<DeletePaymentMethodCommand, Response<int>>
    {
        private readonly ISubscriptionRepositoryAsync _paymentMethodRepository;
        private readonly IMapper _mapper;

        public DeletePaymentMethodCommandHandler(ISubscriptionRepositoryAsync paymentMethodRepository, IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(DeletePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var userStatus = await _paymentMethodRepository.DeleteAsync(request.PaymentId).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}