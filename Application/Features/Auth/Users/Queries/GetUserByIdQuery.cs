using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;

namespace Application.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<Response<UserViewModel>>
    {
        public long UserId { get; set; }

        public GetUserByIdQuery(long userId)
        {
            this.UserId = userId;
        }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<UserViewModel>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public GetUserByIdQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Response<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var userObject = await _userRepository.GetByIdAsync(request.UserId).ConfigureAwait(false);
            return new Response<UserViewModel>(_mapper.Map<UserViewModel>(userObject));
        }
    }
}