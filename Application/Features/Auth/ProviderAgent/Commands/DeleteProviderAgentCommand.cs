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
    public class DeleteProviderAgentCommand : IRequest<Response<int>>
    {
        public virtual long AuthEntityId { get; set; }
        public virtual long ProviderId { get; set; }

        public DeleteProviderAgentCommand(long authEntityId, long providerId)
        {
            this.AuthEntityId = authEntityId;
            this.ProviderId = providerId;
        }
    }

    public class DeleteProviderAgentCommandHandler : IRequestHandler<DeleteProviderAgentCommand, Response<int>>
    {
        private readonly IEntityRepositoryAsync _entityRepository;
        private readonly IMapper _mapper;
        public DeleteProviderAgentCommandHandler(IEntityRepositoryAsync entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(DeleteProviderAgentCommand request, CancellationToken cancellationToken)
        {
            var userStatus = await _entityRepository.DeleteProviderAgentAsync(request.AuthEntityId, request.ProviderId).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}