using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.DTOs.Entity;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Entity.Queries
{
    public class GetEntityByIdQuery : IRequest<Response<EntityViewModel>>
    {
        public long EntityId { get; set; }
        public string DBEntity { get; set; }

        public GetEntityByIdQuery(long entityId, string dbEntity)
        {
            this.EntityId = entityId;
            this.DBEntity = dbEntity;
        }
    }

    public class GetEntityByIdQueryHandler : IRequestHandler<GetEntityByIdQuery, Response<EntityViewModel>>
    {
        private readonly IEntityRepositoryAsync _entityRepository;
        private readonly IMapper _mapper;
        public GetEntityByIdQueryHandler(IEntityRepositoryAsync entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }
        public async Task<Response<EntityViewModel>> Handle(GetEntityByIdQuery request, CancellationToken cancellationToken)
        {
            var entityObject = await _entityRepository.GetByIdAsync(request.EntityId,request.DBEntity).ConfigureAwait(false);
            return new Response<EntityViewModel>(_mapper.Map<EntityViewModel>(entityObject));
        }
    }
}
