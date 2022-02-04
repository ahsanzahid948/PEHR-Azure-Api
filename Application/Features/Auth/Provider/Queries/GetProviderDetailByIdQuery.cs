using Application.DTOs.Auth.Provider;
using Application.Interfaces.Repositories.Auth;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.Provider.Queries
{
    public class GetProviderDetailByIdQuery : IRequest<Response<ProviderViewModel>>
    {
        public long ProviderId { get; set; }
        public GetProviderDetailByIdQuery(long providerId)
        {
            this.ProviderId = providerId;
        }
    }

    public class GetProviderDetailByIdQueryHandler : IRequestHandler<GetProviderDetailByIdQuery, Response<ProviderViewModel>>
    {
        private readonly IProviderRepositoryAsync _providerRepositry;
        private readonly IMapper _mapper;
        public GetProviderDetailByIdQueryHandler(IProviderRepositoryAsync providerRepositry, IMapper mapper)
        {
            _providerRepositry = providerRepositry;
            _mapper = mapper;
        }
        public async Task<Response<ProviderViewModel>> Handle(GetProviderDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var providerDetail = await _providerRepositry.GetProviderDetailByIdAsync(request.ProviderId);
            return new Response<ProviderViewModel>(_mapper.Map<ProviderViewModel>(providerDetail));
        }
    }
}
