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
    public class UpdateUserTokenCommand : IRequest<Response<int>>
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }

    public class UpdateUserTokenCommandHandler : IRequestHandler<UpdateUserTokenCommand, Response<int>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;


        public UpdateUserTokenCommandHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateUserTokenCommand request, CancellationToken cancellationToken)
        {
            var userUpdateStatus = await _userRepository.UpdateUserTokenAsync(request.Email, request.Token).ConfigureAwait(false);
            return new Response<int>(userUpdateStatus);
        }
    }
}