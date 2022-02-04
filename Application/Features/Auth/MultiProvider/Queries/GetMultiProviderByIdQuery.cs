using Application.DTOs.Auth.MultiProvider;
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

namespace Application.Features.Auth.MultiProvider.Queries
{
    public class GetMultiProviderByIdQuery : IRequest<Response<IReadOnlyList<MultiProviderViewModel>>>
    {
        public long EntityId { get; set; }
        public long UserId { get; set; }
        public GetMultiProviderByIdQuery(long entityId, long userId)
        {
            this.EntityId = entityId;
            this.UserId = userId;
        }
    }

    public class GetMultiProviderByIdQueryHandler : IRequestHandler<GetMultiProviderByIdQuery, Response<IReadOnlyList<MultiProviderViewModel>>>
    {
        private readonly IEntityRepositoryAsync _entityRepository;
        private readonly IMapper _mapper;
        public GetMultiProviderByIdQueryHandler(IEntityRepositoryAsync entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<MultiProviderViewModel>>> Handle(GetMultiProviderByIdQuery request, CancellationToken cancellationToken)
        {
            var multiProviderList = await _entityRepository.GetMultiProviderAsync(request.EntityId, request.UserId).ConfigureAwait(false);
            return new Response<IReadOnlyList<MultiProviderViewModel>>(_mapper.Map<IReadOnlyList<MultiProviderViewModel>>(multiProviderList));
        }
    }
}
