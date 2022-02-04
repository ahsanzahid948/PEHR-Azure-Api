using Application.DTOs.Auth.ProviderAgent;
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

namespace Application.Features.Auth.ProviderAgent.Queries
{
    public class GetProviderAgentsByIdQuery : IRequest<Response<IReadOnlyList<ProviderAgentViewModel>>>
    {
        public long EntityId { get; set; }
        public long ProviderId { get; set; }
        public GetProviderAgentsByIdQuery(long entityId, long providerId)
        {
            this.EntityId = entityId;
            this.ProviderId = providerId;
        }
    }

    public class GetProviderAgentsByIdQueryHandler : IRequestHandler<GetProviderAgentsByIdQuery, Response<IReadOnlyList<ProviderAgentViewModel>>>
    {
        private readonly IEntityRepositoryAsync _entityRepositry;
        private readonly IMapper _mapper;
        public GetProviderAgentsByIdQueryHandler(IEntityRepositoryAsync entityRepository, IMapper mapper)
        {
            _entityRepositry = entityRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<ProviderAgentViewModel>>> Handle(GetProviderAgentsByIdQuery request, CancellationToken cancellationToken)
        {
            var providerAgetList = await _entityRepositry.GetProviderAgentsAsync(request.EntityId, request.ProviderId);
            return new Response<IReadOnlyList<ProviderAgentViewModel>>(_mapper.Map<IReadOnlyList<ProviderAgentViewModel>>(providerAgetList));
        }
    }
}
