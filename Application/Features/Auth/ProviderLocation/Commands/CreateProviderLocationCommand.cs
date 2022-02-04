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

namespace Application.Features.Auth.ProviderLocation.Commands
{
    public partial class CreateProviderLocationCommand : IRequest<Response<int>>
    {
        public virtual long? AuthEntityId { get; set; }
        public virtual string UserEmail { get; set; }
        public virtual long? ProviderId { get; set; }
        public virtual long? LocationId { get; set; }
    }

    public class CreateProviderLocationCommandHandler : IRequestHandler<CreateProviderLocationCommand, Response<int>>
    {
        private readonly IEntityRepositoryAsync _entityRepository;
        private readonly IMapper _mapper;
        public CreateProviderLocationCommandHandler(IEntityRepositoryAsync entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateProviderLocationCommand request, CancellationToken cancellationToken)
        {
            var providerLocation = _mapper.Map<Domain.Entities.Auth.ProviderLocation>(request);
            var userStatus = await _entityRepository.CreateProviderLocationAsync(providerLocation).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}
