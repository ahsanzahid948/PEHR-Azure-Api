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

namespace Application.Features.Auth.ProviderAgent.Commands
{
    public class CreateProviderAgentCommand : IRequest<Response<int>>
    {
        public virtual long? AuthEntityId { get; set; }
        public virtual long ProviderId { get; set; }
        public virtual long? UserId { get; set; }
    }

    public class CreateMultiProviderCommandHandler : IRequestHandler<CreateProviderAgentCommand, Response<int>>
    {
        private readonly IEntityRepositoryAsync _entityRepository;
        private readonly IMapper _mapper;
        public CreateMultiProviderCommandHandler(IEntityRepositoryAsync entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }


        public async Task<Response<int>> Handle(CreateProviderAgentCommand request, CancellationToken cancellationToken)
        {
            var createProviderAgent = _mapper.Map<Domain.Entities.Auth.ProviderAgent>(request);
            var userStatus = await _entityRepository.CreateProviderAgentAsync(createProviderAgent).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}
