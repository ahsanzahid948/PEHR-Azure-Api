using Application.DTOs.Auth.UserSetting;
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

namespace Application.Features.Auth.UserSetting.Queries
{
    public class GetUserSettingsByIdQuery : IRequest<Response<IReadOnlyList<UserSettingVewModel>>>
    {
        public string UserEmail { get; set; }
        public GetUserSettingsByIdQuery(string userEmail)
        {
            this.UserEmail = userEmail;
        }
    }
    public class GetUserSettingsByIdQueryHandler : IRequestHandler<GetUserSettingsByIdQuery, Response<IReadOnlyList<UserSettingVewModel>>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public GetUserSettingsByIdQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<UserSettingVewModel>>> Handle(GetUserSettingsByIdQuery request, CancellationToken cancellationToken)
        {
            var userSettingList = await _userRepository.GetUserSettingsByIdAsync(request.UserEmail).ConfigureAwait(false);
            return new Response<IReadOnlyList<UserSettingVewModel>>(_mapper.Map<IReadOnlyList<UserSettingVewModel>>(userSettingList));
        }

    }
}