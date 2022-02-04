using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.Users.Commands
{
    public class UpdateUserPasswordCommand : IRequest<Response<int>>
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, Response<int>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserPasswordCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var userUpdateStatus = await _userRepository.UpdateUserPasswordByEmailAsync(request.Email, request.Password).ConfigureAwait(false);
            return new Response<int>(userUpdateStatus);
        }
    }
}