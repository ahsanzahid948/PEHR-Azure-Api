using Application.DTOs.Auth.RoleAssignment;
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

namespace Application.Features.Auth.MenuPrivilege.Queries
{
    public class GetPrivilegesByIdQuery : IRequest<Response<IReadOnlyList<RoleAssignmentViewModel>>>
    {
        public long RoleId { get; set; }
        public GetPrivilegesByIdQuery(long roleId)
        {
            this.RoleId = roleId;
        }
    }

    public class GetPrivilegesByIdQueryHandler : IRequestHandler<GetPrivilegesByIdQuery, Response<IReadOnlyList<RoleAssignmentViewModel>>>
    {
        private readonly IMenuRoleRepositoryAsync _menuRoleRepositry;
        private readonly IMapper _mapper;
        public GetPrivilegesByIdQueryHandler(IMenuRoleRepositoryAsync menuRoleRepositry, IMapper mapper)
        {
            _menuRoleRepositry = menuRoleRepositry;
            _mapper = mapper;
        }

        public async  Task<Response<IReadOnlyList<RoleAssignmentViewModel>>> Handle(GetPrivilegesByIdQuery request, CancellationToken cancellationToken)
        {
            var menuRoleList = await _menuRoleRepositry.GetPrivilegesByIdAsync(request.RoleId);
            return new Response<IReadOnlyList<RoleAssignmentViewModel>>(_mapper.Map<IReadOnlyList<RoleAssignmentViewModel>>(menuRoleList));
        }
    }
}

