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

namespace Application.Features.Auth.MultiProvider.Commands
{
    public class DeleteMultiProviderCommand : IRequest<Response<int>>
    {
        public virtual long? ProviderId { get; set; }
        public virtual long? UserId { get; set; }
        public virtual long? AuthEntityId { get; set; }
    }

    public class DeleteMultiProviderCommandHandler : IRequestHandler<DeleteMultiProviderCommand, Response<int>>
    {
        private readonly IEntityRepositoryAsync _entityRepository;
        private readonly IMapper _mapper;
        public DeleteMultiProviderCommandHandler(IEntityRepositoryAsync entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(DeleteMultiProviderCommand request, CancellationToken cancellationToken)
        {
            var multiProvider = _mapper.Map<Domain.Entities.Auth.MultiProvider>(request);
            var userStatus = await _entityRepository.DeleteMultiProviderAsync(multiProvider).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}