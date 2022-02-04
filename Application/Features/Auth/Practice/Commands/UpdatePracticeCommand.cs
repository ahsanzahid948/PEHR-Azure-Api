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
    public class UpdatePracticeCommand : IRequest<Response<int>>
    {
        public string PracticeName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ZipCodeExt { get; set; }
        public string Speciality { get; set; }
        public string TaxId { get; set; }
        public string GroupNpi { get; set; }
        public string PhoneNo { get; set; }
        public string ProvidedFaxNo { get; set; }
        public string ProductType { get; set; }
        public long? NoOfProviders { get; set; }
        public string PreEhrVendor { get; set; }
        public string ImpConactName { get; set; }
        public string ImpConactPhone { get; set; }
        public string ImpConactEmail { get; set; }
        public string Migration { get; set; }
        public string EmailApptReminder { get; set; }
        public string CallSmsApptReminder { get; set; }
        public string DirectMail { get; set; }
        public string ElectronicPrescription { get; set; }
        public string ElectPatStatmentService { get; set; }
        public string LabInegration { get; set; }
        public string EpcsSetUp { get; set; }
        public virtual string CompanyLogo { get; set; }
        public string PrepopulatePatient { get; set; }
        public string GroupMCRPTAN { get; set; }
        public string GroupMCRDME { get; set; }
        public string GroupMCRRR { get; set; }
        public long? EntityId { get; set; }
        public string Comments { get; set; }
        public string MedcaidProvider { get; set; }
        public string BCBSProvider { get; set; }
        public string ProviderType { get; set; }
        public string EHRNotified { get; set; }

    }

    public class UpdatePracticeCommandHandler : IRequestHandler<UpdatePracticeCommand, Response<int>>
    {
        private readonly IPracticeRepositoryAsync _practiceRepository;
        private readonly IMapper _mapper;
        public UpdatePracticeCommandHandler(IPracticeRepositoryAsync practiceRepository, IMapper mapper)
        {
            _practiceRepository = practiceRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdatePracticeCommand request, CancellationToken cancellationToken)
        {
            var practice = _mapper.Map<Domain.Entities.Auth.Practice>(request);
            var userStatus = await _practiceRepository.UpdateAsync(practice).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}