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
    public class UpdateInsuranceCommand : IRequest<Response<int>>
    {
        public long InsuranceId { get; set; }
        public string InsuranceName { get; set; }
        public string PayorId { get; set; }
        public string ShortName { get; set; }
        public string Plantype { get; set; }
        public string Comments { get; set; }
        public string BcBsFlag { get; set; }
        public string Medicare { get; set; }
        public string MedicaId { get; set; }
        public string Commercial { get; set; }
        public string Other { get; set; }
    }
    public class UpdateInsuranceCommandHandler : IRequestHandler<UpdateInsuranceCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly IInsuranceRepositoryAsync _insuranceRepository;

        public UpdateInsuranceCommandHandler(IMapper map, IInsuranceRepositoryAsync insuranceRepository)
        {
            _mapper = map;
            _insuranceRepository = insuranceRepository;
        }
        public async Task<Response<int>> Handle(UpdateInsuranceCommand request, CancellationToken cancellationToken)
        {
            var insurance = _mapper.Map<Domain.Entities.Support.Insurance>(request);
            var response = await _insuranceRepository.UpdateAsync(insurance).ConfigureAwait(false);
            return new Response<int>(_mapper.Map<int>(response));
        }
    }
}
