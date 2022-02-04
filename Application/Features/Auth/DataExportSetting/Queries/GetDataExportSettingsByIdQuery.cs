using Application.DTOs.Auth.DataExportSetting;
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

namespace Application.Features.Auth.DataExportSetting.Queries
{
    public class GetDataExportSettingsByIdQuery : IRequest<Response<IReadOnlyList<DataExportSettingViewModel>>>
    {
        public long UserId { get; set; }
        public GetDataExportSettingsByIdQuery(long userId)
        {
            this.UserId = userId;
        }
    }
    public class GetDataExportSettingsByIdQueryHandler : IRequestHandler<GetDataExportSettingsByIdQuery, Response<IReadOnlyList<DataExportSettingViewModel>>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public GetDataExportSettingsByIdQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<DataExportSettingViewModel>>> Handle(GetDataExportSettingsByIdQuery request, CancellationToken cancellationToken)
        {
            var userSettingList = await _userRepository.GetDataExportSettingsByIdAsync(request.UserId).ConfigureAwait(false);
            return new Response<IReadOnlyList<DataExportSettingViewModel>>(_mapper.Map<IReadOnlyList<DataExportSettingViewModel>>(userSettingList));
        }

    }
}
