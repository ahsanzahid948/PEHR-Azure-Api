using Application.DTOs.Auth.UserProviderAgent;
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
    public class GetUserAgentsByIdQuery : IRequest<Response<IReadOnlyList<UserProviderAgentViewModel>>>
    {
        public long EntityId { get; set; }
        public long UserId { get; set; }
        public GetUserAgentsByIdQuery(long entityId, long userId)
        {
            this.EntityId = entityId;
            this.UserId = userId;
        }
    }
    public class GetUserAgentsByIdQueryHandler : IRequestHandler<GetUserAgentsByIdQuery, Response<IReadOnlyList<UserProviderAgentViewModel>>>
    {
        private readonly IEntityRepositoryAsync _entityRepository;
        private readonly IMapper _mapper;
        public GetUserAgentsByIdQueryHandler(IEntityRepositoryAsync entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<UserProviderAgentViewModel>>> Handle(GetUserAgentsByIdQuery request, CancellationToken cancellationToken)
        {
            var userAgentList = await _entityRepository.GetUserAgentsAsync(request.EntityId, request.UserId);           
            return new Response<IReadOnlyList<UserProviderAgentViewModel>>(_mapper.Map<IReadOnlyList<UserProviderAgentViewModel>>(userAgentList));
        }
    }
}
