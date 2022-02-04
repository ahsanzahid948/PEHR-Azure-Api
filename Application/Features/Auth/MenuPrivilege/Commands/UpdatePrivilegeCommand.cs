using Application.DTOs.Auth.RoleAssignment;
using Application.Interfaces.Repositories.Auth;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.MenuPrivilege.Commands
{
    public class UpdatePrivilegeCommand : IRequest<Response<int>>
    {
        public List<PrivilegeModel> RoleAssignment { get; set; }

        public UpdatePrivilegeCommand(List<PrivilegeModel> roleAssignment)
        {
            this.RoleAssignment = roleAssignment;
        }

    }

    public class UpdatePrivilegeCommandHandler : IRequestHandler<UpdatePrivilegeCommand, Response<int>>
    {
        private readonly IMenuRoleRepositoryAsync _menuRoleRepositry;
        private readonly IMapper _mapper;
        public UpdatePrivilegeCommandHandler(IMenuRoleRepositoryAsync menuRoleRepositry, IMapper mapper)
        {
            _menuRoleRepositry = menuRoleRepositry;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdatePrivilegeCommand request, CancellationToken cancellationToken)
        {
            var menuRole = _mapper.Map<List<RoleAssignment>>(request.RoleAssignment);
            var userStatus = await _menuRoleRepositry.UpdatePrivilegesAsync(menuRole).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }

    public class PrivilegeModel
    {
        public virtual long AssignmentId { get; set; }
        public virtual long? RoleId { get; set; }
        public virtual long? MenuId { get; set; }
        public virtual long? TabMenuId { get; set; }
        public virtual long? MenuButtonId { get; set; }
        public virtual string Assignment { get; set; }
    }
}
