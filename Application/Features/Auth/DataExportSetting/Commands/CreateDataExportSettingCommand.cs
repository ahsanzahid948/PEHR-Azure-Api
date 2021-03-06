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

namespace Application.Features.Auth.DataExportSetting.Commands
{
    public class CreateDataExportSettingCommand : IRequest<Response<int>>
    {
        public virtual long? UserId { get; set; }
        public virtual long? AuthEntityId { get; set; }
        public virtual DateTime? DateFrom { get; set; }
        public virtual DateTime? DateTo { get; set; }
        public virtual string ExportType { get; set; }
        public virtual string RelativeExportType { get; set; }
        public virtual long? RelativeExportValue { get; set; }
        public virtual DateTime? SpecificDate { get; set; }
        public virtual string ExportTime { get; set; }
        public virtual DateTime? EntryDate { get; set; }
        public virtual string AccountNum { get; set; }
        public virtual string AllPatient { get; set; }
        public virtual string Status { get; set; }
    }

    public class CreateDataExportSettingCommandHandler : IRequestHandler<CreateDataExportSettingCommand, Response<int>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public CreateDataExportSettingCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateDataExportSettingCommand request, CancellationToken cancellationToken)
        {
            var epcsSetting = _mapper.Map<Domain.Entities.Auth.DataExportSetting>(request);
            var userStatus = await _userRepository.CreateDataExportSettingAsync(epcsSetting).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}
