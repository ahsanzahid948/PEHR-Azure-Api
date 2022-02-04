using Application.DTOs.Auth.EntityUser;
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

namespace Application.Features.Auth.EntityUser.Queries
{
    public class GetEntityUsersByQuery : IRequest<Response<IReadOnlyList<EntityUserViewModel>>>
    {
        public string EntityId { get; set; }
        public string Active { get; set; }
        public GetEntityUsersByQuery(string entityId, string active)
        {
            this.EntityId = entityId;
            this.Active = active;
        }
    }
    public class GetEntityUsersByQueryHandler : IRequestHandler<GetEntityUsersByQuery, Response<IReadOnlyList<EntityUserViewModel>>>
    {
        private readonly IEntityRepositoryAsync _entityRepository;
        private readonly IMapper _mapper;

        public GetEntityUsersByQueryHandler(IEntityRepositoryAsync entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<EntityUserViewModel>>> Handle(GetEntityUsersByQuery request, CancellationToken cancellationToken)
        {
            var advanceOptionsList = await _entityRepository.GetEntityUsersByQueryAsync(request.EntityId, request.Active).ConfigureAwait(false);
            return new Response<IReadOnlyList<EntityUserViewModel>>(_mapper.Map<IReadOnlyList<EntityUserViewModel>>(advanceOptionsList));
        }
    }
}