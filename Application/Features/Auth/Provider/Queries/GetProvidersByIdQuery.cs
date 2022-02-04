using Application.DTOs.Auth.Provider;
using Application.DTOs.Common.PagingResponse;
using Application.Interfaces.Repositories.Auth;
using Application.Parameters;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.Provider.Queries
{
    public class GetProvidersByIdQuery : IRequest<Response<PageResponse<ProviderViewModel>>>
    {
        [Required]
        public long EntityId { get; set; }
        public string SortName { get; set; }
        public string SortOrder { get; set; }
        public RequestPageParameter PageParameter { get; set; }
    }

    public class GetProvidersByIdQueryHandler : IRequestHandler<GetProvidersByIdQuery, Response<PageResponse<ProviderViewModel>>>
    {
        private readonly IProviderRepositoryAsync _providerRepositry;
        private readonly IMapper _mapper;
        public GetProvidersByIdQueryHandler(IProviderRepositoryAsync providerRepositry, IMapper mapper)
        {
            _providerRepositry = providerRepositry;
            _mapper = mapper;
        }
        public async Task<Response<PageResponse<ProviderViewModel>>> Handle(GetProvidersByIdQuery request, CancellationToken cancellationToken)
        {
            var providerPageResponse = await _providerRepositry.GetProvidersByIdAsync(request.EntityId, request.SortName, request.SortOrder, request.PageParameter);
            var providerList = _mapper.Map<List<ProviderViewModel>>(providerPageResponse.Rows);
            return new Response<PageResponse<ProviderViewModel>>(new PageResponse<ProviderViewModel>(providerList, providerPageResponse.Total));
        }
    }
}
