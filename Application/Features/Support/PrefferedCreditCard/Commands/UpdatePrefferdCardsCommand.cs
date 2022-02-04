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
    public class UpdatePrefferdCardsCommand : IRequest<Response<int>>
    {
        public long EntityId { get; set; }

        public string Type { get; set; }

        public UpdatePrefferdCardsCommand(long entityId, string type)
        {
            this.EntityId = entityId;
            this.Type = type;
        }
    }

    public class UpdatePrefferdCardsCommandHandler : IRequestHandler<UpdatePrefferdCardsCommand, Response<int>>
    {
        private readonly ISubscriptionRepositoryAsync _paymentMethodRepository;
        private readonly IMapper _mapper;

        public UpdatePrefferdCardsCommandHandler(ISubscriptionRepositoryAsync paymentMethodRepository, IMapper mapper)
        {
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdatePrefferdCardsCommand request, CancellationToken cancellationToken)
        {
            var userStatus = await _paymentMethodRepository.UpdatePreferedCardsAsync(request.EntityId, request.Type).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}

