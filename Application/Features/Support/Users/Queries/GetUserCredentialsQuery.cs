using Application.DTOs.User;
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

namespace Application.Features.Users.Queries
{
    public class GetUserCredentialsQuery : IRequest<Response<SupportUserViewModel>>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public GetUserCredentialsQuery(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
    }
    public class GetUserCredentialsByQueryHandler : IRequestHandler<GetUserCredentialsQuery, Response<SupportUserViewModel>>
    {
        private readonly ISupportUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public GetUserCredentialsByQueryHandler(ISupportUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<SupportUserViewModel>> Handle(GetUserCredentialsQuery request, CancellationToken cancellationToken)
        {
            var userObject = await _userRepository.UserLoginAsync(request.Email, request.Password);  //.GetUserAsync(request.Email, request.Password).ConfigureAwait(false);


            return new Response<SupportUserViewModel>(_mapper.Map<SupportUserViewModel>(userObject));
        }
    }
}
