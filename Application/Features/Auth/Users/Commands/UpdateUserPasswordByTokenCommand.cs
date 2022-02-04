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
    public class UpdateUserPasswordByTokenCommand : IRequest<Response<int>>
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UpdateUserPasswordByTokenCommandHandler : IRequestHandler<UpdateUserPasswordByTokenCommand, Response<int>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;


        public UpdateUserPasswordByTokenCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateUserPasswordByTokenCommand request, CancellationToken cancellationToken)
        {
            var userUpdateStatus = await _userRepository.UpdateUserPasswordAsync(request.Token, request.Password).ConfigureAwait(false);
            return new Response<int>(userUpdateStatus);
        }
    }
}