using Application.DTOs.Common.CreateRequestResponse;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.UserLog.Commands
{
    public class CreateUserLogCommand : IRequest<Response<CreateRequestResponse>>
    {
        public string Url { get; set; }
        public string IpAddress { get; set; }
        public string AppUser { get; set; }
        public string DbUser { get; set; }
        [DefaultValue("false")]
        public bool AuthSequence { get; set; }
    }

    public class CreateUserLogCommandHandler : IRequestHandler<CreateUserLogCommand, Response<CreateRequestResponse>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public CreateUserLogCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<CreateRequestResponse>> Handle(CreateUserLogCommand request, CancellationToken cancellationToken)
        {
            var userLog = _mapper.Map<AuditSession>(request);
            var userStatus = await _userRepository.AddUserLogAsync(userLog, request.AuthSequence).ConfigureAwait(false);
            return new Response<CreateRequestResponse>(userStatus);
        }
    }
}