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
    public class GetUserProviderCountByQuery : IRequest<Response<int>>
    {
        public string Email { get; set; }
        public long ProviderId { get; set; }

        public GetUserProviderCountByQuery(string email, long providerId)
        {
            this.Email = email;
            this.ProviderId = providerId;
        }
    }

    public class GetUserProviderCountByQueryHandler : IRequestHandler<GetUserProviderCountByQuery, Response<int>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public GetUserProviderCountByQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(GetUserProviderCountByQuery request, CancellationToken cancellationToken)
        {
            var userObject = await _userRepository.GetUserProviderCountAsync(request.Email, request.ProviderId).ConfigureAwait(false);
            return new Response<int>(_mapper.Map<int>(userObject));
        }
    }
}