using Application.DTOs.Support.UserAudit;
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

namespace Application.Features.Support.UserAudit.Queries
{
    public class GetUserAuditByNameQuery : IRequest<Response<IReadOnlyList<UserAuditViewModel>>>
    {
        public string UserName { get; set; }
        public GetUserAuditByNameQuery(string userName)
        {
            this.UserName = userName;
        }
    }

    public class GetUserAuditByNameQueryHandler : IRequestHandler<GetUserAuditByNameQuery, Response<IReadOnlyList<UserAuditViewModel>>>
    {
        private readonly IAuditRepositoryAsync _auditRepositoryAsync;
        private readonly IMapper _mapper;
        public GetUserAuditByNameQueryHandler(IAuditRepositoryAsync auditRepositoryAsync, IMapper mapper)
        {
            _auditRepositoryAsync = auditRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<IReadOnlyList<UserAuditViewModel>>> Handle(GetUserAuditByNameQuery request, CancellationToken cancellationToken)
        {
            var auditList = await _auditRepositoryAsync.GetUserAuditAsync(request.UserName);
            return new Response<IReadOnlyList<UserAuditViewModel>>(_mapper.Map<IReadOnlyList<UserAuditViewModel>>(auditList));
        }
    }
}
