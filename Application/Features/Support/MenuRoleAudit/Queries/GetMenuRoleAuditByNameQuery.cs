using Application.DTOs.Support.MenuRoleAudit;
using Application.Interfaces.Repositories.Support;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.MenuRoleAudit.Queries
{
    public class GetMenuRoleAuditByNameQuery : IRequest<Response<IReadOnlyList<MenuRoleAuditViewModel>>>
    {
        public string RoleName { get; set; }
        public GetMenuRoleAuditByNameQuery(string roleName)
        {
            this.RoleName = roleName;
        }
    }

    public class GetMenuRoleAuditByNameQueryHandler : IRequestHandler<GetMenuRoleAuditByNameQuery, Response<IReadOnlyList<MenuRoleAuditViewModel>>>
    {
        private readonly IAuditRepositoryAsync _auditRepositoryAsync;
        private readonly IMapper _mapper;
        public GetMenuRoleAuditByNameQueryHandler(IAuditRepositoryAsync auditRepositoryAsync, IMapper mapper)
        {
            _auditRepositoryAsync = auditRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<MenuRoleAuditViewModel>>> Handle(GetMenuRoleAuditByNameQuery request, CancellationToken cancellationToken)
        {
            var auditList = await _auditRepositoryAsync.GetMenuRoleAuditAsync(request.RoleName);
            return new Response<IReadOnlyList<MenuRoleAuditViewModel>>(_mapper.Map<IReadOnlyList<MenuRoleAuditViewModel>>(auditList));
        }
    }
}
