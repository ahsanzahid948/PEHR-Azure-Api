using Application.Interfaces.Repositories.Support;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.PrefferedCreditCard.Commands
{
    public class UpdatePrefferedCardCommand : IRequest<Response<int>>
    {
        [Required]
        public virtual string PaymentId { get; set; }
        public virtual string ExpiryMonthYear { get; set; }
        public virtual string Preferred { get; set; }
    }

    public class UpdatePrefferedCardCommandHandler : IRequestHandler<UpdatePrefferedCardCommand, Response<int>>
    {
        private readonly ISubscriptionRepositoryAsync _paymentMethodRepository;
        private readonly IMapper _mapper;

        public UpdatePrefferedCardCommandHandler(ISubscriptionRepositoryAsync paymentMethodRepository, IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdatePrefferedCardCommand request, CancellationToken cancellationToken)
        {
            var userStatus = await _paymentMethodRepository.UpdatePrefferedCardAsync(request.PaymentId, request.ExpiryMonthYear, request.Preferred).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}
