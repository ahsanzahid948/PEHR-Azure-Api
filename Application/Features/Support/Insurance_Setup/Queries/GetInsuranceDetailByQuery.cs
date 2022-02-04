using Application.DTOs;
using Application.DTOs.Support.Insurance;
using Application.Interfaces.Repositories.Support;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Insurance_Setup.Queries
{
    public class GetInsuranceDetailByQuery : IRequest<Response<InsuranceViewModel>>
    {
        public long InsuranceId;
        public GetInsuranceDetailByQuery(long insuranceId)
        {
            InsuranceId = insuranceId;
        }
    }
    public class GetInsuranceDetailHandler : IRequestHandler<GetInsuranceDetailByQuery, Response<InsuranceViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly IInsuranceRepositoryAsync _insuranceRepository;
        public GetInsuranceDetailHandler(IMapper map, IInsuranceRepositoryAsync insuranceRepository)
        {
            _mapper = map;
            _insuranceRepository = insuranceRepository;
        }
        public async Task<Response<InsuranceViewModel>> Handle(GetInsuranceDetailByQuery request, CancellationToken cancellationToken)
        {
            var response = await _insuranceRepository.GetByIdAsync(request.InsuranceId).ConfigureAwait(false); ;
            return new Response<InsuranceViewModel>(_mapper.Map<InsuranceViewModel>(response));
        }
    }
}
