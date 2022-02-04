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

namespace Application.Features.Auth.EPCSSetting.Commands
{
    public class UpdateEPCSSettingCommand : IRequest<Response<int>>
    {
        public virtual string Email { get; set; }
        public virtual string ApproveEpcs { get; set; }
        public virtual string Licensekey { get; set; }
        public virtual string TokenRef { get; set; }
        public virtual string DefaultTokenType { get; set; }
        public virtual string EpcsRegistration { get; set; }
    }

    public class UpdateEPCSSettingCommandHandler : IRequestHandler<UpdateEPCSSettingCommand, Response<int>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public UpdateEPCSSettingCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<Response<int>> Handle(UpdateEPCSSettingCommand request, CancellationToken cancellationToken)
        {
            var epcsSetting = _mapper.Map<Domain.Entities.Auth.User>(request);
            var userStatus = await _userRepository.UpdateEPCSSettingAsync(epcsSetting).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}
