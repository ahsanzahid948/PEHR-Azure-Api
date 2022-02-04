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
    public class CreatePrivilegeCommand : IRequest<Response<int>>
    {
        public List<PrivilegeModel> RoleAssignment { get; set; }

        public CreatePrivilegeCommand(List<PrivilegeModel> roleAssignment)
        {
            this.RoleAssignment = roleAssignment;
        }

    }

    public class CreatePrivilegeCommandHandler : IRequestHandler<CreatePrivilegeCommand, Response<int>>
    {
        private readonly IMenuRoleRepositoryAsync _menuRoleRepositry;
        private readonly IMapper _mapper;
        public CreatePrivilegeCommandHandler(IMenuRoleRepositoryAsync menuRoleRepositry, IMapper mapper)
        {
            _menuRoleRepositry = menuRoleRepositry;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreatePrivilegeCommand request, CancellationToken cancellationToken)
        {
            var menuRole = _mapper.Map<List<RoleAssignment>>(request.RoleAssignment);
            var userStatus = await _menuRoleRepositry.CreatePrivilegesAsync(menuRole).ConfigureAwait(false);
            return new Response<int>(userStatus);
        }
    }
}
