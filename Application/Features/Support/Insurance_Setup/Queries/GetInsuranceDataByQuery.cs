using Application.DTOs;
using Application.DTOs.Common.PagingResponse;
using Application.DTOs.Support.Insurance;
using Application.Interfaces.Repositories.Support;
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

namespace Application.Features.Support.Insurance_Setup.Queries
{
    public class GetInsuranceDataByQuery : IRequest<Response<PageResponse<InsuranceViewModel>>>
    {
        [Required]
        public long EntityId { get; set; }
        public string SortName { get; set; }
        public string SortOrder { get; set; }
        public RequestPageParameter PageParameter { get; set; }
    }
    public class GetInsuranceDataHandler : IRequestHandler<GetInsuranceDataByQuery, Response<PageResponse<InsuranceViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly IInsuranceRepositoryAsync _insuranceRepository;

        public GetInsuranceDataHandler(IMapper map, IInsuranceRepositoryAsync insuranceRepository)
        {
            _mapper = map;
            _insuranceRepository = insuranceRepository;
        }
        public async Task<Response<PageResponse<InsuranceViewModel>>> Handle(GetInsuranceDataByQuery request, CancellationToken cancellationToken)
        {
            var response = await _insuranceRepository.GetInsuranceAsync(request.EntityId, request.SortName, request.SortOrder, request.PageParameter).ConfigureAwait(false);
            var insuranceList = _mapper.Map<List<InsuranceViewModel>>(response.Rows);
            return new Response<PageResponse<InsuranceViewModel>>(new PageResponse<InsuranceViewModel>(insuranceList, response.Total));
        }
    }
}
