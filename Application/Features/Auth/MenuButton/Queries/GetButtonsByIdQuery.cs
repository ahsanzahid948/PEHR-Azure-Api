using Application.DTOs.Auth.MenuButtons;
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

namespace Application.Features.Auth.MenuButton.Queries
{
    public class GetButtonsByIdQuery : IRequest<Response<IReadOnlyList<MenuButtonViewModel>>>
    {
        public string RoleIds { get; set; }
        public GetButtonsByIdQuery(string roleIds)
        {
            this.RoleIds = roleIds;
        }
    }

    public class GetButtonsByIdQueryHandler : IRequestHandler<GetButtonsByIdQuery, Response<IReadOnlyList<MenuButtonViewModel>>>
    {
        private readonly IMenuRoleRepositoryAsync _menuRoleRepositry;
        private readonly IMapper _mapper;
        public GetButtonsByIdQueryHandler(IMenuRoleRepositoryAsync menuRoleRepositry, IMapper mapper)
        {
            _menuRoleRepositry = menuRoleRepositry;
            _mapper = mapper;
        }


        public async Task<Response<IReadOnlyList<MenuButtonViewModel>>> Handle(GetButtonsByIdQuery request, CancellationToken cancellationToken)
        {
            var menuRoleList = await _menuRoleRepositry.GetMenuButtonsByIdsAsync(request.RoleIds);
            return new Response<IReadOnlyList<MenuButtonViewModel>>(_mapper.Map<IReadOnlyList<MenuButtonViewModel>>(menuRoleList));
        }
    }
}