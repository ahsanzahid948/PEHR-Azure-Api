using Application.DTOs.Auth.UserEntities;
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

namespace Application.Features.Auth.UserEntities.Queries
{
    public class GetUserEntitiesByQuery : IRequest<Response<IReadOnlyList<UserEntitiesViewModel>>>
    {
        public string Email { get; set; }
        public string Epcs { get; set; }
        public string ApproveEpcs { get; set; }
        public string AuthEntityID { get; set; }
        public GetUserEntitiesByQuery(string email, string epcs, string approveEpcs, string authEntityID)
        {
            this.Email = email;
            this.Epcs = epcs;
            this.ApproveEpcs = approveEpcs;
            this.AuthEntityID = authEntityID;
        }
    }

    public class GetUserEntitiesByQueryHandler : IRequestHandler<GetUserEntitiesByQuery, Response<IReadOnlyList<UserEntitiesViewModel>>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public GetUserEntitiesByQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<UserEntitiesViewModel>>> Handle(GetUserEntitiesByQuery request, CancellationToken cancellationToken)
        {
            var userObject = await _userRepository.GetUsersEntitesAsync(request.Email, request.Epcs, request.ApproveEpcs, request.AuthEntityID).ConfigureAwait(false);
            return new Response<IReadOnlyList<UserEntitiesViewModel>>(_mapper.Map<IReadOnlyList<UserEntitiesViewModel>>(userObject));
        }
    }
}