using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Application.DTOs.User;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Entity.Queries
{
    public class GetEntityUsersByIdQuery : IRequest<Response<IEnumerable<UserViewModel>>>
    {

        public string EntityId { get; set; }
        public string Filter { get; set; }
        public string Token { get; set; }
        public string MenuRoleId { get; set; }
        public GetEntityUsersByIdQuery(string entityId, string filter, string token, string menuRoleId)
        {
            this.EntityId = entityId;
            this.Filter = filter;
            this.Token = token;
            this.MenuRoleId = menuRoleId;
        }
    }

    public class GetEntityUsersByIdQueryHandler : IRequestHandler<GetEntityUsersByIdQuery, Response<IEnumerable<UserViewModel>>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;
        public GetEntityUsersByIdQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper = null)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<UserViewModel>>> Handle(GetEntityUsersByIdQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.Token))
            {
                var usersList = await _userRepository.GetUserbyTokeAsync(request.Token).ConfigureAwait(false);
                return new Response<IEnumerable<UserViewModel>>(_mapper.Map<IEnumerable<UserViewModel>>(usersList));
            }
            if (!string.IsNullOrEmpty(request.MenuRoleId))
            {
                var usersList = await _userRepository.GetUserbyMenuRoleAsync(request.MenuRoleId).ConfigureAwait(false);
                return new Response<IEnumerable<UserViewModel>>(_mapper.Map<IEnumerable<UserViewModel>>(usersList));
            }
            else
            {
                var usersList = await _userRepository.GetByEntityIdAsync(request.EntityId, request.Filter).ConfigureAwait(false);
                return new Response<IEnumerable<UserViewModel>>(_mapper.Map<IEnumerable<UserViewModel>>(usersList));
            }
            

        }
    }
}
