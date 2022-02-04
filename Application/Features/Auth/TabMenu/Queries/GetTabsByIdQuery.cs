using Application.DTOs.Auth.TabMenu;
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

namespace Application.Features.Auth.TabMenu.Queries
{
    public class GetTabsByIdQuery : IRequest<Response<IReadOnlyList<TabMenuViewModel>>>
    {
        public string RoleIds { get; set; }
        public GetTabsByIdQuery(string roleIds)
        {
            this.RoleIds = roleIds;
        }
    }

    public class GetTabsByIdQueryHandler : IRequestHandler<GetTabsByIdQuery, Response<IReadOnlyList<TabMenuViewModel>>>
    {
        private readonly IMenuRoleRepositoryAsync _menuRoleRepositry;
        private readonly IMapper _mapper;
        public GetTabsByIdQueryHandler(IMenuRoleRepositoryAsync menuRoleRepositry, IMapper mapper)
        {
            _menuRoleRepositry = menuRoleRepositry;
            _mapper = mapper;
        }


        public async Task<Response<IReadOnlyList<TabMenuViewModel>>> Handle(GetTabsByIdQuery request, CancellationToken cancellationToken)
        {
            var menuRoleList = await _menuRoleRepositry.GetMenuTabsByIdsAsync(request.RoleIds);
            return new Response<IReadOnlyList<TabMenuViewModel>>(_mapper.Map<IReadOnlyList<TabMenuViewModel>>(menuRoleList));
        }
    }
}
