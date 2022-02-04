using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using Application.DTOs.EntityConfigurations;

namespace Application.Features.Entity.Queries
{
    public class GetConfigurationsByIdQuery : IRequest<Response<EntityConfigurationViewModel>>
    {
        public long EntityId { get; set; }

        public GetConfigurationsByIdQuery(long entityId)
        {
            this.EntityId = entityId;
        }
    }

    public class GetConfigurationsByIdQueryHandler : IRequestHandler<GetConfigurationsByIdQuery, Response<EntityConfigurationViewModel>>
    {
        private readonly IEntityRepositoryAsync _entityRepository;
        private readonly IMapper _mapper;
        public GetConfigurationsByIdQueryHandler(IEntityRepositoryAsync entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }
        public async Task<Response<EntityConfigurationViewModel>> Handle(GetConfigurationsByIdQuery request, CancellationToken cancellationToken)
        {
            var entityObject = await _entityRepository.GetConfigurationsByIdAsync(request.EntityId).ConfigureAwait(false);
            return new Response<EntityConfigurationViewModel>(_mapper.Map<EntityConfigurationViewModel>(entityObject));
        }
    }
}
