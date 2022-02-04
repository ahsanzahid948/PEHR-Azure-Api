using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using Application.DTOs.DOCServerCredentials;
using System.Collections.Generic;

namespace Application.Features.Entity.Queries
{
    public class GetCredentialsQuery : IRequest<Response<IReadOnlyList<CredentialsViewModel>>>
    {
        public long EntityId { get; set; }
        public string Email { get; set; }
        public string AuthEntityId { get; set; }

        public GetCredentialsQuery(long entityId, string email, string authEntityId)
        {
            this.EntityId = entityId;
            this.Email = email;
            this.AuthEntityId = authEntityId;
        }
    }

    public class GetCredentialsQueryHandler : IRequestHandler<GetCredentialsQuery, Response<IReadOnlyList<CredentialsViewModel>>>
    {
        private readonly IEntityRepositoryAsync _entityRepository;
        private readonly IMapper _mapper;
        public GetCredentialsQueryHandler(IEntityRepositoryAsync entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<CredentialsViewModel>>> Handle(GetCredentialsQuery request, CancellationToken cancellationToken)
        {
            var entityObject = await _entityRepository.GetCredentialsAsync(request.EntityId, request.Email, request.AuthEntityId).ConfigureAwait(false);
            return new Response<IReadOnlyList<CredentialsViewModel>>(_mapper.Map<IReadOnlyList<CredentialsViewModel>>(entityObject));
        }
    }
}
