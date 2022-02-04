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

namespace Application.Features.Support.Insurance_Setup.Commands
{
    public class DeleteInsuranceCommand : IRequest<Response<int>>
    {
        public long InsuranceId { get; set; }
        public DeleteInsuranceCommand(long insuranceId)
        {
            InsuranceId = insuranceId;
        }
    }
    public class DeleteInsuranceCommandHandler : IRequestHandler<DeleteInsuranceCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly IInsuranceRepositoryAsync _insuranceRepository;

        public DeleteInsuranceCommandHandler(IMapper map, IInsuranceRepositoryAsync insuranceRepository)
        {
            _mapper = map;
            _insuranceRepository = insuranceRepository;
        }
        public async Task<Response<int>> Handle(DeleteInsuranceCommand request, CancellationToken cancellationToken)
        {
            var response = await _insuranceRepository.DeleteAsync(request.InsuranceId).ConfigureAwait(false);
            return new Response<int>(_mapper.Map<int>(response));
        }
    }
}