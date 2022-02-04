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
    public class CreateMultiProviderCommand : IRequest<Response<int>>
    {
       public List<MultiProviderModel> MultiProvider { get; set; }
        public CreateMultiProviderCommand(List<MultiProviderModel> multiProvider)
        {
            this.MultiProvider = multiProvider;
        }
    }

    public class CreateMultiProviderCommandHandler : IRequestHandler<CreateMultiProviderCommand, Response<int>>
    {
        private readonly IEntityRepositoryAsync _entityRepository;
        private readonly IMapper _mapper;
        public CreateMultiProviderCommandHandler(IEntityRepositoryAsync entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateMultiProviderCommand request, CancellationToken cancellationToken)
        {
            var multiProvider = _mapper.Map<List<Domain.Entities.Auth.MultiProvider>>(request.MultiProvider);
            var userStatus = await _entityRepository.CreateMultiProviderAsync(multiProvider).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }

    public class MultiProviderModel
    {
        public virtual long? ProviderId { get; set; }
        public virtual long? UserId { get; set; }
        public virtual long? AuthEntityId { get; set; }
    }
}
   