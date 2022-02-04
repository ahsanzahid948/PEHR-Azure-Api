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
    public class DeleteDataExportSettingCommand : IRequest<Response<int>>
    {
        public virtual long SettingId { get; set; }

        public DeleteDataExportSettingCommand(long settingId)
        {
            this.SettingId = settingId;
        }

    }

    public class DeleteDataExportSettingCommandHandler : IRequestHandler<DeleteDataExportSettingCommand, Response<int>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public DeleteDataExportSettingCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(DeleteDataExportSettingCommand request, CancellationToken cancellationToken)
        {
            var userStatus = await _userRepository.DeleteDataExportSettingAsync(request.SettingId).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}