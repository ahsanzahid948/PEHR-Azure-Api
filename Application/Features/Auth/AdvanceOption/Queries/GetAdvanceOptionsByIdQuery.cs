using Application.DTOs.Auth.AdvanceOption;
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

namespace Application.Features.Auth.AdvanceOption.Queries
{
    public class GetAdvanceOptionsByIdQuery : IRequest<Response<IReadOnlyList<AdvanceOptionViewModel>>>
    {
        public long EntityId { get; set; }
        public GetAdvanceOptionsByIdQuery(long entityId)
        {
            this.EntityId = entityId;
        }
    }
    public class GetAdvanceOptionsByIdQueryHandler : IRequestHandler<GetAdvanceOptionsByIdQuery, Response<IReadOnlyList<AdvanceOptionViewModel>>>
    {
        private readonly IEntityRepositoryAsync _entityRepository;
        private readonly IMapper _mapper;

        public GetAdvanceOptionsByIdQueryHandler(IEntityRepositoryAsync entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<AdvanceOptionViewModel>>> Handle(GetAdvanceOptionsByIdQuery request, CancellationToken cancellationToken)
        {
            var advanceOptionsList = await _entityRepository.GetAdvanceOptionsByIdAsync(request.EntityId).ConfigureAwait(false);
            return new Response<IReadOnlyList<AdvanceOptionViewModel>>(_mapper.Map<IReadOnlyList<AdvanceOptionViewModel>>(advanceOptionsList));
        }
    }
}
