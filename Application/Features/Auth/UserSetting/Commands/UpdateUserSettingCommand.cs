using Application.DTOs.Auth.UserSetting;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.UserSetting.Commands
{
    public class UpdateUserSettingCommand : IRequest<Response<int>>
    {
        public virtual string Email { get; set; }
        public virtual UserSettingVewModel UserSetting { get; set; }

        public UpdateUserSettingCommand(UserSettingVewModel userSetting, string email)
        {
            UserSetting = userSetting;
            Email = email;
        }
    }

    public class UpdateUserSettingCommandHandler : IRequestHandler<UpdateUserSettingCommand, Response<int>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserSettingCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;

        }
        public async Task<Response<int>> Handle(UpdateUserSettingCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.UserSetting);
            user.Email = request.Email.ToUpper();
            var userStatus = await _userRepository.UpdateUserSettingAsync(user).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }

}