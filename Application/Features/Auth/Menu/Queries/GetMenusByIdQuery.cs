using Application.DTOs.Auth.Menu;
using Application.Interfaces.Repositories.Auth;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.Menu.Queries
{
    public class GetMenusByIdQuery : IRequest<Response<IReadOnlyList<MenuViewModel>>>
    {
        public string RoleIds { get; set; }
        public GetMenusByIdQuery(string roleIds)
        {
            this.RoleIds = roleIds;
        }
    }

    public class GetMenusByIdQueryHandler : IRequestHandler<GetMenusByIdQuery, Response<IReadOnlyList<MenuViewModel>>>
    {
        private readonly IMenuRoleRepositoryAsync _menuRoleRepositry;
        private readonly IMapper _mapper;
        public GetMenusByIdQueryHandler(IMenuRoleRepositoryAsync menuRoleRepositry, IMapper mapper)
        {
            _menuRoleRepositry = menuRoleRepositry;
            _mapper = mapper;
        }
        public async Task<Response<IReadOnlyList<MenuViewModel>>> Handle(GetMenusByIdQuery request, CancellationToken cancellationToken)
        {
            var menuRoleList = await _menuRoleRepositry.GetMenusByIdsAsync(request.RoleIds);
            return new Response<IReadOnlyList<MenuViewModel>>(_mapper.Map<IReadOnlyList<MenuViewModel>>(menuRoleList));
        }
    }
}
