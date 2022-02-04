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

namespace Application.Features.Auth.Practice.Commands
{
    public class UpdatePracticePatchCommand : IRequest<Response<int>>
    {

        public string CompanyLogo { get; set; }
        public long EntityId { get; set; }
        public string NoInsuranceNeeded { get; set; }
        public bool RemoveLogo { get; set; }
        public UpdatePracticePatchCommand(long entityId, string companyLogo, string noInsuranceNeeded, bool removeLogo)
        {
            this.EntityId = entityId;
            this.CompanyLogo = companyLogo;
            this.NoInsuranceNeeded = noInsuranceNeeded;
            this.RemoveLogo = removeLogo;
        }

    }

    public class UpdatePracticePatchCommandHandler : IRequestHandler<UpdatePracticePatchCommand, Response<int>>
    {
        private readonly IPracticeRepositoryAsync _practiceRepository;
        private readonly IMapper _mapper;
        public UpdatePracticePatchCommandHandler(IPracticeRepositoryAsync practiceRepository, IMapper mapper)
        {
            _practiceRepository = practiceRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdatePracticePatchCommand request, CancellationToken cancellationToken)
        {
            var practice = await _practiceRepository.GetByIdAsync(request.EntityId).ConfigureAwait(false);
            if (practice != null)
            {
                if (request.RemoveLogo)
                    practice.Company_Logo = request.CompanyLogo;

                if (!string.IsNullOrEmpty(request.NoInsuranceNeeded))
                    practice.No_Insurance_Needed = request.NoInsuranceNeeded;

                var userStatus = await _practiceRepository.UpdatePracticePatchAsync(practice).ConfigureAwait(false);
                return new Response<int>(userStatus);
            }
            else
                throw new Exception("Practice doesnot exist");
        }
    }
}