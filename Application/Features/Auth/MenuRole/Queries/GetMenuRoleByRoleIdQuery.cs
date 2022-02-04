using Application.DTOs.Auth.MenuRole;
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

namespace Application.Features.Auth.MenuRole.Queries
{
    public class GetMenuRoleByRoleIdQuery : IRequest<Response<MenuRoleViewModel>>
    {
        public long RoleId { get; set; }
        public string Name { get; set; }
        public GetMenuRoleByRoleIdQuery(long roleId, string name)
        {
            this.RoleId = roleId;
            this.Name = name;
        }
    }

    public class GetMenuRoleByRoleIdQueryHandler : IRequestHandler<GetMenuRoleByRoleIdQuery, Response<MenuRoleViewModel>>
    {
        private readonly IMenuRoleRepositoryAsync _menuRoleRepositry;
        private readonly IMapper _mapper;
        public GetMenuRoleByRoleIdQueryHandler(IMenuRoleRepositoryAsync menuRoleRepositry, IMapper mapper)
        {
            _menuRoleRepositry = menuRoleRepositry;
            _mapper = mapper;
        }

        public async Task<Response<MenuRoleViewModel>> Handle(GetMenuRoleByRoleIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Auth.MenuRole menuRole;
            if (string.IsNullOrEmpty(request.Name))
                menuRole = await _menuRoleRepositry.MenuRoleByRoleIdAsync(request.RoleId);
            else
                menuRole = await _menuRoleRepositry.MenuRoleByNameAsync(request.Name);

            return new Response<MenuRoleViewModel>(_mapper.Map<MenuRoleViewModel>(menuRole));
        }
    }
}
