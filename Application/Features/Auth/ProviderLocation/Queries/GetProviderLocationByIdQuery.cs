using Application.DTOs.Auth.ProviderLocation;
using Application.Features.Entity.Queries;
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

namespace Application.Features.Auth.ProviderLocation.Queries
{
    public class GetProviderLocationByIdQuery : IRequest<Response<IReadOnlyList<ProviderLocationViewModel>>>
    {
        public long EntityId { get; set; }
        public string Email { get; set; }
        public GetProviderLocationByIdQuery(long entityId, string email)
        {
            this.EntityId = entityId;
            this.Email = email;
        }
    }
    public class GetProviderLocationByIdQueryHandler : IRequestHandler<GetProviderLocationByIdQuery, Response<IReadOnlyList<ProviderLocationViewModel>>>
    {
        private readonly IEntityRepositoryAsync _entityRepository;
        private readonly IMapper _mapper;
        public GetProviderLocationByIdQueryHandler(IEntityRepositoryAsync entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<ProviderLocationViewModel>>> Handle(GetProviderLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var providerLocationList = (await _entityRepository.GetProviderLocationAsync(request.EntityId, request.Email).ConfigureAwait(false));
            return new Response<IReadOnlyList<ProviderLocationViewModel>>(_mapper.Map<IReadOnlyList<ProviderLocationViewModel>>(providerLocationList));
        }
    }
}
