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

namespace Application.Features.Auth.Users.Queries
{
    public class GetUserByQuery : IRequest<Response<UserViewModel>>
    {
        public string Email { get; set; }
        public string Token { get; set; }

        public GetUserByQuery(string email, string token)
        {
            this.Email = email;
            this.Token = token;
        }
    }

    public class GetUserByQueryHandler : IRequestHandler<GetUserByQuery, Response<UserViewModel>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public GetUserByQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Response<UserViewModel>> Handle(GetUserByQuery request, CancellationToken cancellationToken)
        {
            var userObject = await _userRepository.GetByFilterAsync(request.Email, request.Token).ConfigureAwait(false);
            return new Response<UserViewModel>(_mapper.Map<UserViewModel>(userObject));
        }
    }
}
