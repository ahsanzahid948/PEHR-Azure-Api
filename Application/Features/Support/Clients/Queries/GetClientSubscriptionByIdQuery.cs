using Application.DTOs.Support.ClientSubscription;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Clients.Queries
{
    public class GetClientSubscriptionByIdQuery : IRequest<Response<ClientSubscriptionViewModel>>
    {
        public long EntityId;
        public GetClientSubscriptionByIdQuery(long entityId)
        {
            EntityId = entityId;
        }
    }
    public class GetClientSubscriptionByIdQueryHandler : IRequestHandler<GetClientSubscriptionByIdQuery, Response<ClientSubscriptionViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IClientRepositoryAsync _clientRepository;
        public GetClientSubscriptionByIdQueryHandler(IMapper map, IClientRepositoryAsync clientRepository)
        {
            _mapper = map;
            _clientRepository = clientRepository;
        }
        public async Task<Response<ClientSubscriptionViewModel>> Handle(GetClientSubscriptionByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _clientRepository.GetClientSubscriptionById(request.EntityId).ConfigureAwait(false);
            return new Response<ClientSubscriptionViewModel>(_mapper.Map<ClientSubscriptionViewModel>(response));
        }
    }
}
